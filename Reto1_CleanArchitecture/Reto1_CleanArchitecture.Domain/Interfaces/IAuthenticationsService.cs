using Reto1_CleanArchitecture.Domain.Models;

namespace Reto1_CleanArchitecture.Domain.Interfaces
{
    public interface IAuthenticationsService
    {
        Task<Authentications?> GetAuthenticationsAsync();
    }
}
