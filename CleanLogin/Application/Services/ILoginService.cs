namespace Application.Services;

using Application.DTOs;

public interface ILoginService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);
}
