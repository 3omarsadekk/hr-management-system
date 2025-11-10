using HRManagementSystem.Application.Mappings;

namespace HRManagementSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Add FluentValidation
        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IDesignationService, DesignationService>();

        services.AddScoped<ILeaveRequestService, LeaveRequestService>();
        // services.AddScoped<ILeaveService, LeaveService>();
        services.AddAutoMapper(x => x.AddProfile(new MappingHelper()));
        services.AddAutoMapper(x => x.AddProfile(new LeaveProfile()));


        return services;
    }
}
