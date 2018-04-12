using Newtonsoft.Json;
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        //Native SQL calls, just for demonstration. Below this method, EF is used.
        public static bool FormLogin(LoginForm loginForm)
        {
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    var userToLogin = db.Users.First(x => x.Email == loginForm.Email && x.Password == loginForm.Password);
                    if (userToLogin.IsAdmin.HasValue)
                        if (userToLogin.IsAdmin.Value)
                            return true;
                    return false;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return false;
            }
        }



        #region FoodTypes
        public static List<FoodType> GetListableFoods()
        {
            var foodTypes = new List<FoodType>();
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    foodTypes = db.FoodTypes.ToList();
                    return foodTypes;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return foodTypes;
            }
        }

        public static List<FoodType> GetSearchResultListableFoods(string query, string options)
        {
            var foodTypes = new List<FoodType>();
            try
            {
                using (var db = new wastelessdbEntities())
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
                log.Error(e.ToString());
                return foodTypes;
            }
        }

        public static void DeleteListableFood(string id)
        {
            if (String.IsNullOrWhiteSpace(id)) return;
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    var toRemove = db.FoodTypes.First(x => x.FoodTypeID.ToString() == id);
                    db.FoodTypes.Remove(toRemove);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }
        }

        public static void CreateListableFood(string name, string code)
        {
            if (String.IsNullOrWhiteSpace(code)) code = "";
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    var toAdd = new FoodType { FoodTypeName = name, Code = code, Created = DateTime.Now, GUID = Guid.NewGuid() };
                    db.FoodTypes.Add(toAdd);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }
        }
        #endregion FoodTypes

        #region Users
        public static User ClientLogin(string email, string password)
        {
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    User user = db.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
                    return user ?? null;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return null;
            }
        }

        //TODO: Consider httprspcode returns instead of bool.
        public static bool ClientSignup(string email, string password)
        {
            try
            {
                using(var db = new wastelessdbEntities())
                {
                    if (db.Users.Any(x => x.Email == email || (x.Email == email && x.Password == password))) return false;
                    var userToSignUp = new User
                    {
                        Email = email,
                        Password = password,
                        IsAdmin = false,
                        CreatedDate = DateTime.Now,
                        ident = Guid.NewGuid()
                    };
                    db.Users.Add(userToSignUp);
                    var saveChanges = db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return false;
            }
        }
        #endregion Users
    }
}