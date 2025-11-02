using System;
using HRManagementSystem.Application.DTOs;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Services;

public class EmployeeService(IRepository<Employee> employeeRepository) : IEmployeeService
{
    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken = default)
    {
        var employee = new Employee
        {
            /*
                public required string FirstName { get; set; }
                public required string LastName { get; set; }
                public DateTime DateOfBirth { get; set; }
                public string? Gender { get; set; }
                public DateTime HireDate { get; set; }
                public DateTime? EFF_Start { get; set; }
                public DateTime? EFF_End { get; set; }
                public required string Email { get; set; }
                public string? ContactNumber { get; set; }
                public string? Address { get; set; }
                public decimal BasicSalary { get; set; }
                public string? ApplicationUserId { get; set; }

            */
            FirstName = createEmployeeDto.FirstName,
            LastName = createEmployeeDto.LastName,
            DateOfBirth = createEmployeeDto.DateOfBirth,
            Gender = createEmployeeDto.Gender,
            HireDate = createEmployeeDto.HireDate,
            EFF_Start = createEmployeeDto.EFF_Start,
            EFF_End = createEmployeeDto.EFF_End,
            Email = createEmployeeDto.Email,
            ContactNumber = createEmployeeDto.ContactNumber,
            Address = createEmployeeDto.Address,
            BasicSalary = createEmployeeDto.BasicSalary,
            ApplicationUserId = createEmployeeDto.ApplicationUserId
        };

        await employeeRepository.AddAsync(employee, cancellationToken);

        return new EmployeeDto
        {
            /*
                public int Id { get; set; }
                public required string FirstName { get; set; }
                public required string LastName { get; set; }
                public DateTime DateOfBirth { get; set; }
                public string? Gender { get; set; }
                public DateTime HireDate { get; set; }
                public DateTime? EFF_Start { get; set; }
                public DateTime? EFF_End { get; set; }
                public required string Email { get; set; }
                public string? ContactNumber { get; set; }
                public string? Address { get; set; }
                public decimal BasicSalary { get; set; }
                public string? ApplicationUserId { get; set; }

            */
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            DateOfBirth = employee.DateOfBirth,
            Gender = employee.Gender,
            HireDate = employee.HireDate,
            EFF_Start = employee.EFF_Start,
            EFF_End = employee.EFF_End,
            Email = employee.Email,
            ContactNumber = employee.ContactNumber,
            Address = employee.Address,
            BasicSalary = employee.BasicSalary,
            ApplicationUserId = employee.ApplicationUserId
        };


    }
    public Task DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Employee> employees = await employeeRepository.GetAllAsync(cancellationToken);
        return employees.Select(e => new EmployeeDto
        {
            FirstName = e.FirstName,
            LastName = e.LastName,
            DateOfBirth = e.DateOfBirth,
            Gender = e.Gender,
            HireDate = e.HireDate,
            EFF_Start = e.EFF_Start,
            EFF_End = e.EFF_End,
            Email = e.Email,
            ContactNumber = e.ContactNumber,
            Address = e.Address,
            BasicSalary = e.BasicSalary,
            ApplicationUserId = e.ApplicationUserId
        });

    }
    public Task<EmployeeDto?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task UpdateEmployeeAsync(int id, UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
