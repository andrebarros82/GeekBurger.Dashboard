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

        public DbSet<UserRestriction> UserRestrictions { get; set; }
        public DbSet<UserWithLessOffer> UserWithLessOffers { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<UserWithLessOffer> UsersRestrictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       
            modelBuilder.ApplyConfiguration(new SalesMap());
            modelBuilder.ApplyConfiguration(new UserWithLessOfferMap());
            modelBuilder.ApplyConfiguration(new UserRestrictionMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
