using System.Collections.Generic;

namespace Furball.Core
{
    public class FurballOptions
    {
        private IPathRepository _pathRepository;
        private readonly List<string> _headersToRemove; 

        public FurballOptions()
        {
            HandlerErrors = HandlerErrorTypes.DontSendErrors;
            _headersToRemove = new List<string>();
        }

        public HandlerErrorTypes HandlerErrors { get; set; }

        public IPathRepository PathRepository
        {
            get { return _pathRepository ?? (_pathRepository = new PathRepository(PathSource.GetPaths())); }
            set { _pathRepository = value; }
        }

        public IPathSource PathSource { get; set; }

        public IEnumerable<string> HeadersToRemove => _headersToRemove;

        public FurballOptions RemoveHeader(string headerName)
        {
            if (!_headersToRemove.Contains(headerName))
            {
                _headersToRemove.Add(headerName);
            }

            return this;
        }
    }
}
