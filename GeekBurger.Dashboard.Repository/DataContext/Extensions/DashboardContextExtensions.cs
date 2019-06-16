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
                dashboardContext.Sales.Add(new Sales { Id = i.ToString(), StoreId = "1111",
                                                       Value = 111 });
            }

            dashboardContext.SaveChanges();
        }
    }
}
