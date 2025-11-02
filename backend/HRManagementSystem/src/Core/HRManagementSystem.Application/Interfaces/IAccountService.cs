using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRManagementSystem.Application.DTOs.Account;

namespace HRManagementSystem.Application.Interfaces;

public interface IAccountService
{
    // Authentication
    Task<(bool Succeeded, RegisterEmployeeResponseDto? Response, IEnumerable<string> Errors)> RegisterEmployeeAsync(RegisterEmployeeDto registerEmployeeDto);
    Task<(bool Succeeded, AuthResponseDto? Response, IEnumerable<string> Errors)> LoginAsync(LoginDto loginDto);
    Task LogoutAsync();

    // Password Management
    Task<(bool Succeeded, IEnumerable<string> Errors)> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto);
    Task<(bool Succeeded, IEnumerable<string> Errors)> ForgotPasswordAsync(string email);
    Task<(bool Succeeded, IEnumerable<string> Errors)> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

    // User Management
    Task<UserDto?> GetUserByIdAsync(Guid userId);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(Guid userId, UserDto userDto);
    Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(Guid userId);
    Task<bool> UserExistsAsync(string email);

    // Role Management
    Task<(bool Succeeded, IEnumerable<string> Errors)> AssignRoleAsync(AssignRoleDto assignRoleDto);
    Task<(bool Succeeded, IEnumerable<string> Errors)> RemoveRoleAsync(Guid userId, string roleName);
    Task<IEnumerable<string>> GetUserRolesAsync(Guid userId);
    Task<IEnumerable<UserDto>> GetUsersInRoleAsync(string roleName);


}
