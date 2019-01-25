using Hodgepodge.Data.Constants;
using Microsoft.Azure.Documents;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hodgepodge.Data.DocumentCollectionUtilities
{
    public static class UserDocumentCollection
    {
        public static readonly KeyValuePair<string, string> Users =
            new KeyValuePair<string, string>("users", "/token");

        public static PartitionKeyDefinition UserDocumentPartitionKeyDefinition =>
            new PartitionKeyDefinition
            {
                Paths = new Collection<string> { Db.PartitionKey }
            };

        public static UniqueKeyPolicy UserDocumentUniqueKeyPolicy =>
            new UniqueKeyPolicy { UniqueKeys = UserDocumentUniqueKeys };

        private static Collection<UniqueKey> UserDocumentUniqueKeys =>
            new Collection<UniqueKey>
            {
                new UniqueKey { Paths = UserDocumentUniqueKeyPaths }
            };

        private static Collection<string> UserDocumentUniqueKeyPaths =>
            new Collection<string> { Users.Value };
    }
}
