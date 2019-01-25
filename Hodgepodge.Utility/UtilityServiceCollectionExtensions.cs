using Hodgepodge.Utility;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UtilityServiceCollectionExtensions
    {
        public static IServiceCollection AddUtilities(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return services.AddTransient<IDbInitializer, DbInitializer>();
        }
    }
}
