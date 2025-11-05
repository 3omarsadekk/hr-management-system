using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Designation;
public class CreateDesignationDto
{
    [StringLength(100, MinimumLength = 5)]
    public string Title { get; set; }
    [StringLength(500)]
    public string? Description { get; set; }
}
