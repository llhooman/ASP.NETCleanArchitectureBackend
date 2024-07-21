using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Admins.Persistence
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly GymManagementDbContext _dbContext;
        public AdminsRepository(GymManagementDbContext dbContext) { 
            _dbContext = dbContext;
        }
        public async Task<Admin?> GetByIdAsync(Guid adminId)
        {
            return await _dbContext.Admins
                .FirstOrDefaultAsync(admin=> admin.Id == adminId);
        }

        public Task UpdateAsync(Admin admin)
        {
            _dbContext.Admins.Update(admin);

            return Task.CompletedTask;
        }
    }
}
