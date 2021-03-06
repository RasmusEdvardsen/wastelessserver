﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wasteless.EntityModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class wastelessdbEntities : DbContext
    {
        public wastelessdbEntities()
            : base("name=wastelessdbEntities")
        {
            /*TODO: This is incredibly important. Not setting it to false, 
             * but the actual setting itself. If lazy load is enabled,
             * it will only load query specific content. If disabled,
             * it will load all related data as well - in this case,
             * The user of the product (because of the foreign key),
             * or all products of the user (because of the relation to the product).
             * It is a question of faster runtime, relevance and errorhandling.
             * Worthy of discussion.
             */
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EAN> EANs { get; set; }
        public virtual DbSet<FoodType> FoodTypes { get; set; }
        public virtual DbSet<Noise> Noises { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
