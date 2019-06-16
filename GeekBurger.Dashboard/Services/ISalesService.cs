using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Dashboard.Repository.Model;

namespace GeekBurger.Dashboard.Services
{
    public interface ISalesService
    {
        Sales GetByOrderId(string orderId);
        void Update(Sales sales);
        bool OrderExists(string orderId);
        void Insert(Sales sales);
        IEnumerable<Sales> GetAllSalesCompleted();
    }
}
