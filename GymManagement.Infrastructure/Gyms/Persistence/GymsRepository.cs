using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Gyms.Persistence
{
    public class GymsRepository : IGymsRepository
    {
        private readonly GymManagementDbContext _dbcontext;
        public GymsRepository(GymManagementDbContext dbcontext) {
            _dbcontext = dbcontext;
        
        }
        public async Task AddGymAsync(Gym gym)
        {
            await _dbcontext.Gyms.AddAsync(gym);
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dbcontext.Gyms.AsNoTracking().AnyAsync(gym=>gym.Id == id);
        }

        public async Task<Gym?> GetByIdAsync(Guid id)
        {
            return await _dbcontext.Gyms.FirstOrDefaultAsync(gym => gym.Id == id);
        }


        public async Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await _dbcontext.Gyms.Where(gym=>gym.SubscriptionId == subscriptionId).ToListAsync();
        }

        public Task RemoveGymAsync(Gym gym)
        {
            _dbcontext.Remove(gym);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(List<Gym> gyms)
        {
            _dbcontext.RemoveRange(gyms);
            return Task.CompletedTask;
        }

        public Task UpdateGymAsync(Gym gym)
        {
            _dbcontext.Update(gym);
            return Task.CompletedTask;
        }
    }
}
