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

        /// <summary>
        /// Obtém um objeto Sales pelo OrderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Task<Sales></returns>
        public async Task<Sales> GetByOrderId(string orderId)
        {
            return await _salesRepository.GetByOrderId(orderId);
        }

        /// <summary>
        /// Obtém uma lista q contém todas as vendas finalizadas.
        /// </summary>  
        /// <returns>Task</returns>
        public async Task<IEnumerable<Sales>> GetAllSalesCompleted()
        {
            return await _salesRepository.GetAllSalesCompleted();
        }

        /// <summary>
        /// Obtém uma lista q contém todas as vendas por período.
        /// </summary>
        /// <param name="dataCorte"></param>
        /// <returns>Task</returns>
        public async Task<IEnumerable<Sales>> GetAllPaidSalesByPeriod(DateTime dataCorte)
        {
            return await _salesRepository.GetAllPaidSalesByPeriod(dataCorte);
        }

        /// <summary>
        /// Inseri uma venda na base de dados
        /// </summary>
        /// <param name="sales"></param>
        /// <returns>Task</returns>
        public async Task Insert(Sales sales)
        {
            sales.State = State.Open;
            sales.CreatedAt = DateTime.Now;

            await _salesRepository.Insert(sales);
        }

        /// <summary>
        /// Verifica se uma determinada venda já existe na base de dados.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Task</returns>
        public async Task<bool> OrderExists(string orderId)
        {
            return await _salesRepository.OrderExists(orderId);
        }

        /// <summary>
        /// Atualiza uma venda na base de dados.
        /// </summary>
        /// <param name="sales"></param>
        /// <returns>Task</returns>
        public async Task Update(Sales sales)
        {
            await _salesRepository.Update(sales);
        }
    }
}
