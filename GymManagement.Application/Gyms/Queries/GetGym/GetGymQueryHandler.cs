﻿using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Gyms.Queries.GetGym
{
    public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
    {
        private readonly IGymsRepository _gymsRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public GetGymQueryHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository)
        {
            _gymsRepository = gymsRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }
        public async Task<ErrorOr<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
        {
            if(await _subscriptionsRepository.ExistsAsync(request.SubscriptionId))
            {
                return Error.NotFound("Subscription not found");
            }
            if(await _gymsRepository.GetByIdAsync(request.GymId) is not Gym gym) {
                return Error.NotFound(description: "Gym not found");
            }
            return gym;
        }
    }
}
