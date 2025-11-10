using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces.LeaveRepository;
public interface IEmployeeLeaveBalanceRepository:IRepository<EmployeeLeaveBalance>
{
    Task<IEnumerable<EmployeeLeaveBalance>> GetByEmployeeIdAndYearAsync(int employeeId, int year);
    Task<IEnumerable<EmployeeLeaveBalance>> GetByEmployeeIdAsync(int employeeId);
}
