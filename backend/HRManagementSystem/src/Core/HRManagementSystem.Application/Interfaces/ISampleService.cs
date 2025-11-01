namespace HRManagementSystem.Application.Interfaces;

/// <summary>
/// Sample service interface - Replace with your actual service interfaces
/// </summary>
public interface ISampleService
{
    Task<IEnumerable<string>> GetAllAsync();
    Task<string?> GetByIdAsync(Guid id);
}
