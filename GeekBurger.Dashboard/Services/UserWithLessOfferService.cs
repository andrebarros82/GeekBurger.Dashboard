using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Dashboard.Repository.Interfaces;
using GeekBurger.Dashboard.Repository.Model;

namespace GeekBurger.Dashboard.Services
{
    public class UserWithLessOfferService : IUserWithLessOfferService
    {
        private readonly IUserWithLessOfferRepository _userWithLessOfferRepository;
        public UserWithLessOfferService(IUserWithLessOfferRepository userWithLessOfferRepository)
        {
            _userWithLessOfferRepository = userWithLessOfferRepository;
        }

        public async Task<IEnumerable<UserWithLessOffer>> GetAll()
        {
           return await _userWithLessOfferRepository.GetAll();
        }

        public async Task Insert(UserWithLessOffer userWithLessOffer)
        {
            await _userWithLessOfferRepository.Insert(userWithLessOffer);
        }
    }
}
