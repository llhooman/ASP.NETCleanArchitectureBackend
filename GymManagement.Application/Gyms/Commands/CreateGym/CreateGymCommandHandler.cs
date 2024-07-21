using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.CreateGym
{
    public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
    {
        private readonly ISubscriptionsRepository _subscriptionRepository;
        private readonly IGymsRepository _gymsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGymCommandHandler(IGymsRepository gymsRepository, IUnitOfWork unitOfWork,ISubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionRepository = subscriptionsRepository;
            _gymsRepository = gymsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Gym>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
        {
            
            var subscription = await _subscriptionRepository.GetByIdAsync(command.SubscriptionId);
            if (subscription == null)
            {
                return Error.NotFound(description: "Subscription not found");
            }

            var gym = new Gym(
                name: command.Name,
                maxRooms: subscription.GetMaxRooms(),
                subscriptionId: subscription.Id);
            var addGymResult = subscription.AddGym(gym);

            if (addGymResult.IsError)
            {
                return addGymResult.Errors;
            }

            await _subscriptionRepository.UpdateAsync(subscription);
            await _gymsRepository.AddGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();
            return gym;
        }
    }
}
