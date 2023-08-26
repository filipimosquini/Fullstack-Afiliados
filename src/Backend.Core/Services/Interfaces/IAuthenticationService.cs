using Backend.Core.Services.DataTransferObjects;

namespace Backend.Core.Services.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticationDto> GenerateJwtToken(string email);
}