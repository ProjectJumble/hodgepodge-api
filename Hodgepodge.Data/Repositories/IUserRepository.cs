using Hodgepodge.Data.Models;
using System;
using System.Threading.Tasks;

namespace Hodgepodge.Data.Repositories
{
    public interface IUserRepository
    {
        Task<Microsoft.Azure.Documents.Document> UpsertAsync(User user);

        User Get(Guid token);
    }
}
