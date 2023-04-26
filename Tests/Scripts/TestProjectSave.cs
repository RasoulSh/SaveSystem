#if UNITY_EDITOR
using SaveSystem.ArchiveSaveSystem;
using SaveSystem.ArchiveSaveSystem.ArchiveEntries;
using UnityEditor;
using UnityEngine;

namespace SaveSystem.Tests
{
    [RequireComponent(typeof(ArchiveGeneratorPresenter))]
    public class TestProjectSave : MonoBehaviour
    {
        private ArchiveGenerator _archiveGenerator;
        private Archive _currentArchive;
        public XmlArchiveEntry<TestData> ProjectDataEntry => _currentArchive.Entries[ProjectArchiveConfig.ProjectDataFileName] as XmlArchiveEntry<TestData>;
        public FileArchiveEntry MissionEntry => _currentArchive.Entries[ProjectArchiveConfig.MissionFileName] as FileArchiveEntry;
        public void SaveCurrentArchive() => _currentArchive.SaveChanges();

        void Start()
        {
            _archiveGenerator = GetComponent<ArchiveGeneratorPresenter>().Generator;
        }

        public void NewArchive(string archivePath)
        {
            if (_currentArchive != null)
            {
                _archiveGenerator.DisposeArchive(_currentArchive);
            }
            _currentArchive = _archiveGenerator.GenerateArchive(GUID.Generate().ToString(), archivePath, TestArchiveConfig.ArchiveFiles, false);
        }
    }
   
}
#endif