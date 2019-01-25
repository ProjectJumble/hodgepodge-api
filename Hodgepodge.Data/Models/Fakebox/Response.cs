using Newtonsoft.Json;

namespace Hodgepodge.Data.Models.Fakebox
{
    public class Response
    {
        [JsonProperty(PropertyName = "content")]
        public Content Content { get; set; }

        [JsonProperty(PropertyName = "domain")]
        public Domain Domain { get; set; }

        [JsonProperty(PropertyName = "title")]
        public Title Title { get; set; }

        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }
    }

    public class Content
    {
        [JsonProperty(PropertyName = "decision")]
        public string Decision { get; set; }

        [JsonProperty(PropertyName = "score")]
        public float Score { get; set; }
    }

    public class Domain
    {
        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }
    }

    public class Title
    {
        [JsonProperty(PropertyName = "decision")]
        public string Decision { get; set; }

        [JsonProperty(PropertyName = "score")]
        public float Score { get; set; }
    }
}
