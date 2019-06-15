using GeekBurger.Dashboard.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.Repository.Interfaces
{
    public interface ISalesRepository
    {
        IEnumerable<Sales> GetAll();
        void Insert(Sales sales);
        bool OrderExists(string orderId);
        void Update(Sales sales);
        Sales GetByOrderId(string orderId);
    }
}
