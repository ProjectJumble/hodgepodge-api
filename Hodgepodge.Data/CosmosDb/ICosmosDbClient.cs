using Hodgepodge.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Azure.Documents.Client
{
    public interface ICosmosDbClient
    {
        Task<Database> CreateDatabaseAsync(string databaseId);

        Task<DocumentCollection> CreateDocumentCollectionAsync(
            string databaseId,
            string documentCollectionId,
            PartitionKeyDefinition partitionKeyDefinition,
            UniqueKeyPolicy uniqueKeyPolicy);

        Task<Document> UpsertDocumentAsync(
            string databaseId,
            string documentCollectionId,
            object document);

        IQueryable<T> CreateDocumentQuery<T>(
            string databaseId,
            string documentCollectionId) where T : IDocument;
    }
}
