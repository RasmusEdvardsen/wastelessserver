namespace wasteless.Scripts
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WastelessContext : DbContext
    {
        public WastelessContext()
            : base("name=WastelessContext")
        {
        }

        public virtual DbSet<FoodType> FoodTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
