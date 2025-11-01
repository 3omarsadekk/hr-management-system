using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HRManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Add FluentValidation
        services.AddValidatorsFromAssembly(assembly);

        

        return services;
    }
}
