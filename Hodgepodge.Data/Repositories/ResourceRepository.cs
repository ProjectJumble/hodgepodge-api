using Hodgepodge.Data.Constants;
using Hodgepodge.Data.DocumentCollectionUtilities;
using Hodgepodge.Data.Models;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hodgepodge.Data.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ICosmosDbClient _cosmosDbClient;

        public ResourceRepository(ICosmosDbClient cosmosDbClient) =>
            _cosmosDbClient = cosmosDbClient ??
                throw new ArgumentNullException(nameof(cosmosDbClient));

        public Task<Microsoft.Azure.Documents.Document> UpsertAsync(Resource resource)
        {
            resource.PartitionKey = DateTime.UtcNow.Hour.ToString();

            return _cosmosDbClient
                .UpsertDocumentAsync(
                    Db.Id,
                    ResourceDocumentCollection.Resources.Key,
                    resource);
        }

        public Resource Get(string url) =>
            _cosmosDbClient
                .CreateDocumentQuery<Resource>(
                    Db.Id,
                    ResourceDocumentCollection.Resources.Key)
                .Where(r => r.Url == url)
                .ToList()
                .FirstOrDefault();
    }
}
