using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;
public interface IDesignationRepository : IRepository<Designation>
{
    Task<IEnumerable<Designation>> GetDesignationsWithEmployeesAsync(int id, CancellationToken cancellationToken = default);
    Task<Designation> GetByTitleAsync(string name, CancellationToken cancellationToken = default);

}
