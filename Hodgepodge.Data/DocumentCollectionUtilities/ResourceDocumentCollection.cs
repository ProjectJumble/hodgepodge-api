using Hodgepodge.Data.Constants;
using Microsoft.Azure.Documents;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hodgepodge.Data.DocumentCollectionUtilities
{
    public static class ResourceDocumentCollection
    {
        public static readonly KeyValuePair<string, string> Resources =
            new KeyValuePair<string, string>("resources", "/url");

        public static PartitionKeyDefinition ResourceDocumentPartitionKeyDefinition =>
            new PartitionKeyDefinition
            {
                Paths = new Collection<string> { Db.PartitionKey }
            };

        public static UniqueKeyPolicy ResourceDocumentUniqueKeyPolicy =>
            new UniqueKeyPolicy { UniqueKeys = ResourceDocumentUniqueKeys };

        private static Collection<UniqueKey> ResourceDocumentUniqueKeys =>
            new Collection<UniqueKey>
            {
                new UniqueKey { Paths = ResourceDocumentUniqueKeyPaths }
            };

        private static Collection<string> ResourceDocumentUniqueKeyPaths =>
            new Collection<string> { Resources.Value };
    }
}
