using Hodgepodge.Data.Constants;
using Microsoft.Azure.Documents;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hodgepodge.Data.DocumentCollectionUtilities
{
    public static class SurveyDocumentCollection
    {
        public static readonly KeyValuePair<string, string> Surveys =
            new KeyValuePair<string, string>("survey", "/url");

        public static PartitionKeyDefinition SurveyDocumentPartitionKeyDefinition =>
            new PartitionKeyDefinition
            {
                Paths = new Collection<string> { Db.PartitionKey }
            };

        public static UniqueKeyPolicy SurveyDocumentUniqueKeyPolicy =>
            new UniqueKeyPolicy { UniqueKeys = SurveyDocumentUniqueKeys };

        private static Collection<UniqueKey> SurveyDocumentUniqueKeys =>
            new Collection<UniqueKey>
            {
                new UniqueKey { Paths = SurveyDocumentUniqueKeyPaths }
            };

        private static Collection<string> SurveyDocumentUniqueKeyPaths =>
            new Collection<string> { "/token", "/url" };
    }
}
