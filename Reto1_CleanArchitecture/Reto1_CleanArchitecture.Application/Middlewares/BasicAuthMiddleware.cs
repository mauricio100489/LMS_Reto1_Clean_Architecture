using Microsoft.AspNetCore.Http;
using System.Text;
using Reto1_CleanArchitecture.Domain.Interfaces;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Extensions.DependencyInjection;

namespace Reto1_CleanArchitecture.Application.Middlewares
{
    public class BasicAuthMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {

        private readonly RequestDelegate _next = next;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _authenticationsService = scope.ServiceProvider.GetRequiredService<IAuthenticationsService>();

                string authHeader = context.Request.Headers["Authorization"];


                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    var encodedUsernamePassword = authHeader["Basic ".Length..].Trim();
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    var parts = decodedUsernamePassword.Split(':');

                    var username = parts[0];
                    var password = parts[1];

                    // Validación simple (puedes usar base de datos aquí)
                    var authentications = await _authenticationsService.GetAuthenticationsAsync();

                    if (username == authentications.UserName && password == authentications.Password)
                    {
                        await _next(context);
                        return;
                    }
                }

                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            
                await context.Response.WriteAsync("");
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}