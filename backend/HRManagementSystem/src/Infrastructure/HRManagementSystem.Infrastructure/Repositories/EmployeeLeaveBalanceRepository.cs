
namespace HRManagementSystem.Infrastructure.Repositories;
public class EmployeeLeaveBalanceRepository(ApplicationDbContext _context) : Repository<EmployeeLeaveBalance>(_context), IEmployeeLeaveBalanceRepository
{
    public async Task<IEnumerable<EmployeeLeaveBalance>> GetByEmployeeIdAndYearAsync(int employeeId, int year)
    {
        return await _context.EmployeeLeaveBalances
            .Where(lb => lb.EmployeeId == employeeId && lb.Year==year)
            .Include(lb => lb.LeaveType)
            .ToListAsync();
    }
    public async Task<IEnumerable<EmployeeLeaveBalance>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _context.EmployeeLeaveBalances
            .Where(lb => lb.EmployeeId == employeeId)
            .Include(lb => lb.LeaveType)
            .OrderByDescending(lb=>lb.Year)
            .ToListAsync();
    }
}
