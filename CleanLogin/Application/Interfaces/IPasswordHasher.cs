using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPasswordHasher
    {
        bool Verify(string hash, string password);

        //el login debe recibir la contraseña en texto plano y compararla con el hash almacenado en la base de datos


    }
}
