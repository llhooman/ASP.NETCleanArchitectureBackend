using GymManagement.Domain.Admins;
using GymManagement.Domain.Gyms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Common.Interfaces
{
    public interface IGymsRepository
    {
        Task AddGymAsync(Gym gym);
        Task<Gym?> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(Guid id);
        Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId);
        Task UpdateGymAsync(Gym gym);
        Task RemoveGymAsync(Gym gym);
        Task RemoveRangeAsync(List<Gym> gyms);
    }
}
