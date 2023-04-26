using System.IO;
using UnityEngine;

namespace SaveSystem.CommonSavers
{
    public class FileCopySaver : Saver<string>
    {
        public override string Load(string path, bool uniqueDevice)
        {
            return File.Exists(path) ? path : null;
        }

        public override void Save(string path, string oldPath, bool uniqueDevice)
        {
            if (string.IsNullOrEmpty(oldPath) || File.Exists(oldPath) == false)
            {
                Debug.LogError("The source file doesn't exist");
                return;
            }

            if (oldPath == path)
            {
                Debug.LogWarning("The source and target path is the same");
                return;
            }
            File.Copy(oldPath, path, true);
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