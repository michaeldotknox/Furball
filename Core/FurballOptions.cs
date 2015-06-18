namespace Furball.Core
{
    public class FurballOptions
    {
        private IPathRepository _pathRepository;

        public FurballOptions()
        {
            HandlerErrors = HandlerErrorTypes.DontSendErrors;
        }

        public HandlerErrorTypes HandlerErrors { get; set; }

        public IPathRepository PathRepository
        {
            get { return _pathRepository ?? (_pathRepository = new PathRepository(PathSource.GetPaths())); }
            set { _pathRepository = value; }
        }

        public IPathSource PathSource { get; set; }
    }
}
