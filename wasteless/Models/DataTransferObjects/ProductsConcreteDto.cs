using System;

namespace wasteless.Models.DataTransferObjects
{
    public class ProductsConcreteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public ProductsConcreteDto() { }
    }
}