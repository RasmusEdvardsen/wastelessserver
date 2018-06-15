using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using wasteless.EntityModel;
using wasteless.Forms;
using wasteless.Models.DataTransferObjects;

namespace wasteless.Services
{
    //Should have considered going public, not static, with this class, due to memory
    public class DBService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #region FoodTypes
        public static List<FoodType> GetFoodTypes()
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

        public static List<FoodType> GetFoodTypesSearchResult(string query, string options)
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

        public static void DeleteFoodType(string id)
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

        public static bool CreateFoodType(string name, string code)
        {
            if (String.IsNullOrWhiteSpace(code)) code = "";
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    var toAdd = new FoodType { FoodTypeName = name, Code = code, Created = DateTime.Now, GUID = Guid.NewGuid() };
                    db.FoodTypes.Add(toAdd);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return false;
            }
        }
        
        public static FoodType GetFoodType(int eanId)
        {
            using (var db = new wastelessdbEntities())
            {
                var ean = db.EANs.FirstOrDefault(x => x.EANID == eanId);
                var foodType = db.FoodTypes.FirstOrDefault(x => x.FoodTypeID == ean.FoodTypeID);
                return foodType;
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
                    var userToLogin = db.Users.FirstOrDefault(x => x.Email == loginForm.Email && x.Password == loginForm.Password);
                    if(userToLogin != null)
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
                    return (password != null
                        ? db.Users.FirstOrDefault(x => x.Email == email && x.Password == password)
                        : db.Users.FirstOrDefault(x => x.Email == email)) 
                        ?? null;
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
        public static User ClientSignup(string email, string password)
        {
            try
            {
                using(var db = new wastelessdbEntities())
                {
                    if (db.Users.Any(x => x.Email == email || (x.Email == email && x.Password == password))) return null;
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
                    return userToSignUp;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return null;
            }
        }
        #endregion Users

        #region Noises
        //TODO: MAKE NOISEWORD IN TABLE UNIQUE IN THE DATABASE! SECURITY!

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

        public static List<Noise> GetNoisesSearchResult(string query, string options)
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
                    if (!db.Noises.Any(x => x.NoiseWord.Equals(name)))
                    {
                        var toAdd = new Noise { NoiseWord = name };
                        db.Noises.Add(toAdd);
                        db.SaveChanges();
                        
                        var listToCache = HttpContext.Current.Cache["noisewords"] as List<string>;
                        if (listToCache== null)
                            listToCache = DBService.GetNoises().Select(x => x.NoiseWord).ToList();
                        listToCache.Add(name);
                        HttpContext.Current.Cache.Remove("noisewords");
                        HttpContext.Current.Cache.Insert("noisewords", listToCache, null, DateTime.Now.AddMinutes(10d), Cache.NoSlidingExpiration);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }
        }
        #endregion Noise

        #region Products
        public static List<Product> GetProducts()
        {
            var products = new List<Product>();
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    products = db.Products.ToList();
                    return products;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return products;
            }
        }

        public static List<Product> GetProductsForUser(int userID)
        {
            var products = new List<Product>();
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    return products = db.Products.Where(x => x.UserID == userID).ToList();
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return products;
            }
        }

        public static bool DeleteProduct(int productId, int userId)
        {
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    var toRemove = db.Products.First(x => x.ProductID == productId);
                    if (toRemove.UserID == userId)
                    {
                        db.Products.Remove(toRemove);
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return false;
            }
        }

        public static bool CreateProduct(Product product)
        {
            if (product == null) return false;
            if (product.GUID == new Guid())
                product.GUID = Guid.NewGuid();
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    if ((db.Products.Any(x => x.GUID == product.GUID))
                        || (!db.Users.Any(y => y.UserID == product.UserID)))
                        return false;
                    db.Products.Add(product);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return false;
            }
        }

        public static List<ProductsConcreteDto> ProductsToConcrete(List<Product> products)
        {
            var list = new List<ProductsConcreteDto>();
            foreach (var product in products)
            {
                try
                {
                    var productConcrete = new ProductsConcreteDto();
                    productConcrete.Id = product.ProductID;
                    productConcrete.Name = GetFoodType(product.EANID ?? default(int)).FoodTypeName;
                    productConcrete.ExpiryDate = product.ExpirationDate;
                    list.Add(productConcrete);
                }
                catch (Exception e)
                {
                    log.Error(e.ToString());
                }
            }
            return list;
        }
        #endregion

        #region EAN
        public static List<EAN> GetEANs()
        {
            var ean = new List<EAN>();
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    ean = db.EANs.ToList();
                    return ean;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return ean;
            }
        }

        public static void DeleteEAN(int id)
        {
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    var toRemove = db.EANs.First(x => x.EANID == id);
                    db.EANs.Remove(toRemove);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }
        }

        public static EAN CreateEAN(Int64 eanCode, string foodTypeName)
        {
            var ean = new EAN() { EAN_Value = eanCode.ToString(), EAN_Score = 1 };
            try
            {
                using (var db = new wastelessdbEntities())
                {
                    var foodtype = db.FoodTypes.FirstOrDefault(x=>x.FoodTypeName.Equals(foodTypeName));
                    if (foodtype == null)
                    {
                        if (!CreateFoodType(foodTypeName, ""))
                            return null;
                        foodtype = db.FoodTypes.FirstOrDefault(x => x.FoodTypeName.Equals(foodTypeName));
                    }

                    var existingEan = db.EANs.FirstOrDefault(x => x.FoodTypeID == foodtype.FoodTypeID 
                                                               && x.EAN_Value == eanCode.ToString());
                    if (existingEan != null)
                    {
                        existingEan.EAN_Score += 1;
                        db.SaveChanges();
                        return existingEan;
                    }

                    ean.FoodTypeID = foodtype.FoodTypeID;
                    db.EANs.Add(ean);
                    db.SaveChanges();
                    return ean;
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
                return null;
            }
        }
        #endregion
    }
}