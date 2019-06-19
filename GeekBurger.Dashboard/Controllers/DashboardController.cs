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

        public DashboardController(ISalesService salesService, IUserWithLessOfferService userWithLessOfferService ,IMapper mapper)
        {
            _salesService = salesService;
            _userWithLessOfferService = userWithLessOfferService;
            _mapper = mapper;
        }

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

        [HttpGet("sales/{per}/{value}")]
        public IActionResult GetSales(string per, int value)
        {  
            return Ok();
        }

        [HttpGet("usersWithLessOffer")]
        public IActionResult GetUsersWithLessOffer()
        {
            IEnumerable<UsersRestrictionsDTO> usersRestrictionsDTOs = _userWithLessOfferService.GetAll().Result.GroupBy(g => g.Restrictions.ToString())
                                  .Select(x => new UsersRestrictionsDTO { Restrictions = x.Key, Users = x.Count() });

            return Ok(usersRestrictionsDTOs);
        }
        
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
    }
}
