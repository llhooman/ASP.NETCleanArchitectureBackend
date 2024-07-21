using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Domain.Gyms
{
    public static class GymErrors
    {
        public static readonly Error CannotHaveMoreRoomsThanSubscriptionAllows = Error.Validation(
             "Room.CannotHaveMoreRoomsThanSubscriptionAllows",
             "A gym cannot have more rooms than the subscription allows"
            );
    }
}
