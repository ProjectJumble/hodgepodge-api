using Hodgepodge.Data.Enums.Audit;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System;

namespace Hodgepodge.Data.Models
{
    public class Resource : Document, IDocument
    {
        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; }

        [JsonProperty(PropertyName = "label")]
        public Label Label { get; set; }

        [JsonIgnore]
        public DateTime SeedTimeStamp { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
