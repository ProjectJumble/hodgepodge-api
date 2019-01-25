using Hodgepodge.Data.Constants;
using Microsoft.Azure.Documents;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hodgepodge.Data.DocumentCollectionUtilities
{
    public static class ReviewDocumentCollection
    {
        public static readonly KeyValuePair<string, string> Reviews =
            new KeyValuePair<string, string>("review", "/url");

        public static PartitionKeyDefinition ReviewDocumentPartitionKeyDefinition =>
            new PartitionKeyDefinition
            {
                Paths = new Collection<string> { Db.PartitionKey }
            };

        public static UniqueKeyPolicy ReviewDocumentUniqueKeyPolicy =>
            new UniqueKeyPolicy { UniqueKeys = ReviewDocumentUniqueKeys };

        private static Collection<UniqueKey> ReviewDocumentUniqueKeys =>
            new Collection<UniqueKey>
            {
                new UniqueKey { Paths = ReviewDocumentUniqueKeyPaths }
            };

        private static Collection<string> ReviewDocumentUniqueKeyPaths =>
            new Collection<string> { "/token", "/url" };
    }
}
