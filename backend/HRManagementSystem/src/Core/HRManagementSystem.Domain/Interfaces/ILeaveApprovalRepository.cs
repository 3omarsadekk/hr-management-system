using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;
public interface ILeaveApprovalRepository : IRepository<LeaveApproval>
{
    Task<LeaveApproval> GetByLeaveRequestIdAsync(int leaveRequestId, CancellationToken cancellationToken = default);

    Task<IEnumerable<LeaveApproval>> GetByApproverIdAsync(int approverId, CancellationToken cancellationToken = default);

}
