using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtTokenService _jwt;
        private readonly IUnitOfWork _uow;



        public LoginService(IUsuarioRepository repo, IPasswordHasher hasher, IJwtTokenService jwt, IUnitOfWork uow )
        {
            _repo = repo; _hasher = hasher; _jwt = jwt; _uow = uow;
        }
        //_repo leer usuario por nombre
        //_hasher verificar contraseña
        //_jwt generar token
        //_uow guardar cambios


        public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
        {
            var user = await _repo.GetByNombreAsync(request.Nombre, ct)
                       ?? throw new UnauthorizedAccessException("Usuario o contraseña inválidos");
            //si no existe el usuario lanzo excepcion


            if (!_hasher.Verify(user.PasswordHash, request.Password))
                throw new UnauthorizedAccessException("Usuario o contraseña inválidos");
            //si la contraseña no coincide lanzo excepcion

        
            user.FechaLogin = DateTime.UtcNow;
            await _uow.SaveChangesAsync(ct);
            //si todo es correcto actualizo la fecha de login

            var token = _jwt.Generate(user);
            //genero el token
            return new LoginResponse(user.IdUsuario, user.Nombre, user.Apellido, user.FechaLogin, token);
        }
    }
}
