using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wasteless.Models;
using wasteless.Services;

namespace wasteless.Resolvers
{
    public class ListableFoodTypeResolver
    {
        public static ListableFoodTypeViewModel GetListableFoodTypeResolver()
        {
            var model = new ListableFoodTypeViewModel()
            {
                ListableFoodTypeDTO = DBService.GetListableFoods()
            };
            return model;
        }
    }
}