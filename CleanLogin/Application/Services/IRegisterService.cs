using Application.DTOs;

namespace Application.Services;

public interface IRegisterService
{
    Task<CreateUsuarioResponse> RegisterAsync(CreateUsuarioRequest request, CancellationToken ct = default);
}
