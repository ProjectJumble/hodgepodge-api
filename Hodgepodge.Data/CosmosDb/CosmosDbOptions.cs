namespace Microsoft.Azure.Documents.Client
{
    public class CosmosDbOptions
    {
        public string AuthKey { get; set; }

        // https://docs.microsoft.com/en-us/azure/cosmos-db/set-throughput#setting-throughput-on-a-database
        public bool? ProvisionThroughputForCosmosDbDatabase { get; set; }

        public string ServiceEndpoint { get; set; }
    }
}
