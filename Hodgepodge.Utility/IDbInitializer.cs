using System;
using System.Threading.Tasks;

namespace Hodgepodge.Utility
{
    public interface IDbInitializer
    {
        Task InitializeAsync();

        Task SeedAsync();

        Task SeedAsync(DateTime seedTimeStamp);
    }
}
