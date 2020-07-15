﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface ICuotaRepository : IRepository<Cuota>
    {
        Task<List<Credito>> PorEstadosConContraparte(string nEstadoCuota, string nEstado);
    }
}
