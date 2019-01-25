using System.Threading.Tasks;

namespace Hodgepodge.Service
{
    public interface IHtmlParserService
    {
        Task<string> ParseAsync(string url);
    }
}
