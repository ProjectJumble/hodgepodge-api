using Hodgepodge.Data.Models;
using System.Threading.Tasks;

namespace Hodgepodge.Data.Repositories
{
    public interface IResourceRepository
    {
        Task<Microsoft.Azure.Documents.Document> UpsertAsync(Resource resource);

        Resource Get(string url);
    }
}
