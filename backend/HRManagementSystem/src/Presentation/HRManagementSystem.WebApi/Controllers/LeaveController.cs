using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Leaves;
using HRManagementSystem.Application.DTOs.Leaves.LeaveRequestDtos;
using HRManagementSystem.Application.DTOs.Leaves.LeaveTypeDtos;
using HRManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveController(ILeaveService _leaveService) : ControllerBase
{
    // ========================= Leave Types =========================

    [HttpPost("types")]
    public async Task<IActionResult> CreateLeaveType([FromBody] CreateLeaveTypeDto dto)
    {
        Response<int> result = await _leaveService.CreateLeaveTypeAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpPut("types/{id:int}")]
    public async Task<IActionResult> UpdateLeaveType([FromRoute] int id, [FromBody] UpdateLeaveTypeDto dto)
    {
        dto.Id = id;
        Response<bool> result = await _leaveService.UpdateLeaveTypeAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpGet("types")]
    public async Task<IActionResult> GetLeaveTypes()
    {
        Response<List<LeaveTypeDto>> result = await _leaveService.GetLeaveTypesAsync();
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpGet("types/{id:int}")]
    public async Task<IActionResult> GetLeaveType([FromRoute] int id)
    {
        Response<LeaveTypeDto> result = await _leaveService.GetLeaveTypeAsync(id);
        if (result.HasError)
            return NotFound(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpDelete("types/{id:int}")]
    public async Task<IActionResult> DeleteLeaveType([FromRoute] int id)
    {
        Response<bool> result = await _leaveService.DeleteLeaveTypeAsync(id);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    // ========================= Balances =========================

    [HttpGet("balances/{employeeId:int}/{leaveTypeId:int}/{year:int}")]
    public async Task<IActionResult> GetEmployeeBalance([FromRoute] int employeeId, [FromRoute] int leaveTypeId, [FromRoute] int year)
    {
        Response<EmployeeLeaveBalanceDto> result = await _leaveService.GetEmployeeBalanceAsync(employeeId, leaveTypeId, year);
        if (result.HasError)
            return NotFound(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpPost("balances/allocate/{year:int}")]
    public async Task<IActionResult> AllocateAnnualBalances([FromRoute] int year)
    {
        Response<int> result = await _leaveService.AllocateAnnualBalancesAsync(year);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    // ========================= Requests =========================

    [HttpPost("requests")]
    public async Task<IActionResult> RequestLeave([FromBody] CreateLeaveRequestDto dto, [FromQuery] int currentYear)
    {
        Response<int> result = await _leaveService.RequestLeaveAsync(dto, currentYear);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpGet("requests/employee/{employeeId:int}")]
    public async Task<IActionResult> GetEmployeeRequests([FromRoute] int employeeId)
    {
        Response<List<LeaveRequestDto>> result = await _leaveService.GetEmployeeRequestsAsync(employeeId);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpGet("requests/pending/{approverId:int}")]
    public async Task<IActionResult> GetPendingApprovals([FromRoute] int approverId)
    {
        Response<List<LeaveRequestDto>> result = await _leaveService.GetPendingApprovalsAsync(approverId);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    // ========================= Approvals =========================

    [HttpPost("approvals/approve")]
    public async Task<IActionResult> Approve([FromBody] LeaveApprovalActionDto dto)
    {
        Response<bool> result = await _leaveService.ApproveAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpPost("approvals/reject")]
    public async Task<IActionResult> Reject([FromBody] LeaveApprovalActionDto dto)
    {
        Response<bool> result = await _leaveService.RejectAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    // ========================= Utilities =========================

    // مثال: فحص التداخل عبر Query String:
    // GET api/leave/overlap?employeeId=5&start=2025-11-01&end=2025-11-03
    [HttpGet("overlap")]
    public async Task<IActionResult> HasOverlap([FromQuery] int employeeId, [FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        Response<bool> result = await _leaveService.HasOverlapAsync(employeeId, start, end);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }
}
