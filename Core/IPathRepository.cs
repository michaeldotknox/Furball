namespace Furball.Core
{
    public interface IPathRepository
    {
        Path GetMethod(string path, string webMethod, object[] parameters);
    }
}
