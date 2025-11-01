using System;
using HRManagementSystem.Application.DTOs;

namespace HRManagementSystem.Application.Interfaces;

public interface IEmployeeService
{

    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken = default);
    Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken = default);
    Task UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken = default);
    Task DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken = default);

}
