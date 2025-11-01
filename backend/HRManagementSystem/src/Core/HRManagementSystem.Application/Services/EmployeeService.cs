using System;
using HRManagementSystem.Application.DTOs;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Services;

public class EmployeeService (IRepository<Employee> employeeRepository) : IEmployeeService
{
    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken = default)
    {
        var employee = new Employee
        {
            FirstName = createEmployeeDto.FirstName,
            LastName = createEmployeeDto.LastName,
            Email = createEmployeeDto.Email,
            DateOfBirth = createEmployeeDto.DateOfBirth,
            HireDate = createEmployeeDto.HireDate,
            Position = createEmployeeDto.Position,
            Salary = createEmployeeDto.Salary
        };

        await employeeRepository.AddAsync(employee, cancellationToken);

        return new EmployeeDto
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            DateOfBirth = employee.DateOfBirth,
            HireDate = employee.HireDate,
            Position = employee.Position,
            Salary = employee.Salary
        };


    }
    public Task DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
    {
        var employees = await employeeRepository.GetAllAsync(cancellationToken);
        return employees.Select(e => new EmployeeDto
        {
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            DateOfBirth = e.DateOfBirth,
            HireDate = e.HireDate,
            Position = e.Position,
            Salary = e.Salary
        });

    }
    public Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
