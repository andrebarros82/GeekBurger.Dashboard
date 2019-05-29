using AutoMapper;
using GeekBurger.Dashboard.Contract;
using GeekBurger.Dashboard.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.Controllers
{
    [Route("api/dashboard")]
    public class DashboardController : Controller
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IMapper _mapper;

        public DashboardController(ISalesRepository salesRepository, IMapper mapper)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
        }

        [HttpGet("sales")]
        public IActionResult GetSales()
        {
            IEnumerable<SalesDTO> sales = _mapper.Map<IEnumerable<SalesDTO>>(_salesRepository.GetAll());

            if (sales.ToList().Count > 0)
                return Ok(sales);
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
            return Ok();
        }
    }
}
