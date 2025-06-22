using MeterOrm.Core.Accessor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MeterOrm.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddMeterOrm(this IServiceCollection services)
    {
        services.TryAddSingleton<IMeterContextAccessor, MeterContextAccessor>();
        return services;
    }
}