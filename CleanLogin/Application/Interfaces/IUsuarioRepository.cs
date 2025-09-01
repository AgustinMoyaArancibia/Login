using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioRepository
    {

        Task<Usuario?> GetByNombreAsync(string nombre , CancellationToken ct = default);
        //necesito un repositorio que me permita obtener un usuario por su nombre
        //se implementa en Infrastructure/Data/Repositories/UsuarioRepository.cs



    }
}
