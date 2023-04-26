using System.IO;
using SaveSystem.Common;

namespace SaveSystem.CommonSavers
{
    public class XMLFileSaver<T> : Saver<T> where T : class, new ()
    {
        public override T Load(string path, bool uniqueDevice)
        {
            var data = EncryptedXmlSerializer.Load<T>(path, uniqueDevice);
            return data ?? new T();
        }

        public override void Save(string path, T data, bool uniqueDevice)
        {
            EncryptedXmlSerializer.Save<T>(path, data, uniqueDevice);
        }

        public override void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}