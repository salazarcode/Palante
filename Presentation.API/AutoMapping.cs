using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using GestionCartera.API.ValueObjects;

namespace GestionCartera.API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Credito, CreditoDTO>();
        }
    }
}
