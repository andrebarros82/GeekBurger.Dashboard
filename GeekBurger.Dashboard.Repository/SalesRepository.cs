using GeekBurger.Dashboard.Repository.DataContext;
using GeekBurger.Dashboard.Repository.Interfaces;
using GeekBurger.Dashboard.Repository.Model;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Sales>> GetAllSalesCompleted()
        {
            return await _dashboardContext.Sales?.Where(s => s.State == State.Finished).ToListAsync();
        }

        public async Task<Sales> GetByOrderId(string orderId)
        {
            return await _dashboardContext.Sales.FirstAsync(s => s.OrderId == orderId);
        }

        public async Task Insert(Sales sales)
        {
            await _dashboardContext.Sales.AddAsync(sales);
            await _dashboardContext.SaveChangesAsync();
        }

        public async Task<bool> OrderExists(string orderId)
        {
            return await _dashboardContext.Sales.AnyAsync(s => s.OrderId == orderId);
        }

        public async Task Update(Sales sales)
        {
            _dashboardContext.Sales.Update(sales);
            await _dashboardContext.SaveChangesAsync();
        }
    }
}
