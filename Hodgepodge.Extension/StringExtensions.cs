namespace System
{
    public static class StringExtensions
    {
        public static string TrimSubdomain(this string host)
        {
            if (host.StartsWith("www.", StringComparison.InvariantCultureIgnoreCase))
                host = host.Replace("www.", null);

            return host;
        }

        public static string AbsolutePath(this string input)
        {
            var occurrence = input.IndexOf('/');
            if (occurrence > -1)
            {
                var last = input.Length;
                input = input.Substring(occurrence, last - occurrence);
            }
            return input;
        }

        public static string TrimTrailingSlash(this string input)
        {
            var occurrence = input.LastIndexOf('/');
            if (occurrence > 0)
            {
                input = input.TrimEnd('/');
            }
            return input;
        }
    }
}
