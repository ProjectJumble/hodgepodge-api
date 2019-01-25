using Hodgepodge.Data.Constants;
using Hodgepodge.Data.DocumentCollectionUtilities;
using Hodgepodge.Data.Models;
using Hodgepodge.Data.Repositories;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hodgepodge.Utility
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ICosmosDbClient _cosmosDbClient;
        private readonly IStaticResourceRepository _staticResourceRepository;

        public DbInitializer(
            ICosmosDbClient cosmosDbClient,
            IStaticResourceRepository staticResourceRepository)
        {
            _cosmosDbClient = cosmosDbClient ??
                throw new ArgumentNullException(nameof(cosmosDbClient));

            _staticResourceRepository = staticResourceRepository ??
                throw new ArgumentNullException(nameof(staticResourceRepository));
        }

        public async Task InitializeAsync()
        {
            // Database
            await _cosmosDbClient
                .CreateDatabaseAsync(Db.Id)
                .ConfigureAwait(false);

            // Resources
            await _cosmosDbClient
                .CreateDocumentCollectionAsync(
                    Db.Id,
                    ResourceDocumentCollection.Resources.Key,
                    ResourceDocumentCollection.ResourceDocumentPartitionKeyDefinition,
                    ResourceDocumentCollection.ResourceDocumentUniqueKeyPolicy)
                .ConfigureAwait(false);

            // Review
            await _cosmosDbClient
                .CreateDocumentCollectionAsync(
                    Db.Id,
                    ReviewDocumentCollection.Reviews.Key,
                    ReviewDocumentCollection.ReviewDocumentPartitionKeyDefinition,
                    ReviewDocumentCollection.ReviewDocumentUniqueKeyPolicy)
                .ConfigureAwait(false);

            // Survey
            await _cosmosDbClient
                .CreateDocumentCollectionAsync(
                    Db.Id,
                    SurveyDocumentCollection.Surveys.Key,
                    SurveyDocumentCollection.SurveyDocumentPartitionKeyDefinition,
                    SurveyDocumentCollection.SurveyDocumentUniqueKeyPolicy)
                .ConfigureAwait(false);

            // Users
            await _cosmosDbClient
                .CreateDocumentCollectionAsync(
                    Db.Id,
                    UserDocumentCollection.Users.Key,
                    UserDocumentCollection.UserDocumentPartitionKeyDefinition,
                    UserDocumentCollection.UserDocumentUniqueKeyPolicy)
                .ConfigureAwait(false);
        }

        public async Task SeedAsync()
        {
            var resources = _staticResourceRepository
                .Resources
                .Where(r => r.SeedTimeStamp == default(DateTime));

            await Seed(resources).ConfigureAwait(false);
        }

        public async Task SeedAsync(DateTime seedTimeStamp)
        {
            // Poor man's database migration implementation.
            var resources = _staticResourceRepository
                .Resources
                .Where(r => r.SeedTimeStamp > seedTimeStamp);

            await Seed(resources).ConfigureAwait(false);
        }

        private async Task Seed(IEnumerable<Resource> resources)
        {
            foreach (var resource in resources)
            {
                await _cosmosDbClient
                    .UpsertDocumentAsync(
                        Db.Id,
                        ResourceDocumentCollection.Resources.Key,
                        resource)
                    .ConfigureAwait(false);
            }
        }
    }
}
