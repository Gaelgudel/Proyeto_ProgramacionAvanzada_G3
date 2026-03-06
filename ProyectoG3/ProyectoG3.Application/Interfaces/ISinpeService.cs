using ProyectoG3.Application.DTOs;
using ProyectoG3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoG3.Application.Interfaces
{
    public interface ISinpeService
    {
        Task<IEnumerable<Sinpe>> ObtenerSinpePorTelefono(string telefono);
        Task<bool> RegistrarAsync(SinpeCreateDto dto);
    }
}
