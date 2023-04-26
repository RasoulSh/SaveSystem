using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;

namespace SaveSystem.ArchiveSaveSystem
{
    public class Archive : IDisposable
    {
        public string DirectoryPath { get; }
        public string FilePath { get; }
        public IDictionary<string, ArchiveEntry> Entries { get; private set; }

        internal Archive(string directoryPath, string filePath, IDictionary<string, Type> archiveFiles, bool uniqueDevice)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                Debug.LogError("The archive's directory path is empty");
                return;
            }
            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogError("The archive's file path is empty");
                return;
            }
            DirectoryPath = directoryPath;
            FilePath = filePath;
            
            if (File.Exists(filePath))
            {
                using var archive = ZipFile.Open(FilePath, ZipArchiveMode.Read);
                archive.ExtractToDirectory(DirectoryPath);
            }
            
            Entries = new Dictionary<string, ArchiveEntry>();
            archiveFiles ??= new Dictionary<string, Type>();
            foreach (var archiveFile in archiveFiles)
            {
                var newEntry = AddEntry(archiveFile.Key, archiveFile.Value);
                newEntry.LoadData(uniqueDevice);
            }
            if (Directory.Exists(DirectoryPath) == false)
            {
                Directory.CreateDirectory(DirectoryPath);
            }
        }

        public void Dispose()
        {
            Directory.Delete(DirectoryPath, true);
        }

        public void SaveChanges()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            using var archive = ZipFile.Open(FilePath, ZipArchiveMode.Create);
            foreach (var path in Directory.EnumerateFiles(DirectoryPath))
            {
                archive.CreateEntryFromFile(path, Path.GetFileName(path));
            }
        }

        public T AddEntry<T>(string fileName) where T : ArchiveEntry => AddEntry(fileName, typeof(T)) as T;
        
        public ArchiveEntry AddEntry(string fileName, Type entryType)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                Debug.LogError("The entry file name is empty");
                return null;
            }
            if (typeof(ArchiveEntry).IsAssignableFrom(entryType) == false)
            {
                Debug.LogError("The entry type is not assignable from ArchiveEntry class");
                return null;
            }
            var fileAddress = GenerateFileAddress(fileName);
            var newEntry = Activator.CreateInstance(entryType, fileAddress) as ArchiveEntry;
            Entries.Add(fileName, newEntry);
            return newEntry;
        }

        public void RemoveEntry(string fileName)
        {
            var entry = Entries[fileName];
            Entries.Remove(fileName);
            entry.Dispose();
        }

        private string GenerateFileAddress(string fileName)
        {
            return $@"{DirectoryPath}/{fileName}";
        }
    }
}