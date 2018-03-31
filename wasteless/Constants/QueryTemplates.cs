using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wasteless.Constants
{
    public class QueryTemplates
    {
        public static readonly string SelectWithContains = "SELECT * FROM FoodTypes WHERE FoodType LIKE '%{0}%'";
        public static readonly string SelectWithStartsWith = "SELECT * FROM FoodTypes WHERE FoodType LIKE '{0}%'";
        public static readonly string SelectWithEndsWith = "SELECT * FROM FoodTypes WHERE FoodType LIKE '%{0}'";

        public static string getTemplate(string options)
        {
            string queryToUse = "";
            switch (options.Trim().ToLower())
            {
                case "starts with": queryToUse = SelectWithStartsWith; break;
                case "ends with": queryToUse = SelectWithEndsWith; break;
                case "contains":
                default: queryToUse = SelectWithContains; break;
            }
            return queryToUse;
        }
    }
}