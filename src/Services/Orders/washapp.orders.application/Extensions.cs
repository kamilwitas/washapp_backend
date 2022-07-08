using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace washapp.orders.application;

public static class Extensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}