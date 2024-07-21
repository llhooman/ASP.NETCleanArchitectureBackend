using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Contracts.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using GymManagement.Application.Subscriptions.Queries.GetSubscription;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;
using GymManagement.Application.Subscriptions.Commands.DeleteSubscription;
namespace GymManagement.Api.Controllers;

[Route("[controller]")]
public class SubscriptionsController:ApiController
{
    
    private readonly IMediator _mediator;
    public SubscriptionsController(IMediator mediator){
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request){
        if(!DomainSubscriptionType.TryFromName(
            request.SubscriptionType.ToString(),
            out var subscriptionType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid subscription Type"
                );
        }
        var command = new CreateSubscriptionCommand(
            subscriptionType,
            request.AdminId 
        );
        var createSubscriptionResult = await _mediator.Send(command);

        //return createSubscriptionResult.MatchFirst(
        //    subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
        //    error => Problem());

        return createSubscriptionResult.Match(
            subscription => CreatedAtAction(
                nameof(GetSubscription),
                new { subscriptionId = subscription.Id },
                new SubscriptionResponse(
                    subscription.Id,
                    ToDto(subscription.SubscriptionType))),
            Problem);
    }

    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscription(Guid subscriptionId)
    {
        var query = new GetSubscriptionQuery(subscriptionId);

        var getSubscriptionsResult = await _mediator.Send(query);

        return getSubscriptionsResult.MatchFirst(
            subscription => Ok(new SubscriptionResponse(
                subscription.Id,
                Enum.Parse<Contracts.Subscriptions.SubscriptionType>(subscription.SubscriptionType.Name))),
            error => Problem());
    }

    [HttpDelete("{subscriptionId:guid}")]
    public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
    {
        var command = new DeleteSubscriptionCommand(subscriptionId);

        var deleteSubscriptionResult = await _mediator.Send(command);

        return deleteSubscriptionResult.Match(
            _ => NoContent(),
            Problem
            );
    }

    private static SubscriptionType ToDto(DomainSubscriptionType subscriptionType)
    {
        return subscriptionType.Name switch
        {
            nameof(DomainSubscriptionType.Free) => SubscriptionType.Free,
            nameof(DomainSubscriptionType.Starter) => SubscriptionType.Starter,
            nameof(DomainSubscriptionType.Pro) => SubscriptionType.Pro,
            _ => throw new InvalidOperationException(),
        };
    }
}