namespace HRManagementSystem.Infrastructure.Repositories;
public class DesignationRepository(ApplicationDbContext _context) : Repository<Designation>(_context), IDesignationRepository
{
    public async Task<Designation> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await _context.Designations
            .FirstOrDefaultAsync(d => d.Title == title, cancellationToken);
    }

    public async Task<IEnumerable<Designation>> GetDesignationsWithEmployeesAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Designations
            .Where(d => d.Id == id)
            .Include(d => d.Employees)
            .ToListAsync(cancellationToken);
    }
}
