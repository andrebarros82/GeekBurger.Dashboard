using GeekBurger.Dashboard.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.Services
{
    public interface IUserWithLessOfferService
    {
        Task Insert(UserWithLessOffer userWithLessOffer);
        Task<IEnumerable<UserWithLessOffer>> GetAll();
    }
}
