namespace Furball.Core
{
    public static class Parameter
    {
        public static T OfType<T>()
        {
            return default(T);
        }
    }
}
