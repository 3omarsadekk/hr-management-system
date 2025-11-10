
using HRManagementSystem.Application.DTOs.Leaves;




namespace HRManagementSystem.Application.Servicess;

    public class LeaveService //: ILeaveService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly ILeaveRequestRepository _leaveRequestRepo;
        private readonly ILeaveApprovalRepository _leaveApprovalRepo;
        private readonly IEmployeeLeaveBalanceRepository _leaveBalanceRepo;
        private readonly IRepository<Employee> _employeeRepo;         
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IMapper _mapper;

        public LeaveService(
            ILeaveTypeRepository leaveTypeRepo,
            ILeaveRequestRepository leaveRequestRepo,
            ILeaveApprovalRepository leaveApprovalRepo,
            IEmployeeLeaveBalanceRepository leaveBalanceRepo,
            IRepository<Employee> employeeRepo,
            IDepartmentRepository departmentRepo,
            IMapper mapper)
        {
            _leaveTypeRepo = leaveTypeRepo;
            _leaveRequestRepo = leaveRequestRepo;
            _leaveApprovalRepo = leaveApprovalRepo;
            _leaveBalanceRepo = leaveBalanceRepo;
            _employeeRepo = employeeRepo;
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

      


        //// ========================= Approvals =========================

        //public async Task<Response<bool>> ApproveAsync(LeaveApprovalActionDto dto)
        //{
        //    try
        //    {
        //    List<LeaveApproval> allSteps = (await _leaveApprovalRepo.GetAllAsync())
        //                       .Where(a => a.LeaveRequestId == dto.LeaveRequestId)
        //                       .ToList();

        //    LeaveApproval? approval = allSteps.FirstOrDefault(a =>
        //            a.LeaveRequestId == dto.LeaveRequestId &&
        //            a.ApproverId == dto.ApproverId &&
        //            a.Level == dto.Level &&
        //            a.ActionDate == null);

        //        if (approval is null)
        //            return new Response<bool>(false, "Approval step not found or already processed", true);

        //        approval.ActionDate = DateTime.UtcNow;
        //        await _leaveApprovalRepo.UpdateAsync(approval);

        //    bool stillPending = allSteps.Any(a => a.ActionDate == null);
        //        if (stillPending)
        //            return new Response<bool>(true, null, false); 

        //    LeaveRequest? req = await _leaveRequestRepo.GetByIdAsync(dto.LeaveRequestId);
        //        if (req is null)
        //            return new Response<bool>(false, "Leave request not found", true);

        //    List<EmployeeLeaveBalance> balances = (await _leaveBalanceRepo.GetByEmployeeIdAndYearAsync(req.EmployeeId, req.StartDate.Year)).ToList();
        //    EmployeeLeaveBalance? bal = balances.FirstOrDefault(b => b.LeaveTypeId == req.LeaveTypeId);
        //        if (bal is null)
        //            return new Response<bool>(false, "Balance not found for employee/year", true);

        //        if (bal.RemainingDays < req.TotalDays)
        //            return new Response<bool>(false, "Insufficient leave balance", true);

        //        bal.UsedDays += req.TotalDays;
        //        bal.RemainingDays -= req.TotalDays;
        //        bal.LastUpdated = DateTime.UtcNow;

        //        req.Status = LeaveStatus.Approved;
        //        req.ReviewedAt = DateTime.UtcNow;
        //        req.ReviewedById = dto.ApproverId;

        //        await _leaveBalanceRepo.UpdateAsync(bal);
        //        await _leaveRequestRepo.UpdateAsync(req);

        //        return new Response<bool>(true, null, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<bool>(false, $"Failed to approve: {ex.Message}", true);
        //    }
        //}

        //public async Task<Response<bool>> RejectAsync(LeaveApprovalActionDto dto)
        //{
        //    try
        //    {
        //    List<LeaveApproval> allSteps = (await _leaveApprovalRepo.GetAllAsync())
        //                       .Where(a => a.LeaveRequestId == dto.LeaveRequestId)
        //                       .ToList();

        //    LeaveApproval? approval = allSteps.FirstOrDefault(a =>
        //            a.LeaveRequestId == dto.LeaveRequestId &&
        //            a.ApproverId == dto.ApproverId &&
        //            a.Level == dto.Level &&
        //            a.ActionDate == null);

        //        if (approval is null)
        //            return new Response<bool>(false, "Approval step not found or already processed", true);

        //        approval.ActionDate = DateTime.UtcNow;
        //        await _leaveApprovalRepo.UpdateAsync(approval);

        //        var req = await _leaveRequestRepo.GetByIdAsync(dto.LeaveRequestId);
        //        if (req is null)
        //            return new Response<bool>(false, "Leave request not found", true);

        //        req.Status = LeaveStatus.Rejected;
        //        req.ReviewedAt = DateTime.UtcNow;
        //        req.ReviewedById = dto.ApproverId;

        //        await _leaveRequestRepo.UpdateAsync(req);

        //        foreach (var step in allSteps.Where(a => a.ActionDate == null))
        //        {
        //            step.ActionDate = DateTime.UtcNow;
        //            await _leaveApprovalRepo.UpdateAsync(step);
        //        }

        //        return new Response<bool>(true, null, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<bool>(false, $"Failed to reject: {ex.Message}", true);
        //    }
        //}

}
