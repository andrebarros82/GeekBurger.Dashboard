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

        /// <summary>
        /// Pega todos os registro da tabela UserWithLessOffer.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserWithLessOffer>> GetAll()
        {
           return await _userWithLessOfferRepository.GetAll();
        }
        /// <summary>
        /// Inseri um UserWithLessOffer na base de dados.
        /// </summary>
        /// <param name="userWithLessOffer"></param>
        /// <returns>Task</returns>
        public async Task Insert(UserWithLessOffer userWithLessOffer)
        {
            await _userWithLessOfferRepository.Insert(userWithLessOffer);
        }
    }
}
