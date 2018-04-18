using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wasteless.Models;
using wasteless.Services;

namespace wasteless.Resolvers
{
    public class FoodTypeListResolver
    {
        public static FoodTypeListViewModel GetFoodTypeListResolver()
        {
            var model = new FoodTypeListViewModel()
            {
                FoodTypeListDTO = DBService.GetFoodTypes()
            };
            return model;
        }
    }
}