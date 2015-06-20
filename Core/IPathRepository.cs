using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furball.Core
{
    public interface IPathRepository
    {
        Task<RequestedPath> GetMethodAsync(string path, string httpMethod, List<RequestParameter> parameters);
    }
}
