using Microsoft.EntityFrameworkCore;
using Reto1_CleanArchitecture.Application.Middlewares;
using Reto1_CleanArchitecture.Application.Services;
using Reto1_CleanArchitecture.Domain.Interfaces;
using Reto1_CleanArchitecture.Domain.Models;
using Reto1_CleanArchitecture.Infrastructure.Context;
using Reto1_CleanArchitecture.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar AppDbContext con SQLite
string PathDB = ($"{builder.Configuration.GetConnectionString("DefaultConnection")}").Replace("@physicalPath\\", $"{AppDomain.CurrentDomain.BaseDirectory}");

builder.Services.AddDbContext<Reto1_DBContext>
(options =>
    options.UseSqlite($"{PathDB}")
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
           .EnableSensitiveDataLogging(true)
);

#region CORSPolicy
string Origin = builder.Configuration.GetValue<string>("Origin").ToString();
builder.Services.AddCors(policy =>
{
    policy.AddPolicy(name: "MiCors",
                     builder =>
                     {
                         builder.WithOrigins(Origin)
                         .AllowAnyMethod()
                         .AllowAnyHeader();
                     });
});
#endregion

#region Repositories Injections
builder.Services.AddScoped<IRepositoryGeneric<Contacts>, RepositoryGenericReto1<Contacts>>();
builder.Services.AddScoped<IRepositoryGeneric<Authentications>, RepositoryGenericReto1<Authentications>>();
#endregion

#region Services Injections
builder.Services.AddScoped<IContactsService, ContactsService>();
builder.Services.AddScoped<IAuthenticationsService, AuthenticationsService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reto 1 - Aplicando Clean Architecture");
    });
}

app.UseHttpsRedirection();

app.UseCors("MiCors");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<BasicAuthMiddleware>();

app.Run();
