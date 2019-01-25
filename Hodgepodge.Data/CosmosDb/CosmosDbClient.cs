using Hodgepodge.Data.Models;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Azure.Documents.Client
{
    public class CosmosDbClient : ICosmosDbClient
    {
        private readonly CosmosDbOptions _options;
        private readonly DocumentClient _documentClient;
        private readonly FeedOptions _feedOptions;
        private readonly RequestOptions _requestOptions;

        public CosmosDbClient(IOptions<CosmosDbOptions> options)
        {
            _options = options.Value
                ?? throw new ArgumentNullException(nameof(options));

            var connectionPolicy = new ConnectionPolicy
            {
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp
            };

            _documentClient = new DocumentClient(
                new Uri(_options.ServiceEndpoint), _options.AuthKey, connectionPolicy);

            _feedOptions = new FeedOptions { EnableCrossPartitionQuery = true };

            if (_options.ProvisionThroughputForCosmosDbDatabase == true)
                _requestOptions = new RequestOptions { OfferThroughput = 400 };
        }

        public async Task<Database> CreateDatabaseAsync(string databaseId) =>
            await _documentClient
                .CreateDatabaseIfNotExistsAsync(
                    new Database { Id = databaseId },
                    _requestOptions)
                .ConfigureAwait(false);

        public async Task<DocumentCollection> CreateDocumentCollectionAsync(
            string databaseId,
            string documentCollectionId,
            PartitionKeyDefinition partitionKeyDefinition,
            UniqueKeyPolicy uniqueKeyPolicy)
        {
            var documentCollection = new DocumentCollection
            {
                Id = documentCollectionId,
                PartitionKey = partitionKeyDefinition,
                UniqueKeyPolicy = uniqueKeyPolicy
            };

            return await _documentClient
                .CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(databaseId),
                    documentCollection)
                .ConfigureAwait(false);
        }

        public async Task<Document> UpsertDocumentAsync(
            string databaseId,
            string documentCollectionId,
            object document) => await _documentClient
                .UpsertDocumentAsync(
                    UriFactory.CreateDocumentCollectionUri(
                        databaseId,
                        documentCollectionId),
                    document)
                .ConfigureAwait(false);

        public IQueryable<T> CreateDocumentQuery<T>(
            string databaseId,
            string documentCollectionId) where T : IDocument =>
            _documentClient
                .CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(
                        databaseId,
                        documentCollectionId),
                    _feedOptions);
    }
}
