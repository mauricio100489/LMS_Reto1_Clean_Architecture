using Microsoft.EntityFrameworkCore;
using Reto1_CleanArchitecture.Infrastructure.Repository;
using Reto1_CleanArchitecture.Domain.Interfaces;
using Reto1_CleanArchitecture.Domain.Models;

namespace Reto1_CleanArchitecture.Application.Services
{
    public class AuthenticationsService(IRepositoryGeneric<Authentications> _authenticationsRepository) : IAuthenticationsService
    {
        private readonly IRepositoryGeneric<Authentications> _authenticationsRepository = _authenticationsRepository;

        public async Task<Authentications?> GetAuthenticationsAsync()
        {
            try
            {
                var authentications = await _authenticationsRepository
                                            .All()
                                            .Where(c => c.AuthId == 1)
                                            .FirstOrDefaultAsync();

                if (authentications is null)
                    throw new Exception("No se encontró la configuración de autenticación");

                return authentications;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Autenticaciones", ex);
            }
        }
    }
}
