using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System;

namespace Hodgepodge.Data.Models
{
    public class Survey : Document, IDocument
    {
        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; }

        [JsonProperty(PropertyName = "token")]
        public Guid Token { get; set; }
    }
}
