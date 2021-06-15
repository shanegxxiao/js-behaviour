namespace Base.Runtime
{
    public class Singleton<T> where T : class, new()
    {
        protected Singleton()
        {
        }

        public static T Inst { get; } = new T();
    }
}