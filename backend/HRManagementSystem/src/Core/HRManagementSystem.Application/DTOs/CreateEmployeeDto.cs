using System;

namespace HRManagementSystem.Application.DTOs;

public class CreateEmployeeDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime HireDate { get; set; }
    public required string Position { get; set; }
    public decimal Salary { get; set; }

}
