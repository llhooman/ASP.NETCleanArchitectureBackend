using ErrorOr;
using FluentAssertions;
using GymManagement.Domain.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Gyms;
using TestCommon.Subscriptions;

namespace GymManagement.Domain.UnitTests.Subscriptions
{
    public class SubscriptionTests
    {
        [Fact]
        public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
        {
            //Arange
            //create a subscription
            var subscription = SubscriptionFactory.CreateSubscription();
            //create the maximum number of gyms + 1
            var gyms = Enumerable.Range(0,subscription.GetMaxGyms()+1)
                .Select(_=>GymFactory.CreateGym(id:Guid.NewGuid())).ToList();
            //Act
            var addGymResults = gyms.ConvertAll(subscription.AddGym);
            //Assert
            var allButLastGymResult = addGymResults.Take(addGymResults.Count
                                                         - 1).ToArray();
            allButLastGymResult.Should().AllSatisfy(addGymResult => addGymResult.Value.Should().Be(Result.Success));
            var lastAddGymResult = addGymResults.Last();
            lastAddGymResult.IsError.Should().BeTrue();
            lastAddGymResult.FirstError.Should()
                .Be(SubscriptionErrors.CannotHaveMoreGymsThanTheSubscriptionAllows);



        }

    }
}
