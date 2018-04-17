using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using wasteless.Forms;

namespace wasteless.Services
{
    public class DBService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #region FoodTypes
        //TODO: DROP 'LISTABLE' FROM NAMES, GIVES NO EXTRA CONTEXT
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

        public static User GetUser(string email, string password = null)
        {
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    User user = password != null 
                        ? db.Users.FirstOrDefault(x => x.Email == email && x.Password == password)
                        : db.Users.FirstOrDefault(x => x.Email == email);
                    return user ?? null;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return null;
            }
        }

        public static User GetUser(Guid guid)
        {
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    User user = db.Users.FirstOrDefault(x => x.ident == guid);
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

        #region Noise
        public static List<Noise> GetNoises()
        {
            var noises = new List<Noise>();
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    noises = db.Noises.ToList();
                    return noises;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return noises;
            }
        }

        public static List<Noise> GetSearchResultNoises(string query, string options)
        {
            var noises = new List<Noise>();
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    switch (options.ToLower())
                    {
                        case "starts with": noises = db.Noises.Where(x => x.NoiseWord.StartsWith(query)).ToList(); break;
                        case "ends with": noises = db.Noises.Where(x => x.NoiseWord.EndsWith(query)).ToList(); break;
                        case "contains":
                        default: noises = db.Noises.Where(x => x.NoiseWord.Contains(query)).ToList(); break;
                    }
                    return noises;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return noises;
            }
        }

        public static void DeleteNoise(string id)
        {
            if (String.IsNullOrWhiteSpace(id)) return;
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    var toRemove = db.Noises.First(x => x.NoiseID.ToString() == id);
                    db.Noises.Remove(toRemove);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }
        }

        public static void CreateNoise(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return;
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    var toAdd = new Noise { NoiseWord = name };
                    db.Noises.Add(toAdd);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }
        }
        #endregion Noise
    }
}