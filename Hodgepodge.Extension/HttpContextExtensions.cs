namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextExtensions
    {
        public static string RemoteIpAddress(this HttpContext httpContext)
        {
            if (httpContext.Request.Headers.ContainsKey("CF-Connecting-IP"))
            {
                if (httpContext.Request.Headers.TryGetValue("CF-Connecting-IP", out var value))
                {
                    return value;
                }
            }

            if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                if (httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var value))
                {
                    return value;
                }
            }

            return httpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
