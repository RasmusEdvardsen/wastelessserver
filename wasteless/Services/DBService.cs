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
using wasteless.Models;

namespace wasteless.Services
{
    public class DBService
    {
        private static readonly string connString = ConfigurationManager.ConnectionStrings["wastelessDB"].ConnectionString;

        public static bool FormLogin(LoginForm loginForm)
        {
            try
            {
                string cmdText = "SELECT * FROM Users WHERE Email = @email AND Password = @password";
                using (var conn = new SqlConnection(connString))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.Parameters.AddRange(new List<SqlParameter> { new SqlParameter("@email", SqlDbType.NVarChar) { Value = loginForm.Email },
                                                                     new SqlParameter("@password", SqlDbType.NVarChar) { Value = loginForm.Password } }.ToArray());
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
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

        public static List<FoodTypeDTO> GetListableFoods()
        {
            var foodTypes = new List<FoodTypeDTO>();
            try
            {
                string cmdText = "SELECT * FROM FoodTypes";
                using (var conn = new SqlConnection(connString))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                foodTypes.Add(new FoodTypeDTO()
                                {
                                    FoodTypeID = !reader.IsDBNull(0) ? reader.GetInt32(0) : -1,
                                    FoodType = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                    Code = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                    Created = reader.GetDateTime(3),
                                    GUID = reader.GetGuid(4)
                                });
                            }
                            catch (Exception e)
                            {
                                //A tuple failed, should log somewhere.
                            }
                        }
                    }
                    return foodTypes;
                }
            }
            catch (Exception e)
            {
                return foodTypes;
            }
        }

        public static List<FoodTypeDTO> GetSearchResultListableFoods(string query, string options)
        {
            var foodTypes = new List<FoodTypeDTO>();
            try
            {
                using (var conn = new SqlConnection(connString))
                using (var cmd = conn.CreateCommand())
                {
                    //TODO: Change this to comply with sqlparams standards.
                    cmd.CommandText = String.Format(QueryTemplates.getTemplate(options), query);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                foodTypes.Add(new FoodTypeDTO()
                                {
                                    FoodTypeID = !reader.IsDBNull(0) ? reader.GetInt32(0) : -1,
                                    FoodType = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                    Code = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                    Created = reader.GetDateTime(3),
                                    GUID = reader.GetGuid(4)
                                });
                            }
                            catch (Exception e)
                            {
                                //A tuple failed, should log somewhere.
                            }
                        }
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
                using (var conn = new SqlConnection(connString))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = QueryTemplates.DeleteQuery;
                    cmd.Parameters.AddWithValue("@id", id.Trim());
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return;
                }
            }
            catch (Exception e)
            {
                return;
            }
        }

        public static void CreateListableFood(string name, string code)
        {
            //Do this properly. This is shit.
            if (String.IsNullOrWhiteSpace(code)) code = "";
            try
            {
                using (var conn = new SqlConnection(connString))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = QueryTemplates.InsertQuery;
                    cmd.Parameters.AddRange(new List<SqlParameter> { new SqlParameter("@foodtype", SqlDbType.NVarChar) { Value = name },
                                                                     new SqlParameter("@code", SqlDbType.NVarChar) { Value = code } }.ToArray());
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return;
                }
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}