using CQRSimple.Contracts;
using CQRSimple.Handlers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DispatcherServiceCollection
    {
        public static IServiceCollection AddDispatcher(this IServiceCollection services)
        {
            services.AddScoped<IDispatcher, Dispatcher>();
            return services;
        }
    }
}
