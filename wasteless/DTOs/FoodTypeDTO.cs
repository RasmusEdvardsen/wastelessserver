using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wasteless.DTOs
{
    public class FoodTypeDTO
    {
        public int FoodTypeID { get; set; }
        public string FoodType { get; set; }
        public string Code { get; set; }
        public DateTime Created { get; set; }
        public Guid GUID { get; set; }
    }
}