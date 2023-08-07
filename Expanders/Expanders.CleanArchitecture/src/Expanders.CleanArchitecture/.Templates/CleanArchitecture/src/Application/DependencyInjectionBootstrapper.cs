using Microsoft.Extensions.DependencyInjection;
using NS.Application.Boundaries;
using NS.Application.Interactors;
using NS.Application.Mappers;
using NS.Application.Validators;
using NS.Domain.Entities;
using NS.Domain;

namespace NS.Application
{
    public static class DependencyInjectionBootstrapper
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            return services;
        }
    }
}
