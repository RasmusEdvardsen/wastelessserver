using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wasteless.DTOs;

namespace wasteless.Models
{
    public class ListableFoodTypeViewModel
    {
        public List<FoodType> ListableFoodTypeDTO { get; set; }
    }
}