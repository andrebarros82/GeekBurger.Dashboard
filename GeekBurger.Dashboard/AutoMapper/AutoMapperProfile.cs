using AutoMapper;
using GeekBurger.Dashboard.Contract;
using GeekBurger.Dashboard.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Sales, SalesDTO>();
            CreateMap<UserWithLessOffer, UsersRestrictionsDTO>();
        }

    }
}
