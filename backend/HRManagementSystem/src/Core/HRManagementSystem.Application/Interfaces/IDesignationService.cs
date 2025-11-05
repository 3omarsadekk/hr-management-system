namespace HRManagementSystem.Application.Interfaces;
public interface IDesignationService
{
    Task<Response<IEnumerable<DesignationDto>>> GetAllDesignationsAsync(CancellationToken cancellationToken = default);
    Task<Response<DesignationDto>> GetDesignationByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<DesignationDto>> CreateDesignationAsync(CreateDesignationDto createDesignationDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> UpdateDesignationAsync(int id, UpdateDesignationDto updateDesignationDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteDesignationAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<DesignationWithEmployeesDto>> GetDesignationWithEmployeesAsync(int id, CancellationToken cancellationToken = default);

}
