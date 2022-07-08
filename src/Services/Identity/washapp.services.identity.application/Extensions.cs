using System.Reflection;
using washapp.services.identity.domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace washapp.services.identity.application;

public static class Extensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }
    public static void UseApplication(this IApplicationBuilder app)
    {
            
    }

}