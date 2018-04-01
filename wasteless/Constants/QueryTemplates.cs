using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wasteless.Constants
{
    public class QueryTemplates
    {
        public static string InsertQuery = "INSERT INTO FoodTypes (FoodType, Code) VALUES (@foodtype, @code)";

        public static string DeleteQuery = "DELETE FROM FoodTypes WHERE FoodTypeID = @id";

        public static string SelectWithContains = "SELECT * FROM FoodTypes WHERE FoodType LIKE '%{0}%'";
        public static string SelectWithStartsWith = "SELECT * FROM FoodTypes WHERE FoodType LIKE '{0}%'";
        public static string SelectWithEndsWith = "SELECT * FROM FoodTypes WHERE FoodType LIKE '%{0}'";

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