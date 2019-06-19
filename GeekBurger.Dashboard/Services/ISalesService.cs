using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Dashboard.Repository.Model;

namespace GeekBurger.Dashboard.Services
{
    public interface ISalesService
    {
        Task <Sales> GetByOrderId(string orderId);
        Task Update(Sales sales);
        Task<bool> OrderExists(string orderId);
        Task Insert(Sales sales);
        Task<IEnumerable<Sales>> GetAllSalesCompleted();
    }
}
