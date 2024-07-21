using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Events
{
    public class SubscriptionDeletedEventHandler : INotificationHandler<SubscriptionDeletedEvent>
    {
        private readonly IGymsRepository _gymsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SubscriptionDeletedEventHandler(IGymsRepository gymsRepository, IUnitOfWork unitOfWork)
        {
            _gymsRepository = gymsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
        {
            var gyms = await _gymsRepository.ListBySubscriptionIdAsync(notification.SubscriptionId);
            await _gymsRepository.RemoveRangeAsync(gyms);
            await _unitOfWork.CommitChangesAsync(); 

        }
    }
}
