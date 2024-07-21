
using ErrorOr;
using FluentValidation;
using GymManagement.Application.Common.Behaviours;
using GymManagement.Application.Gyms.Commands.CreateGym;
using GymManagement.Domain.Gyms;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace GymManagement.Application;
public static class DependencyInjection{
    public static IServiceCollection AddApplication(this IServiceCollection services){
        services.AddMediatR(options=>{
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            options.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
        return services;
    }
}