using GeekBurger.Dashboard.Contract;
using GeekBurger.Dashboard.Repository.Interfaces;
using GeekBurger.Dashboard.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        public SalesService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public Sales GetByOrderId(string orderId)
        {
            return _salesRepository.GetByOrderId(orderId);
        }

        public IEnumerable<Sales> GetAllSalesCompleted()
        {
            return _salesRepository.GetAllSalesCompleted();
        }

        public void Insert(Sales sales)
        {
            sales.State = State.Open;
            sales.CreatedAt = DateTime.Now;

            _salesRepository.Insert(sales);
        }

        public bool OrderExists(string orderId)
        {
            return _salesRepository.OrderExists(orderId);
        }

        public void Update(Sales sales)
        {
            _salesRepository.Update(sales);
        }
    }
}
