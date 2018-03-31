using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wasteless.Constants
{
    public class QueryTemplates
    {
        public static readonly string selectWithContains = "SELECT * FROM FoodTypes WHERE FoodType LIKE'%@query%'";
        public static readonly string selectWithStartsWith = "SELECT * FROM FoodTypes WHERE FoodType LIKE'@query%'";
        public static readonly string selectWithEndsWith = "SELECT * FROM FoodTypes WHERE FoodType LIKE'%@query'";
    }
}