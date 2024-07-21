using ErrorOr;
using GymManagement.Domain.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.CreateGym
{
    public record CreateGymCommand(string Name,Guid SubscriptionId):IRequest<ErrorOr<Gym>>;
    
}
