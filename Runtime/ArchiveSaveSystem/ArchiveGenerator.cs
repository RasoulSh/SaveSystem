using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveSystem.ArchiveSaveSystem
{
    public class ArchiveGenerator : IDisposable
    {
        private readonly IList<Archive> _currentArchives;
        public string RootDirectoryPath { get; }

        internal ArchiveGenerator(string rootDirectoryPath)
        {
            RootDirectoryPath = rootDirectoryPath;
            _currentArchives = new List<Archive>();
            if (Directory.Exists(RootDirectoryPath))
            {
                Directory.Delete(RootDirectoryPath, true);
            }
            var directory = Directory.CreateDirectory(RootDirectoryPath);
            directory.Attributes = FileAttributes.Directory | FileAttributes.Hidden; 
        }

        public Archive GenerateArchive(string directoryName, string filePath, IDictionary<string, Type> archiveFiles, bool uniqueDevice)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogError("Please provide the file path");
                return null;
            }
            var directoryPath = $@"{RootDirectoryPath}/{directoryName}";
            var newArchive = new Archive(directoryPath, filePath, archiveFiles, uniqueDevice);
            _currentArchives.Add(newArchive);
            return newArchive;
        }

        public void Dispose()
        {
            foreach (var archive in _currentArchives)
            {
                archive.Dispose();
            }
            _currentArchives.Clear();
            Directory.Delete(RootDirectoryPath);
        }

        public void DisposeArchive(Archive archive)
        {
            archive.Dispose();
            _currentArchives.Remove(archive);
        }
    }
}