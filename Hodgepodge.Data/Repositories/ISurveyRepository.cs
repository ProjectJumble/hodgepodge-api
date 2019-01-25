using Hodgepodge.Data.Models;
using System.Threading.Tasks;

namespace Hodgepodge.Data.Repositories
{
    public interface ISurveyRepository
    {
        Task<Microsoft.Azure.Documents.Document> UpsertAsync(Survey survey);

        Survey Get(Survey survey);
    }
}
