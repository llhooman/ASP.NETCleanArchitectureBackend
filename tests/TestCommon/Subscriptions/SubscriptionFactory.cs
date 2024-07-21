using GymManagement.Domain.Subscriptions;
using TestCommon.TestConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommon.Subscriptions
{
    public class SubscriptionFactory
    {
        public static Subscription CreateSubscription(
            SubscriptionType? subscriptionType = null,
            Guid? adminId = null,
            Guid? id = null
            )
        {
            return new Subscription(
                subscriptionType: subscriptionType ?? Constants.Subscription.DefaultSubscriptionType,
                adminId: adminId ?? Constants.Admin.Id,
                id: id ?? Constants.Subscription.Id
                );
        }
    }
}
