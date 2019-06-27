using AutoMapper;
using GeekBurger.Dashboard.Contract;
using GeekBurger.Dashboard.Repository.Interfaces;
using GeekBurger.Dashboard.Repository.Model;
using GeekBurger.Dashboard.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.Controllers
{
    [Route("api/dashboard")]
    public class DashboardController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly IUserWithLessOfferService _userWithLessOfferService;
        private readonly IMapper _mapper;

        public DashboardController(ISalesService salesService, IUserWithLessOfferService userWithLessOfferService, IMapper mapper)
        {
            _salesService = salesService;
            _userWithLessOfferService = userWithLessOfferService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todas as vendas finalizadas
        /// api/dashboard/sales/
        /// </summary>
        /// <returns></returns>
        [HttpGet("sales")]
        public IActionResult GetSales()
        {
            IEnumerable<SalesDTO> salesDTOs = _salesService.GetAllSalesCompleted().Result.GroupBy(g => g.StoreName)
                                              .Select(x => new SalesDTO { StoreName = x.Key, Total = x.Count(), Value = x.Sum(s => s.Value) });

            if (salesDTOs.ToList().Count > 0)
                return Ok(salesDTOs);
            else
                return NotFound();
        }

        /// <summary>
        /// Retorna todas as vendas finalizadas por período 
        /// api/dashboard/sales/minute/30
        /// </summary>
        /// <param name="per"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet("sales/{per}/{value}")]
        public IActionResult GetSales(string per, int value)
        {
            DateTime dataCorte = DateTime.Now;
            if (per.Contains("minute"))
                dataCorte = dataCorte.AddMinutes(value * -1);
            else if (per.Contains("second"))
                dataCorte = dataCorte.AddSeconds(value * -1);
            else
                dataCorte = dataCorte.AddHours(value * -1);


            IEnumerable<SalesDTO> salesDTOs = _salesService.GetAllPaidSalesByPeriod(dataCorte).Result.GroupBy(g => g.StoreName)
                                           .Select(x => new SalesDTO { StoreName = x.Key, Total = x.Count(), Value = x.Sum(s => s.Value) });

            return Ok(salesDTOs);
        }

        /// <summary>
        /// Retorna todas as restrições dos usuários
        /// api/dashboard/usersWithLessOffer/
        /// </summary>
        /// <returns></returns>
        [HttpGet("usersWithLessOffer")]
        public IActionResult GetUsersWithLessOffer()
        {
            IEnumerable<UsersRestrictionsDTO> usersRestrictionsDTOs = _userWithLessOfferService.GetAll().Result.GroupBy(x => x.RestrictionsUser)
                                 .Select(x => new UsersRestrictionsDTO { User = x.Count(), Restrictions = x.Key });

            return Ok(usersRestrictionsDTOs);
        }

        /// <summary>
        /// api/dashboard/chart/
        /// Exibi o gráfico com todas as vendas realizadas e com todas as restrições dos usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet("chart")]
        public ContentResult GetChart()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = System.IO.File.ReadAllText("Views/chart.html")
            };
        }

        /// <summary>
        /// Exibi o gráfico com todas as vendas realizadas por período e com todas as restrições dos usuários
        /// api/dashboard/chart/minute/30
        /// </summary>
        /// <param name="per"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet("chart/{per}/{value}")]
        public ContentResult GetChart(string per, int value)
        {
            string html = System.IO.File.ReadAllText("Views/chart.html").Replace("{param1}", per).Replace("{param2}", value.ToString()).Replace("parameter = false", "parameter = true");

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = html,
            };
        }
    }
}
