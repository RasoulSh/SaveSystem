namespace SaveSystem
{
    public abstract class Saver<T>
    {
        public abstract T Load(string key, bool uniqueDevice);

        public abstract void Save(string key, T data, bool uniqueDevice);
        public abstract void Delete(string key);
    }
}
