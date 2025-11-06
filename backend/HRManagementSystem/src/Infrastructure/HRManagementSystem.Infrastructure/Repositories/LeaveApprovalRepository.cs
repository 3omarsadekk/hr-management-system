using System.Threading;

namespace HRManagementSystem.Infrastructure.Repositories;
public class LeaveApprovalRepository(ApplicationDbContext _context) : Repository<LeaveApproval>(_context), ILeaveApprovalRepository
{
    public async Task<IEnumerable<LeaveApproval>> GetByApproverIdAsync(int approverId, CancellationToken cancellationToken) 
    {
        return await _context.LeaveApprovals
            .Where(la => la.ApproverId==approverId)
            .Include(la => la.Approver)
            .OrderBy(la=>la.Status)
            .ToListAsync();
    }
    public async Task<LeaveApproval> GetByLeaveRequestIdAsync(int leaveRequestId, CancellationToken cancellationToken)
    {
        return await _context.LeaveApprovals
            .FirstOrDefaultAsync(la => la.LeaveRequestId == leaveRequestId, cancellationToken);
    }
}
