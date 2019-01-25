using Hodgepodge.Data.Models;
using System.Collections.Generic;

namespace Hodgepodge.Data.Repositories
{
    public interface IStaticResourceRepository
    {
        IList<Resource> Resources { get; }
    }
}
