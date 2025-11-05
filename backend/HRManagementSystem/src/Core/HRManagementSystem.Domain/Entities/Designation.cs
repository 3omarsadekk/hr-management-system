using System.ComponentModel.DataAnnotations;

namespace HRManagementSystem.Domain.Entities;
public class Designation:BaseEntity
{
    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Title { get; set; }
    [StringLength(500)]
    public string? Description { get; set; }
    public int? EmployeeCount { get; set; }

    // Navigation property
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
