namespace wasteless.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FoodType
    {
        public int FoodTypeID { get; set; }

        [Column("FoodType")]
        [Required]
        [StringLength(255)]
        //TODO: CHANGE IN DB; APPEND NAME TO FOODTYPE.
        public string FoodTypeName { get; set; }

        [StringLength(255)]
        public string Code { get; set; }

        public DateTime? Created { get; set; }

        public Guid? GUID { get; set; }
    }
}
