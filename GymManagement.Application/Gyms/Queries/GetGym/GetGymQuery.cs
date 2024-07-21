using GymManagement.Domain.Gyms;
using MediatR;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Queries.GetGym
{
    public record GetGymQuery(Guid SubscriptionId,Guid GymId):IRequest<ErrorOr<Gym>>;
    
}
