using Hodgepodge.Service;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return services
                .AddTransient<IHtmlParserService, HtmlParserService>()
                .AddTransient<IResourceService, ResourceService>();
        }
    }
}
