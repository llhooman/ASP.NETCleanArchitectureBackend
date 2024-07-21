using GymManagement.Domain.Gyms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.TestConstants;

namespace TestCommon.Gyms
{
    public static class GymFactory
    {
        public static Gym CreateGym(
            string name = Constants.Gym.Name,
            int maxRooms = Constants.Subscription.MaxRoomsFreeTier,
            Guid? id = null
            )
        {
            return new Gym(
                name,
                maxRooms,
                subscriptionId: Constants.Subscription.Id,
                id: id ?? Constants.Gym.Id
                );
        } 
    }
}
