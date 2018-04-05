using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using wasteless.Constants;
using wasteless.DTOs;
using wasteless.Forms;

namespace wasteless.Services
{
    public class DBService
    {
        
        private static readonly string connString = ConfigurationManager.ConnectionStrings["wastelessDB"].ConnectionString;
        
        //Native SQL calls, just for demonstration. Below this method, EF is used.
        public static bool FormLogin(LoginForm loginForm)
        {
            try
            {
                string cmdText = "SELECT * FROM Users WHERE Email = @email AND Password = @password";
                using (var conn = new SqlConnection(connString))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;

                    //TODO: ADDWITHVALUE INSTEAD!
                    cmd.Parameters.AddRange(new List<SqlParameter> { new SqlParameter("@email", SqlDbType.NVarChar) { Value = loginForm.Email },
                                                                     new SqlParameter("@password", SqlDbType.NVarChar) { Value = loginForm.Password } }.ToArray());
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        //TODO: TEST READER
                        reader.Read();

                        //TODO: SET AS VARIABLES, THEN TEST CONDITION
                        if (reader.GetString(3) != loginForm.Email || reader.GetString(4) != loginForm.Password) return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<FoodType> GetListableFoods()
        {
            var foodTypes = new List<FoodType>();
            try
            {
                using (var db = new WastelessContext())
                {
                    foodTypes = db.FoodTypes.ToList();
                    return foodTypes;
                }
            }
            catch (Exception e)
            {
                return foodTypes;
            }
        }

        public static List<FoodType> GetSearchResultListableFoods(string query, string options)
        {
            var foodTypes = new List<FoodType>();
            try
            {
                using (var db = new WastelessContext())
                {
                    switch (options.ToLower())
                    {
                        case "starts with": foodTypes = db.FoodTypes.Where(x => x.FoodTypeName.StartsWith(query)).ToList(); break;
                        case "ends with": foodTypes = db.FoodTypes.Where(x => x.FoodTypeName.EndsWith(query)).ToList(); break;
                        case "contains":
                        default: foodTypes = db.FoodTypes.Where(x => x.FoodTypeName.Contains(query)).ToList(); break;
                    }
                    return foodTypes;
                }
            }
            catch (Exception e)
            {
                return foodTypes;
            }
        }

        public static void DeleteListableFood(string id)
        {
            if (String.IsNullOrWhiteSpace(id)) return;
            try
            {
                using (var db = new WastelessContext())
                {
                    var toRemove = db.FoodTypes.First(x => x.FoodTypeID.ToString() == id);
                    db.FoodTypes.Remove(toRemove);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
            }
        }

        public static void CreateListableFood(string name, string code)
        {
            if (String.IsNullOrWhiteSpace(code)) code = "";
            try
            {
                using (var db = new WastelessContext())
                {
                    var toAdd = new FoodType { FoodTypeName = name, Code = code, Created = DateTime.Now, GUID = Guid.NewGuid() };
                    db.FoodTypes.Add(toAdd);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}