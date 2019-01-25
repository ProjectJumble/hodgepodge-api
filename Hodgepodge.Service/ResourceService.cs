using Hodgepodge.Data.Models;
using Hodgepodge.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hodgepodge.Service
{
    public class ResourceService : IResourceService
    {
        private readonly IStaticResourceRepository _staticResourceRepository;

        public ResourceService(IStaticResourceRepository staticResourceRepository) =>
            _staticResourceRepository = staticResourceRepository ??
                throw new ArgumentNullException(nameof(staticResourceRepository));

        // TODO:
        // Parse URLs using regular expressions:
        // https://www.cambiaresearch.com/articles/46/parsing-urls-with-regular-expressions-and-the-regex-object
        public Resource GetStatic(
            Uri uri,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            Resource result = null;

            var url = uri.Host.TrimSubdomain() + uri.AbsolutePath;

            var segments = new List<string>();

            int current = 0;

            while (current < url.Length)
            {
                int next = url.IndexOf('/', current);

                if (next == -1)
                {
                    next = url.Length - 1;
                }

                segments.Add(url.Substring(0, next + 1));

                current = next + 1;
            }

            for (var i = segments.Count - 1; i >= 0; i--)
            {
                bool predicate(Resource r) =>
                    string.Equals(
                        r.Url.TrimTrailingSlash(),
                        segments[i].TrimTrailingSlash(),
                        stringComparison);

                var resource = _staticResourceRepository
                    .Resources
                    .SingleOrDefault(predicate);

                if (resource != default(Resource))
                {
                    result = resource;
                    break;
                }
            }

            return result;
        }
    }
}
