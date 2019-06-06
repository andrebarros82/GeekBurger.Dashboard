using GeekBurger.Dashboard.Repository.DataContext;
using GeekBurger.Dashboard.Repository.Interfaces;
using GeekBurger.Dashboard.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private DashboardContext _dashboardContext;

        public SalesRepository(DashboardContext dashboardContext)
        {
            _dashboardContext = dashboardContext;
        }

        public IEnumerable<Sales> GetAll()
        {
            return _dashboardContext.Sales?.ToList();
        } 
    }
}
