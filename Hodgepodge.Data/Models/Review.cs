using Hodgepodge.Data.Enums.Review;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System;

namespace Hodgepodge.Data.Models
{
    public class Review : Document, IDocument
    {
        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; }

        [JsonProperty(PropertyName = "ideologicalConsistency")]
        public IdeologicalConsistency IdeologicalConsistency { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public Rating Rating { get; set; }

        [JsonProperty(PropertyName = "token")]
        public Guid Token { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
