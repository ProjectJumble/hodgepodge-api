using Hodgepodge.Data.Models;
using System;

namespace Hodgepodge.Service
{
    public interface IResourceService
    {
        Resource GetStatic(
            Uri uri,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase);
    }
}
