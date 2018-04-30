using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace wasteless.Services
{
    public class ScrapeService
    {
        //TODO: Make log sit in /App_Data or /log, and make it save 30 files of 10mb each at most.
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly char[] delimiters = { ' ', ',', '.', ':', ';', '\n' };
        
        //TODO: Some products are in 2 words (e.g. Faxe Kondi)

        /// <summary>
        /// Scrapes google by searching with the given id, finding N occurences of words in search results. 
        /// End result is to find the product of the ean code (e.g. milk).
        /// </summary>
        /// <param name="id">EAN code</param>
        /// <returns>Words found scraping google, scored by occurrences.</returns>
        public static IEnumerable<WordScore> ScrapeGoogle(string id)
        {
            //TODO: SEARCH OTHER SITES AS WELL (EAN,UPC,CHECKER, etc.) SCORE WORDS FOUND FROM GOOGLE ALSO HIGHER.
            var list = new List<WordScore>();
            try
            {
                //Get document
                HtmlWeb web = new HtmlWeb();
                HtmlDocument googleDoc = web.Load("https://www.google.dk/search?q=" + id);
                
                //Get inner search result divisions
                var divNodeList = googleDoc.DocumentNode.CssSelect("div");
                var divNodeListCleaned = new List<HtmlNode>();
                foreach (var divNode in divNodeList)
                {
                    if (divNode.Attributes["class"] != null)
                        if (divNode.Attributes["class"].Value == "g")
                            divNodeListCleaned.Add(divNode);
                }

                //Get spantext, citation and anchorlink
                var spanWordList = new List<String>();
                var anchorNodeList = new List<HtmlNode>();
                var citeNodeList = new List<HtmlNode>();
                var spanNodeList = new List<HtmlNode>();
                foreach (var divNode in divNodeListCleaned)
                {
                    try
                    {
                        var tempAnchorNode = divNode.CssSelect("h3.r > a").FirstOrDefault();
                        var tempSpanNode = divNode.CssSelect("span.st").FirstOrDefault();
                        var tempCiteNode = divNode.ChildNodes.FirstOrDefault(x => x.Name.Equals("div")).ChildNodes.FirstOrDefault(x => x.Name.Equals("div"));
                        if (tempSpanNode != null && tempAnchorNode != null)
                        {
                            //if(tempCiteNode != null)
                            //    if ((((tempAnchorNode.InnerHtml.Contains(id)) ? 1 : 0) + ((tempCiteNode.InnerHtml.Contains(id)) ? 1 : 0) + ((tempSpanNode.InnerHtml.Contains(id)) ? 1 : 0)) < 2)
                            //        continue;
                            if (tempSpanNode.InnerText.Contains(id))
                            {
                                spanWordList.Add(tempSpanNode.InnerText);
                                spanNodeList.Add(tempSpanNode);
                                if (tempAnchorNode.Attributes["href"] != null)
                                {
                                    spanWordList.Add(tempAnchorNode.InnerText);
                                    anchorNodeList.Add(tempAnchorNode);
                                    if (tempCiteNode != null)
                                    {
                                        spanWordList.Add(tempCiteNode.InnerText);
                                        citeNodeList.Add(tempCiteNode);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e.ToString());
                    }
                }

                //Validate words
                var spanWordString = String.Join(" ", spanWordList).ToLower();
                var wordScoreList = new List<WordScore>();
                
                var wordListValidated = spanWordString.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Where(x => IsValid(x));

                var dashed = new List<string>();
                foreach(var dash in wordListValidated.Where(x => x.Contains('-'))) { dashed.AddRange(dash.Split('.').ToList()); }
                dashed = dashed.Where(x => !String.IsNullOrWhiteSpace(x)).Distinct().ToList();

                foreach (var word in wordListValidated.Distinct())
                {
                    try
                    {
                        if (word.Equals("be"))
                        {
                            var test = "";
                        }
                        if (dashed.Any(x => x.Contains(word) && !x.Equals(word))) continue;
                        var tempCount = 0;
                        var occurrence = word.Split('-').Where(x => !String.IsNullOrWhiteSpace(x)).First();
                        tempCount += Regex.Matches(spanWordString, occurrence).Count;
                        if (tempCount != 0)
                            wordScoreList.Add(new WordScore { WordName = word, WordCount = tempCount });
                    }
                    catch (Exception e)
                    {
                        log.Error(e.ToString());
                    }
                }

                //TODO: WORDS WITH N OCCURRENCES - IF FOUND IN OTHER WORDS ("mælk" in "skummetmælk") ADD TO SCORE. INCLUDING NOISE WORDS.
                //TODO: IMPORTANT! SCORE WORDS OCCURRING IN SEARCHRESULTS HIGHER THAN OTHERS!

                list = wordScoreList.ToList();
                
                //For every atomic occurrence of a word in html nodes, times the word score by (1+count/10)
                //E.g. A word occurs in 7 html nodes -> wordscore*1.7.
                foreach(var word in list)
                {
                    var count = 0d;
                    var occurrence = word.WordName;

                    if (word.WordName.Contains("-"))
                        occurrence = word.WordName.Split('-').First();

                    foreach (var text in spanWordList)
                    {
                        try
                        {
                            if (text.ToLower().Contains(occurrence.ToLower()))
                                count++;
                        }
                        catch (Exception e)
                        {
                            log.Error(e.ToString());
                        }
                    }
                    word.WordCount *= count > 1 ? (1 + count/10) : 1;
                }

                //Check DB for food types, and add to score if found.
                var foodTypeList = CacheService.GetFoodTypes().Select(x=>x.ToLower());
                string asd = string.Join("|", list.Select(x => x.WordName));
                if (foodTypeList.Any())
                {
                    foreach (var word in list)
                    {
                        if (foodTypeList.Any(x=>x.Equals(word.WordName.ToLower())))
                        {
                            word.WordCount += 30;
                        }
                    }
                }

                return list;
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return list;
            }
        }
        public class WordScore
        {
            //TODO: Word occurring 5 times in 1 span = bad. Word occurring 1 time in 5 spans = good.
            //TODO: Regarding score: Give itempropped names higher score - or occurrence.
            public string WordName { get; set; }
            public double WordCount { get; set; }
            public string String() { return WordName + ": " + WordCount.ToString(); }
        }

        public static bool IsValid(string str)
        {
            var cachedNoiseWords = HttpContext.Current.Cache["noisewords"] as List<string>;
            if(cachedNoiseWords == null)
            {
                cachedNoiseWords = DBService.GetNoises().Select(x=>x.NoiseWord).ToList();
                HttpContext.Current.Cache.Insert("noisewords", cachedNoiseWords, null, DateTime.Now.AddMinutes(10d), Cache.NoSlidingExpiration);
            }
            Regex Validator = new Regex(@"^[abcdefghijklmnopqrstuvwxyzæøå-]+$");
            if (Validator.IsMatch(str))
                if (str.Length > 1)
                    return !cachedNoiseWords.Contains(str);
            return false;
        }
        
    }
}