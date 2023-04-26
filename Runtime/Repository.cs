namespace SaveSystem
{
    public abstract class Repository<T, TS> where TS : Saver<T>, new()
    {
        protected readonly Saver<T> saver;
        public abstract string GetKey();

        public Repository()
        {
            saver = new TS();
        }
        public void Save(T data, bool uniqueDevice)
        {
            saver.Save(GetKey(), data, uniqueDevice);
        }

        public T Load(bool uniqueDevice)
        {
            return saver.Load(GetKey(), uniqueDevice);
        }
        public void Delete(string key)
        {
            saver.Delete(key);
        }
        public void Delete()
        {
            saver.Delete(GetKey());
        }
    }
}
