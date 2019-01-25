using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using System;

namespace Hodgepodge.Data.Models
{
    public class User : Document, IDocument
    {
        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; }

        [JsonProperty(PropertyName = "banned")]
        public bool Banned { get; set; }

        [JsonProperty(PropertyName = "lastSeenTimeStamp")]
        public DateTime LastSeenTimeStamp { get; set; }

        [JsonProperty(PropertyName = "remoteIpAddress")]
        public string RemoteIpAddress { get; set; }

        [JsonProperty(PropertyName = "token")]
        public Guid Token { get; set; }
    }
}
