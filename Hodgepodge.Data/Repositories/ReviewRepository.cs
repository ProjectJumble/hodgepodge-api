using Hodgepodge.Data.Constants;
using Hodgepodge.Data.DocumentCollectionUtilities;
using Hodgepodge.Data.Models;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hodgepodge.Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ICosmosDbClient _cosmosDbClient;

        public ReviewRepository(ICosmosDbClient cosmosDbClient) =>
            _cosmosDbClient = cosmosDbClient ??
                throw new ArgumentNullException(nameof(cosmosDbClient));

        public Task<Microsoft.Azure.Documents.Document> UpsertAsync(Review review)
        {
            review.PartitionKey = DateTime.UtcNow.Hour.ToString();

            return _cosmosDbClient
                .UpsertDocumentAsync(
                    Db.Id,
                    ReviewDocumentCollection.Reviews.Key,
                    review);
        }

        public Review Get(Review review) =>
            _cosmosDbClient
                .CreateDocumentQuery<Review>(
                    Db.Id,
                    ReviewDocumentCollection.Reviews.Key)
                .Where(r => r.Token == review.Token && r.Url == review.Url)
                .ToList()
                .FirstOrDefault();
    }
}
