namespace HRManagementSystem.Application.Services;

public class EmployeeDeductionService(
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IEmployeeDeductionService
{
    public async Task<Response<IEnumerable<EmployeeDeductionDto>>> GetAllAsync()
    {
        try
        {
            var employeeDeductions = await _unitOfWork.EmployeeDeductions.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<EmployeeDeductionDto>>(employeeDeductions);
            return new Response<IEnumerable<EmployeeDeductionDto>>(dtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeDeductionDto>>(null!, ex.Message, true);
        }
    }

    public async Task<Response<EmployeeDeductionDto>> GetByCompositeKeyAsync(int employeeId, int deductionId)
    {
        try
        {
            var employeeDeduction = await _unitOfWork.EmployeeDeductions.GetByCompositeKeyAsync(employeeId, deductionId);
            if (employeeDeduction == null)
                return new Response<EmployeeDeductionDto>(null!, "Employee deduction not found", true);

            var dto = _mapper.Map<EmployeeDeductionDto>(employeeDeduction);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null!, ex.Message, true);
        }
    }
    public async Task<Response<IEnumerable<EmployeeDeductionWithDetailsDto>>> GetByEmployeeIdAsync(int employeeId)
    {
        try
        {
            
            var deductions = await _unitOfWork.EmployeeDeductions.GetByEmployeeIdAsync(employeeId);

           
            var dtos = _mapper.Map<IEnumerable<EmployeeDeductionWithDetailsDto>>(deductions);

            return new Response<IEnumerable<EmployeeDeductionWithDetailsDto>>(dtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeDeductionWithDetailsDto>>(
                null!,
                $"Error occurred while retrieving deductions for employee: {ex.Message}",
                true
            );
        }
    }


    public async Task<Response<EmployeeDeductionDto>> CreateAsync(CreateEmployeeDeductionDto request)
    {
        try
        {
            switch (request.Recurrence)
            {
                case RecurrenceType.OneTime:
                case RecurrenceType.Annual:
                case RecurrenceType.Permanent:
                    if (request.StartDate == null)
                        return new Response<EmployeeDeductionDto>(null!, "Start date is required.", true);
                    request.EndDate = null;
                    break;

                case RecurrenceType.Period:
                    if (request.StartDate == null || request.EndDate == null)
                        return new Response<EmployeeDeductionDto>(
                            null!,
                            "Start and end dates are required.",
                            true
                        );

                    if (request.EndDate <= request.StartDate)
                        return new Response<EmployeeDeductionDto>(
                            null!,
                            "End date must be greater than start date.",
                            true
                        );

                    if (request.StartDate.Value.Month == request.EndDate.Value.Month &&
                        request.StartDate.Value.Year == request.EndDate.Value.Year)
                        return new Response<EmployeeDeductionDto>(
                            null!,
                            "Start and end dates cannot fall within the same month and year.",
                            true
                        );
                    break;
                default:
                    return new Response<EmployeeDeductionDto>(
                        null!,
                        "Invalid recurrence type.",
                        true
                    );
            }

            var existing = await _unitOfWork.EmployeeDeductions.GetByCompositeKeyAsync(request.EmployeeId, request.DeductionId);
            if (existing != null)
                return new Response<EmployeeDeductionDto>(null!, "Employee already has this deduction assigned.", true);

            var entity = _mapper.Map<EmployeeDeduction>(request);
            entity.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.EmployeeDeductions.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<EmployeeDeductionDto>(entity);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null!, ex.Message, true);
        }
    }


    public async Task<Response<EmployeeDeductionDto>> UpdateAsync(
        int employeeId,
        int deductionId,
        UpdateEmployeeDeductionDto request)
    {
        try
        {
            var employeeDeduction = await _unitOfWork.EmployeeDeductions
                .GetByCompositeKeyAsync(employeeId, deductionId);

            if (employeeDeduction == null)
                return new Response<EmployeeDeductionDto>(null!, "Employee deduction not found", true);

            if (request.Recurrence.HasValue)
            {
                employeeDeduction.Recurrence = request.Recurrence.Value;

                switch (request.Recurrence.Value)
                {
                    case RecurrenceType.OneTime:
                    case RecurrenceType.Annual:
                    case RecurrenceType.Permanent:
                        if (!request.StartDate.HasValue)
                            return new Response<EmployeeDeductionDto>(null!, "Start date is required", true);

                        employeeDeduction.StartDate = request.StartDate;
                        employeeDeduction.EndDate = null;
                        break;

                        break;

                    case RecurrenceType.Period:
          
                        if (request.StartDate.HasValue && request.EndDate.HasValue)
                        {
                            if (request.EndDate <= request.StartDate)
                                return new Response<EmployeeDeductionDto>(null!, "End date must be greater than start date.", true);

                            employeeDeduction.StartDate = request.StartDate;
                            employeeDeduction.EndDate = request.EndDate;
                        }
                        break;

                    default:
                        return new Response<EmployeeDeductionDto>(
                            null!,
                            "Invalid recurrence type.",
                            true
                        );
                }
            }
            else
            {
                
                if (request.StartDate.HasValue)
                    employeeDeduction.StartDate = request.StartDate;
                if (request.EndDate.HasValue)
                    employeeDeduction.EndDate = request.EndDate;
            }


            if (request.Amount is not null)
            {
                employeeDeduction.Amount = request.Amount;
            }
            else
            {
                employeeDeduction.Amount = null;
            }

            if (request.IsPercentage is not null)
            {
                employeeDeduction.IsPercentage = request.IsPercentage;
            }
            else
            {
                employeeDeduction.IsPercentage = null;
            }


            if (request.DeductionId.HasValue && request.DeductionId.Value != deductionId)
            {
                var duplicate = await _unitOfWork.EmployeeDeductions
                    .GetByCompositeKeyAsync(employeeId, request.DeductionId.Value);

                if (duplicate != null)
                    return new Response<EmployeeDeductionDto>(null!, "Employee already has this deduction assigned.", true);

                employeeDeduction.DeductionId = request.DeductionId.Value;
            }


            employeeDeduction.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.EmployeeDeductions.UpdateAsync(employeeDeduction);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<EmployeeDeductionDto>(employeeDeduction);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null!, ex.Message, true);
        }
    }


    public async Task<Response<bool>> DeleteAsync(int employeeId, int deductionId)
    {
        try
        {
            var employeeDeduction = await _unitOfWork.EmployeeDeductions.GetByCompositeKeyAsync(employeeId, deductionId);
            if (employeeDeduction == null)
                return new Response<bool>(false, "Employee deduction not found", true);

            await _unitOfWork.EmployeeDeductions.DeleteAsync(employeeDeduction.EmployeeId, employeeDeduction.DeductionId);
            await _unitOfWork.SaveChangesAsync();

            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, true);
        }
    }
}
