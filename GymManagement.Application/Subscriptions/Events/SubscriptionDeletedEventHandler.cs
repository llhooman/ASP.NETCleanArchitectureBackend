using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Events
{
    public class SubscriptionDeletedEventHandler : INotificationHandler<SubscriptionDeletedEvent>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SubscriptionDeletedEventHandler(ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionsRepository
                 .GetByIdAsync(notification.SubscriptionId);
            if (subscription is null) 
            {
                //resilient error handling
                throw new InvalidOperationException();
            }
            await _subscriptionsRepository.RemoveSubscriptionAsync(subscription);
            await _unitOfWork.CommitChangesAsync();

        }
    }
}
