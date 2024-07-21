using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Subscriptions.Queries.GetSubscription
{
    public record GetSubscriptionQuery(Guid SubscriptionId):IRequest<ErrorOr<Subscription>>;
}

