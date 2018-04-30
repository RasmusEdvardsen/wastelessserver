using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using wasteless.EntityModel;

namespace wasteless.Services
{
    public class CacheService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool ResetCache(string key)
        {
            try
            {
                HttpContext.Current.Cache.Remove(key);
                return true;
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return false;
            }
        }

        public static List<string> GetFoodTypes()
        {
            var list = new List<string>();
            try
            {
                var cachedFoodTypes = HttpContext.Current.Cache["foodtypes"] as List<string>;
                if(cachedFoodTypes == null)
                {
                    cachedFoodTypes = DBService.GetFoodTypes().Select(x => x.FoodTypeName).ToList();
                    HttpContext.Current.Cache.Insert("foodtypes", cachedFoodTypes, null, DateTime.Now.AddMinutes(10d), Cache.NoSlidingExpiration);
                }
                return cachedFoodTypes;
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return list;
            }
        }
    }
}