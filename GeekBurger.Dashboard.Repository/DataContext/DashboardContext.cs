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
        public DashboardContext(DbContextOptions<DashboardContext> options) : base(options)
        {
        }

        public DbSet<Sales> Sales { get; set; }
        public DbSet<UsersRestrictions> UsersRestrictions { get; set; }
    }
}
