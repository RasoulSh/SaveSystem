#if UNITY_EDITOR
using SaveSystem.ArchiveSaveSystem;
using SaveSystem.ArchiveSaveSystem.ArchiveEntries;
using UnityEditor;
using UnityEngine;

namespace SaveSystem.Tests
{
    [RequireComponent(typeof(ArchiveGeneratorPresenter))]
    public class TestArchiveSave : MonoBehaviour
    {
        private ArchiveGenerator _archiveGenerator;
        private Archive _currentArchive;
        public XmlArchiveEntry<TestData> DataEntry => _currentArchive.Entries[TestArchiveConfig.DataFileName] as XmlArchiveEntry<TestData>;
        public FileArchiveEntry ImageEntry => _currentArchive.Entries[TestArchiveConfig.ImageFileName] as FileArchiveEntry;
        public void SaveCurrentArchive() => _currentArchive.SaveChanges();

        void Start()
        {
            _archiveGenerator = GetComponent<ArchiveGeneratorPresenter>().Generator;
        }

        public void NewArchive(string archivePath, bool uniqueDevice)
        {
            if (_currentArchive != null)
            {
                _archiveGenerator.DisposeArchive(_currentArchive);
            }
            _currentArchive = _archiveGenerator.GenerateArchive(GUID.Generate().ToString(), archivePath, TestArchiveConfig.ArchiveFiles, uniqueDevice);
        }
    }
}
#endif