using Newtonsoft.Json;

namespace Hodgepodge.Data.Models.Fakebox
{
    public class Request
    {
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
