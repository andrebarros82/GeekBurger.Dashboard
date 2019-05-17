using GeekBurger.Dashboard.Contract;
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
        public readonly List<UsersRestrictions> _usersRestrictions;
        public readonly Sales _sales;

        public DashboardController()
        {
            _sales = new Sales { StoredId = Guid.NewGuid(), Total = 1000, Value = "59385.00" };

            _usersRestrictions = new List<UsersRestrictions>
            {
                new UsersRestrictions { Users = 1, Restrictions = "soy, dairy" },
                new UsersRestrictions { Users = 2, Restrictions = "soy, dairy, peanut" }
            };
        }

        [HttpGet("sales")]
        public IActionResult GetSales()
        {
            return Ok(_sales);
        }
        
        [HttpGet("sales/{per}/{value}")]
        public IActionResult GetSales(string per, int value)
        {  
            return Ok(_sales);
        }

        [HttpGet("usersWithLessOffer")]
        public IActionResult GetUsersWithLessOffer()
        {
            return Ok(_usersRestrictions);
        }
    }
}
