using ProyectoG3.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoG3.Application.Interfaces
{
    public interface ISinpeService
    {
        Task<bool> RegistrarAsync(SinpeCreateDto dto);
    }
}
