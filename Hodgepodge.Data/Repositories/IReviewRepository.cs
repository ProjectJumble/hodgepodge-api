using Hodgepodge.Data.Models;
using System.Threading.Tasks;

namespace Hodgepodge.Data.Repositories
{
    public interface IReviewRepository
    {
        Task<Microsoft.Azure.Documents.Document> UpsertAsync(Review review);

        Review Get(Review review);
    }
}
