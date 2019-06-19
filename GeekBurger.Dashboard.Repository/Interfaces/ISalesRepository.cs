using GeekBurger.Dashboard.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.Repository.Interfaces
{
    public interface ISalesRepository
    {
        Task<IEnumerable<Sales>> GetAllSalesCompleted();
        Task<IEnumerable<Sales>> GetAllPaidSalesByPeriod(DateTime dataCorte);
        Task Insert(Sales sales);
        Task<bool> OrderExists(string orderId);
        Task Update(Sales sales);
        Task<Sales> GetByOrderId(string orderId);
    }
}
