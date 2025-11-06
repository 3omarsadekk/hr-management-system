using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class LeaveTypeRepository(ApplicationDbContext _context) : Repository<LeaveType>(_context), ILeaveTypeRepository
{
}
