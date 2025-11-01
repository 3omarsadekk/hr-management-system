using HRManagementSystem.Application.Interfaces;

namespace HRManagementSystem.Application.Services;

/// <summary>
/// Sample service implementation - Replace with your actual services
/// </summary>
public class SampleService : ISampleService
{
    public async Task<IEnumerable<string>> GetAllAsync()
    {
        await Task.CompletedTask;
        return new List<string> { "Sample1", "Sample2" };
    }

    public async Task<string?> GetByIdAsync(Guid id)
    {
        await Task.CompletedTask;
        return $"Sample-{id}";
    }
}
