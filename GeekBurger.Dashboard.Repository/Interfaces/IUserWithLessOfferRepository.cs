using GeekBurger.Dashboard.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.Repository.Interfaces
{
    public interface IUserWithLessOfferRepository
    {
        Task Insert(UserWithLessOffer userWithLessOffer);
        Task<List<UserWithLessOffer>> GetAll();
    }
}
