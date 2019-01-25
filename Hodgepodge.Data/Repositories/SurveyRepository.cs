using Hodgepodge.Data.Constants;
using Hodgepodge.Data.DocumentCollectionUtilities;
using Hodgepodge.Data.Models;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hodgepodge.Data.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly ICosmosDbClient _cosmosDbClient;

        public SurveyRepository(ICosmosDbClient cosmosDbClient) =>
            _cosmosDbClient = cosmosDbClient ??
                throw new ArgumentNullException(nameof(cosmosDbClient));

        public Task<Microsoft.Azure.Documents.Document> UpsertAsync(Survey survey)
        {
            survey.PartitionKey = DateTime.UtcNow.Hour.ToString();

            return _cosmosDbClient
                .UpsertDocumentAsync(
                    Db.Id,
                    SurveyDocumentCollection.Surveys.Key,
                    survey);
        }

        public Survey Get(Survey survey) =>
            _cosmosDbClient
                .CreateDocumentQuery<Survey>(
                    Db.Id,
                    SurveyDocumentCollection.Surveys.Key)
                .Where(s => s.Token == survey.Token)
                .ToList()
                .FirstOrDefault();
    }
}
