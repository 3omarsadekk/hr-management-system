using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums;

namespace HRManagementSystem.Application.DTOs.Leaves;
public class LeaveApprovalActionDto
{
    public int LeaveRequestId { get; set; }
    public int ApproverId { get; set; }
    public LevelApproval Level { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? Comment { get; set; }
}
