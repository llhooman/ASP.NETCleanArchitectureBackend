using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Commands.DeleteGym
{
    public class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IGymsRepository _gymsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGymCommandHandler(
        ISubscriptionsRepository subscriptionsRepository,
        IGymsRepository gymsRepository, 
        IUnitOfWork unitOfWork
            )
        {
            _subscriptionsRepository = subscriptionsRepository;
            _gymsRepository = gymsRepository;
            _unitOfWork = unitOfWork;
        
        }
        public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand request, CancellationToken cancellationToken)
        {
            var gym = await _gymsRepository.GetByIdAsync( request.GymId );
            if(gym == null )
            {
                return Error.NotFound(description: "Gym not found");

            }
            var subscription = await _subscriptionsRepository.GetByIdAsync( request.SubscriptionId );

            if(subscription == null)
            {
                return Error.NotFound(description: "subscription not found");
            }
            if (!subscription.HasGym(request.GymId))
            {
                return Error.Unexpected(description: "Gym not found");
            }
            subscription.RemoveGym(request.GymId);

            await _subscriptionsRepository.UpdateAsync(subscription);
            await _gymsRepository.RemoveGymAsync(gym);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}
