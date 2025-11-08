using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Application.DTOs.Leaves;
using HRManagementSystem.Application.DTOs.Leaves.LeaveRequestDtos;
using HRManagementSystem.Application.DTOs.Leaves.LeaveTypeDtos;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;




namespace HRManagementSystem.Application.Services;

    public class LeaveService : ILeaveService
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

        // ========================= Leave Types =========================

        public async Task<Response<int>> CreateLeaveTypeAsync(CreateLeaveTypeDto dto)
        {
            try
            {
                LeaveType entity = _mapper.Map<LeaveType>(dto);
                entity.CreatedAt = DateTime.UtcNow;
                entity.CreatedDate = DateTime.UtcNow;

                await _leaveTypeRepo.AddAsync(entity);
                return new Response<int>(entity.Id, null, false);
            }
            catch (Exception ex)
            {
                return new Response<int>(0, $"Failed to create leave type: {ex.Message}", true);
            }
        }

        public async Task<Response<bool>> UpdateLeaveTypeAsync(UpdateLeaveTypeDto dto)
        {
            try
            {
                LeaveType? entity = await _leaveTypeRepo.GetByIdAsync(dto.Id);
                if (entity is null)
                    return new Response<bool>(false, "LeaveType not found", true);

                _mapper.Map(dto, entity);
                entity.UpdatedAt = DateTime.UtcNow;

                await _leaveTypeRepo.UpdateAsync(entity);
                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, $"Failed to update leave type: {ex.Message}", true);
            }
        }

        public async Task<Response<List<LeaveTypeDto>>> GetLeaveTypesAsync()
        {
            try
            {
                IEnumerable<LeaveType> list = await _leaveTypeRepo.GetAllAsync();
                List<LeaveTypeDto> dtos = _mapper.Map<List<LeaveTypeDto>>(list);
                return new Response<List<LeaveTypeDto>>(dtos, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<LeaveTypeDto>>(null, $"Failed to load leave types: {ex.Message}", true);
            }
        }

        public async Task<Response<LeaveTypeDto>> GetLeaveTypeAsync(int id)
        {
            try
            {
                LeaveType? entity = await _leaveTypeRepo.GetByIdAsync(id);
                if (entity is null)
                    return new Response<LeaveTypeDto>(null, "LeaveType not found", true);

                LeaveTypeDto dto = _mapper.Map<LeaveTypeDto>(entity);
                return new Response<LeaveTypeDto>(dto, null, false);
            }
            catch (Exception ex)
            {
                return new Response<LeaveTypeDto>(null, $"Failed to get leave type: {ex.Message}", true);
            }
        }

        public async Task<Response<bool>> DeleteLeaveTypeAsync(int id)
        {
            try
            {
                await _leaveTypeRepo.DeleteAsync(id);
                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, $"Failed to delete leave type: {ex.Message}", true);
            }
        }

        // ========================= Balances =========================

        public async Task<Response<EmployeeLeaveBalanceDto>> GetEmployeeBalanceAsync(int employeeId, int leaveTypeId, int year)
        {
            try
            {
                IEnumerable<EmployeeLeaveBalance> list = await _leaveBalanceRepo.GetByEmployeeIdAndYearAsync(employeeId, year);
                EmployeeLeaveBalance? bal = list.FirstOrDefault(b => b.LeaveTypeId == leaveTypeId);

                if (bal is null)
                    return new Response<EmployeeLeaveBalanceDto>(null, "Balance not found", true);

                EmployeeLeaveBalanceDto dto = _mapper.Map<EmployeeLeaveBalanceDto>(bal);
                return new Response<EmployeeLeaveBalanceDto>(dto, null, false);
            }
            catch (Exception ex)
            {
                return new Response<EmployeeLeaveBalanceDto>(null, $"Failed to get balance: {ex.Message}", true);
            }
        }
        
         public async Task<Response<int>> AllocateAnnualBalancesAsync(int year)
        {
            try
            {
                List<LeaveType> leaveTypes = (await _leaveTypeRepo.GetAllAsync()).ToList();
            //ToList
                List<Employee> employees = (await _employeeRepo.GetAllAsync()).ToList();

                int created = 0;

                foreach (var emp in employees)
                {
                    var empYearBalances = (await _leaveBalanceRepo.GetByEmployeeIdAndYearAsync(emp.Id, year)).ToList();

                    foreach (var lt in leaveTypes)
                    {
                        bool exists = empYearBalances.Any(x => x.LeaveTypeId == lt.Id);
                        if (!exists)
                        {
                            await _leaveBalanceRepo.AddAsync(new EmployeeLeaveBalance
                            {
                                EmployeeId = emp.Id,
                                LeaveTypeId = lt.Id,
                                Year = year,
                                TotalAllocated = lt.MaxDays,
                                UsedDays = 0,
                                RemainingDays = lt.MaxDays,
                                LastUpdated = DateTime.UtcNow
                            });
                            created++;
                        }
                    }
                }

                return new Response<int>(created, null, false);
            }
            catch (Exception ex)
            {
                return new Response<int>(0, $"Failed to allocate balances: {ex.Message}", true);
            }
        }

        // ========================= Requests =========================
     
        public async Task<Response<int>> RequestLeaveAsync(CreateLeaveRequestDto dto, int currentYear)
        {
            try
            {
                DateTime start = dto.StartDate.Date;
                DateTime end = dto.EndDate.Date;

                if (start > end)
                    return new Response<int>(0, "Start date cannot be after end date.", true);

                Response<bool> overlap = await HasOverlapAsync(dto.EmployeeId, start, end);
                if (overlap.Data == true && !overlap.HasError)
                    return new Response<int>(0, "Overlapping leave request exists.", true);
                if (overlap.HasError)
                    return new Response<int>(0, overlap.ErrorMessage, true);

                int totalDays = (int)(end - start).TotalDays + 1;

                LeaveRequest req = _mapper.Map<LeaveRequest>(dto);
                req.StartDate = start;
                req.EndDate = end;
                req.TotalDays = totalDays;
                req.Status = LeaveStatus.Pending;
                req.CreatedAt = DateTime.UtcNow;

                await _leaveRequestRepo.AddAsync(req);

                List<(int ApproverId, LevelApproval Level)> approvers = await ResolveApproversAsync(dto.EmployeeId);
                foreach ((int ApproverId, LevelApproval Level) step in approvers)
                {
                LeaveApproval approval = new LeaveApproval
                    {
                        LeaveRequestId = req.Id,
                        ApproverId = step.ApproverId,
                        Level = step.Level,        
                        Status = LeaveStatus.Pending,
                        ActionDate = null
                    };
                    await _leaveApprovalRepo.AddAsync(approval);
                }

                return new Response<int>(req.Id, null, false);
            }
            catch (Exception ex)
            {
                return new Response<int>(0, $"Failed to create leave request: {ex.Message}", true);
            }
        }

        public async Task<Response<List<LeaveRequestDto>>> GetEmployeeRequestsAsync(int employeeId)
        {
            try
            {
            IEnumerable<LeaveRequest> list = await _leaveRequestRepo.GetAllByEmployeeIdAsync(employeeId);
            List<LeaveRequestDto> dtos = _mapper.Map<List<LeaveRequestDto>>(list);
                return new Response<List<LeaveRequestDto>>(dtos, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<LeaveRequestDto>>(null, $"Failed to load requests: {ex.Message}", true);
            }
        }

        public async Task<Response<List<LeaveRequestDto>>> GetPendingApprovalsAsync(int approverId)
        {
            try
            {
            IEnumerable<LeaveApproval> steps = await _leaveApprovalRepo.GetByApproverIdAsync(approverId, default);

            List<int> pendingReqIds = steps
                    .Where(a => a.ActionDate == null)
                    .Select(a => a.LeaveRequestId)
                    .Distinct()
                    .ToList();

            List<LeaveRequest> result = new List<LeaveRequest>();
                foreach (var id in pendingReqIds)
                {
                    var req = await _leaveRequestRepo.GetByIdAsync(id);
                    if (req != null)
                        result.Add(req);
                }

            List<LeaveRequestDto> dtos = _mapper.Map<List<LeaveRequestDto>>(result);
                return new Response<List<LeaveRequestDto>>(dtos, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<LeaveRequestDto>>(null, $"Failed to load pending approvals: {ex.Message}", true);
            }
        }

        // ========================= Approvals =========================

        public async Task<Response<bool>> ApproveAsync(LeaveApprovalActionDto dto)
        {
            try
            {
            List<LeaveApproval> allSteps = (await _leaveApprovalRepo.GetAllAsync())
                               .Where(a => a.LeaveRequestId == dto.LeaveRequestId)
                               .ToList();

            LeaveApproval? approval = allSteps.FirstOrDefault(a =>
                    a.LeaveRequestId == dto.LeaveRequestId &&
                    a.ApproverId == dto.ApproverId &&
                    a.Level == dto.Level &&
                    a.ActionDate == null);

                if (approval is null)
                    return new Response<bool>(false, "Approval step not found or already processed", true);

                approval.ActionDate = DateTime.UtcNow;
                await _leaveApprovalRepo.UpdateAsync(approval);

            bool stillPending = allSteps.Any(a => a.ActionDate == null);
                if (stillPending)
                    return new Response<bool>(true, null, false); 

            LeaveRequest? req = await _leaveRequestRepo.GetByIdAsync(dto.LeaveRequestId);
                if (req is null)
                    return new Response<bool>(false, "Leave request not found", true);

            List<EmployeeLeaveBalance> balances = (await _leaveBalanceRepo.GetByEmployeeIdAndYearAsync(req.EmployeeId, req.StartDate.Year)).ToList();
            EmployeeLeaveBalance? bal = balances.FirstOrDefault(b => b.LeaveTypeId == req.LeaveTypeId);
                if (bal is null)
                    return new Response<bool>(false, "Balance not found for employee/year", true);

                if (bal.RemainingDays < req.TotalDays)
                    return new Response<bool>(false, "Insufficient leave balance", true);

                bal.UsedDays += req.TotalDays;
                bal.RemainingDays -= req.TotalDays;
                bal.LastUpdated = DateTime.UtcNow;

                req.Status = LeaveStatus.Approved;
                req.ReviewedAt = DateTime.UtcNow;
                req.ReviewedById = dto.ApproverId;

                await _leaveBalanceRepo.UpdateAsync(bal);
                await _leaveRequestRepo.UpdateAsync(req);

                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, $"Failed to approve: {ex.Message}", true);
            }
        }

        public async Task<Response<bool>> RejectAsync(LeaveApprovalActionDto dto)
        {
            try
            {
            List<LeaveApproval> allSteps = (await _leaveApprovalRepo.GetAllAsync())
                               .Where(a => a.LeaveRequestId == dto.LeaveRequestId)
                               .ToList();

            LeaveApproval? approval = allSteps.FirstOrDefault(a =>
                    a.LeaveRequestId == dto.LeaveRequestId &&
                    a.ApproverId == dto.ApproverId &&
                    a.Level == dto.Level &&
                    a.ActionDate == null);

                if (approval is null)
                    return new Response<bool>(false, "Approval step not found or already processed", true);

                approval.ActionDate = DateTime.UtcNow;
                await _leaveApprovalRepo.UpdateAsync(approval);

                var req = await _leaveRequestRepo.GetByIdAsync(dto.LeaveRequestId);
                if (req is null)
                    return new Response<bool>(false, "Leave request not found", true);

                req.Status = LeaveStatus.Rejected;
                req.ReviewedAt = DateTime.UtcNow;
                req.ReviewedById = dto.ApproverId;

                await _leaveRequestRepo.UpdateAsync(req);

                foreach (var step in allSteps.Where(a => a.ActionDate == null))
                {
                    step.ActionDate = DateTime.UtcNow;
                    await _leaveApprovalRepo.UpdateAsync(step);
                }

                return new Response<bool>(true, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, $"Failed to reject: {ex.Message}", true);
            }
        }

        // ========================= Utilities =========================

        public async Task<Response<bool>> HasOverlapAsync(int employeeId, DateTime start, DateTime end)
        {
            try
            {
            IEnumerable<LeaveRequest> all = await _leaveRequestRepo.GetAllAsync();
            DateTime s = start.Date;
            DateTime e = end.Date;

            bool overlapped = all.Any(r =>
                    r.EmployeeId == employeeId &&
                    r.Status != LeaveStatus.Rejected &&
                    r.StartDate <= e && r.EndDate >= s);

                return new Response<bool>(overlapped, null, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, $"Failed to check overlap: {ex.Message}", true);
            }
        }

    
        private async Task<List<(int ApproverId, LevelApproval Level)>> ResolveApproversAsync(int employeeId)
    {
        List<(int, LevelApproval)> result = new List<(int, LevelApproval)>();

        //Response
        Employee emp = await _employeeRepo.GetByIdAsync(employeeId)
                  ?? throw new KeyNotFoundException("Employee not found");

        // Manager only
        if (emp.DepartmentId is not null)
        {
            //Response
            Department? dept = await _departmentRepo.GetByIdAsync(emp.DepartmentId.Value);

            if (dept?.ManagerId is not null && dept.ManagerId.Value != employeeId)
            {
                result.Add((dept.ManagerId.Value, LevelApproval.Manager));
            }
        }

        return result;
    }

}
