using GymManagement.Infrastructure.Common.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure
{
    public static class RequestPipeline
    {
        public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder builder) 
        {
            builder.UseMiddleware<EventualConsistencyMiddleware>();
            return builder;
        
        }
    }
}
