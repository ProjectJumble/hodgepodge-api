using Hodgepodge.Data.Constants;
using Hodgepodge.Data.DocumentCollectionUtilities;
using Hodgepodge.Data.Models;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hodgepodge.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICosmosDbClient _cosmosDbClient;

        public UserRepository(ICosmosDbClient cosmosDbClient) =>
            _cosmosDbClient = cosmosDbClient ??
                throw new ArgumentNullException(nameof(cosmosDbClient));

        public Task<Microsoft.Azure.Documents.Document> UpsertAsync(User user)
        {
            user.PartitionKey = DateTime.UtcNow.Hour.ToString();

            return _cosmosDbClient
                .UpsertDocumentAsync(
                    Db.Id,
                    UserDocumentCollection.Users.Key,
                    user);
        }

        public User Get(Guid token) =>
            _cosmosDbClient
                .CreateDocumentQuery<User>(
                    Db.Id,
                    UserDocumentCollection.Users.Key)
                .Where(u => u.Token == token)
                .ToList()
                .FirstOrDefault();
    }
}
