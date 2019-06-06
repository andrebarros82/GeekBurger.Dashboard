using GeekBurger.Dashboard.Repository.MapModel;
using GeekBurger.Dashboard.Repository.Model;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Dashboard.Repository.DataContext
{
    public class DashboardContext : DbContext
    {
        public DashboardContext(DbContextOptionsBuilder<DashboardContext> options) : base(options.Options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Sales> Sales { get; set; }
        public DbSet<UsersRestrictions> UsersRestrictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       
            modelBuilder.ApplyConfiguration(new SalesMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
