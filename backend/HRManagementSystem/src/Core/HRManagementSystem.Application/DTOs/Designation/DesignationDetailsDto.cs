using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Application.DTOs.Employee;

namespace HRManagementSystem.Application.DTOs.Designation;
public class DesignationDetailsDto: DesignationDto
{
    public List<EmployeeSummaryDto>? Employees { get; set; }
}
