﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ConfectioneryEntities : DbContext
    {
        public ConfectioneryEntities()
            : base("name=ConfectioneryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<buying> buyings { get; set; }
        public virtual DbSet<consist> consists { get; set; }
        public virtual DbSet<credit> credits { get; set; }
        public virtual DbSet<making> makings { get; set; }
        public virtual DbSet<post> posts { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<raw> raws { get; set; }
        public virtual DbSet<Repayment> Repayments { get; set; }
        public virtual DbSet<salary> salaries { get; set; }
        public virtual DbSet<selling> sellings { get; set; }
        public virtual DbSet<unit> units { get; set; }
        public virtual DbSet<worker> workers { get; set; }
        public virtual DbSet<budjet> budjets { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<month> months { get; set; }
        public virtual DbSet<year> years { get; set; }
    }
}
