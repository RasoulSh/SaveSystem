using System;

namespace SaveSystem.ArchiveSaveSystem
{

    public abstract class ArchiveEntry<T,TS> : ArchiveEntry where T : class where TS : Saver<T>, new()
    {
        private Saver<T> _saver;
        public T Data { get; private set; }

        public ArchiveEntry(string filePath) : base(filePath)
        {
            _saver = new TS();
        }
        public override void SaveChanges(bool uniqueDevice) => _saver.Save(FilePath, Data, uniqueDevice);

        public override void LoadData(bool uniqueDevice) => Data = _saver.Load(FilePath, uniqueDevice);

        public override void UpdateData(object data)
        {
            UpdateData(data as T);
        }

        public void UpdateData(T data)
        {
            Data = data;
        }

        public override void Dispose()
        {
            _saver.Delete(FilePath);
        }
    }

    public abstract class ArchiveEntry : IDisposable
    {
        public string FilePath { get; }

        public ArchiveEntry(string filePath)
        {
            FilePath = filePath;
        }

        public abstract void LoadData(bool uniqueDevice);
        public abstract void SaveChanges(bool uniqueDevice);
        public abstract void UpdateData(object data);
        public abstract void Dispose();
    }
}