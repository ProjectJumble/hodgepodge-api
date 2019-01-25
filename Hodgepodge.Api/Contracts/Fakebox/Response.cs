namespace Hodgepodge.Api.Contracts.Fakebox
{
    public class Response
    {
        public Content Content { get; set; }
        public Domain Domain { get; set; }
        public Title Title { get; set; }
        public bool Success { get; set; }
    }

    public class Content
    {
        public string Decision { get; set; }
        public float Score { get; set; }
    }

    public class Domain
    {
        public string Category { get; set; }
    }

    public class Title
    {
        public string Decision { get; set; }
        public float Score { get; set; }
    }
}
