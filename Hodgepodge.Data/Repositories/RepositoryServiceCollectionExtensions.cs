using Hodgepodge.Data.Repositories;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return services
                .AddTransient<IResourceRepository, ResourceRepository>()
                .AddTransient<IReviewRepository, ReviewRepository>()
                .AddSingleton<IStaticResourceRepository, StaticResourceRepository>()
                .AddTransient<ISurveyRepository, SurveyRepository>()
                .AddTransient<IUserRepository, UserRepository>();
        }
    }
}
