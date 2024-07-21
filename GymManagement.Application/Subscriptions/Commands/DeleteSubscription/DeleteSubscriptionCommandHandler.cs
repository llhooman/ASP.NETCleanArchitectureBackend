﻿using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
    {
        private readonly IAdminsRepository _adminsRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSubscriptionCommandHandler(
            IAdminsRepository adminsRepository,
            ISubscriptionsRepository subscriptionsRepository,
            IUnitOfWork unitOfWork
            )
        {
            _adminsRepository = adminsRepository;
            _subscriptionsRepository = subscriptionsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

            if (subscription is null)
            {
                return Error.NotFound(description: "Subscription not found");
            }

            var admin = await _adminsRepository.GetByIdAsync(subscription.AdminId);

            if (admin is null)
            {
                return Error.Unexpected(description: "Admin not found");
            }

            admin.DeleteSubscription(command.SubscriptionId);


            await _adminsRepository.UpdateAsync(admin);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}
