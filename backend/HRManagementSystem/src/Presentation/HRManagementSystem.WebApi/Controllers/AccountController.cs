using System.Security.Claims;
using HRManagementSystem.Application.Common;
using Microsoft.AspNetCore.Authorization;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService _accountService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        Response<AuthResponseDto> result = await _accountService.LoginAsync(loginDto);
        if (result.HasError)
        {
            return Unauthorized(new { hasError = result.HasError, errorMessage = result.ErrorMessage });
        }
        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto assignRoleDto)
    {
        Response<bool> result = await _accountService.AssignRoleAsync(assignRoleDto);
        if (result.HasError)
        {
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });
        }
        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    // Register
    [HttpPost("register-employee")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeDto registerEmployeeDto)
    {
        Response<RegisterEmployeeResponseDto> result = await _accountService.RegisterEmployeeAsync(registerEmployeeDto);
        if (result.HasError)
        {
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });
        }
        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized(new
            {
                hasError = true,
                errorMessage = "Unable to identify the user."
            });
        }

        var result = await _accountService.ChangePasswordAsync(Guid.Parse(userId), dto);

        if (result.HasError)
        {
            return BadRequest(new { hasError = true, errorMessage = result.ErrorMessage });
        }

        return Ok(new { hasError = false, data = result.Data });
    }


}
