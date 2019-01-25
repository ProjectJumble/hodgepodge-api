using Microsoft.Azure.Documents.Client;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CosmosDbServiceCollectionExtensions
    {
        public static IServiceCollection AddCosmosDb(
            this IServiceCollection services,
            Action<CosmosDbOptions> configure)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            services.Configure(configure);
            return services.AddSingleton<ICosmosDbClient, CosmosDbClient>();
        }
    }
}
