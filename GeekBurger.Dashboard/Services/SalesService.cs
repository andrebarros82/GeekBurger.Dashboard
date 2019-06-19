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

        public async Task<Sales> GetByOrderId(string orderId)
        {
            return await _salesRepository.GetByOrderId(orderId);
        }

        public async Task<IEnumerable<Sales>> GetAllSalesCompleted()
        {
            return await _salesRepository.GetAllSalesCompleted();
        }

        public async Task<IEnumerable<Sales>> GetAllPaidSalesByPeriod(DateTime dataCorte)
        {
            return await _salesRepository.GetAllPaidSalesByPeriod(dataCorte);
    }

        
        public async Task Insert(Sales sales)
        {
            sales.State = State.Open;
            sales.CreatedAt = DateTime.Now;

            await _salesRepository.Insert(sales);
        }

        public async Task<bool> OrderExists(string orderId)
        {
            return await _salesRepository.OrderExists(orderId);
        }

        public async Task Update(Sales sales)
        {
            await _salesRepository.Update(sales);
        }
    }
}
