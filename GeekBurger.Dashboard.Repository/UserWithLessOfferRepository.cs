using GeekBurger.Dashboard.Repository.DataContext;
using GeekBurger.Dashboard.Repository.Interfaces;
using GeekBurger.Dashboard.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.Repository
{
    public class UserWithLessOfferRepository : IUserWithLessOfferRepository
    {
        private DashboardContext _dashboardContext;

        public UserWithLessOfferRepository(DashboardContext dashboardContext)
        {
            _dashboardContext = dashboardContext;
        }

        public async Task<List<UserWithLessOffer>> GetAll()
        {
            return await _dashboardContext.UserWithLessOffers?.ToListAsync();
        }

        public async Task Insert(UserWithLessOffer userWithLessOffer)
        {
            await _dashboardContext.UserWithLessOffers.AddAsync(userWithLessOffer);
            await _dashboardContext.SaveChangesAsync();
        }
    }
}
