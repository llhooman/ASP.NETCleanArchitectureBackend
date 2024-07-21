using GymManagement.Domain.Common;
using GymManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Common.Middleware
{
    public class EventualConsistencyMiddleware
    {
        private readonly RequestDelegate _next;
        public EventualConsistencyMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,IPublisher publisher,GymManagementDbContext dbContext)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            context.Response.OnCompleted(async () =>
            {
                try
                {
                    if (context.Items.TryGetValue("DomainEventsQueue", out var value) &&
                        value is Queue<IDomainEvent> domainEventsQueue)
                    {
                        while (domainEventsQueue!.TryDequeue(out var domainEvent))
                        {
                            await publisher.Publish(domainEvent);
                        }

                    }
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    //notify the user that even though they got a successful response
                    //it was not successful due to unhandled error
                }
                finally
                {
                    //(in case of error) if you despose without commiting
                    //the changes transaction will be rolled back
                    await transaction.DisposeAsync();
                }

            }
            );
            await _next(context);
        }
    }
}
