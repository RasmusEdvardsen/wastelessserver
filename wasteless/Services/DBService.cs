﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
    }
}