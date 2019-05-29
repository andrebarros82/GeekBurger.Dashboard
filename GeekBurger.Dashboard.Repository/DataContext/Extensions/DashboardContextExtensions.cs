using GeekBurger.Dashboard.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Dashboard.Repository.DataContext.Extensions
{
    public static class DashboardContextExtensions
    {
        public static void Seed(this DashboardContext dashboardContext)
        {   
            dashboardContext.Sales.RemoveRange(dashboardContext.Sales);
            dashboardContext.SaveChanges();

            for (var i = 0; i < 100; i++)
            {
                dashboardContext.Sales.Add(new Sales { Id = Guid.NewGuid(), StoredId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75c834e"),
                                                       Total = i, Value = "100" });
            }

            dashboardContext.SaveChanges();
        }
    }
}
