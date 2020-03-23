using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Contracts
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        //Métodos específicos como busquedas especializadas
        //Métodos para obtener los detalles de las relaciones
        List<Prestamo> Prestamos(out Cliente cliente);
    }
}
