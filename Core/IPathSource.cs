using System.Collections.Generic;

namespace Furball.Core
{
    public interface IPathSource
    {
        List<Path> GetPaths();
    }
}
