using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace wasteless.Services
{
    public class ScrapeService
    {
        //TODO: Make log sit in /App_Data or /log, and make it save 30 files of 10mb each at most.
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static IEnumerable<WordScore> ScrapeGoogle(string id)
        {
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
                            if(tempCiteNode != null)
                                if ((((tempAnchorNode.InnerHtml.Contains(id)) ? 1 : 0) + ((tempCiteNode.InnerHtml.Contains(id)) ? 1 : 0) + ((tempSpanNode.InnerHtml.Contains(id)) ? 1 : 0)) < 2)
                                    continue;
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
                char[] delimiters = { ' ', ',', '.', ':', ';', '\n' };
                var wordListValidated = spanWordString.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Where(x => IsValid(x));

                var dashed = new List<string>();
                foreach(var dash in wordListValidated.Where(x => x.Contains('-'))) { dashed.AddRange(dash.Split('.').ToList()); }
                dashed = dashed.Where(x => !String.IsNullOrWhiteSpace(x)).Distinct().ToList();

                foreach (var word in wordListValidated.Distinct())
                {
                    try
                    {
                        if (dashed.Any(x => x.Contains(word) && !x.Equals(word))) continue;
                        var tempCount = new List<int>();
                        foreach (var split in word.Split('-').Where(x=>!String.IsNullOrWhiteSpace(x)))
                        {
                            tempCount.Add(Regex.Matches(spanWordString, split).Count);
                        }
                        tempCount.Add(Regex.Matches(spanWordString, word).Count);
                        if (tempCount.Any())
                            wordScoreList.Add(new WordScore { WordName = word, WordCount = (int)tempCount.Average() });
                    }
                    catch (Exception e)
                    {
                        log.Error(e.ToString());
                    }
                }

                list = wordScoreList.ToList();
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
            public int WordCount { get; set; }
            public string String() { return WordName + ": " + WordCount.ToString(); }
        }
        public class ToRemove
        {
            //TODO: As db table.
            public static readonly List<string> list = new List<string>() { "/", "og", "i", "and", "cl", "til", "export", "ean", "solvej", "nielsen"
                                                                            , "er", "de", "har", "en", "mejeri", "øko", "at", "der", "som", "upc", "vær"
                                                                            , "på", "den", "in", "or", "code", "packaging", "summer", "ch", "product", "www" };
        }
        public static bool IsValid(string str)
        {
            Regex Validator = new Regex(@"^[abcdefghijklmnopqrstuvwxyzæøå-]+$");
            if (Validator.IsMatch(str))
                if (str.Length > 1)
                    return !ToRemove.list.Contains(str);
            return false;
        }
    }
}