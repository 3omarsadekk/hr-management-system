namespace HRManagementSystem.Application.Services.LeaveServices;
public class LeaveTypeService(ILeaveTypeRepository _leaveTypeRepository, IMapper _mapper) : ILeaveTypeService
{
    public async Task<Response<IEnumerable<LeaveTypeDto>>> GetAllLeaveTypesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<LeaveType> types = await _leaveTypeRepository.GetAllAsync();
            IEnumerable<LeaveTypeDto> leaveTypeDtos = _mapper.Map<List<LeaveTypeDto>>(types);
            return new Response<IEnumerable<LeaveTypeDto>>(leaveTypeDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<LeaveTypeDto>>(null, $"Failed to load leaveTypes: {ex.Message}", true);
        }
    }

    public async Task<Response<LeaveTypeDto>> GetLeaveTypeByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            LeaveType? entity = await _leaveTypeRepository.GetByIdAsync(id);
            if (entity is null)
                return new Response<LeaveTypeDto>(default!, "LeaveType not found", true);

            LeaveTypeDto dto = _mapper.Map<LeaveTypeDto>(entity);
            return new Response<LeaveTypeDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<LeaveTypeDto>(default!, $"Failed to get leaveType: {ex.Message}", true);
        }
    }
}
