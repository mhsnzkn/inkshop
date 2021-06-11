using AutoMapper;
using Data.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Automapper
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<OrderAddDto, Order>();

        }
    }
}
