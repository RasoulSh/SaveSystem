#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

namespace SaveSystem.Tests
{
    public class TestArchiveSaveUI : MonoBehaviour
    {
        [SerializeField] private InputField archivePath;
        [SerializeField] private TestArchiveSave archiveSaver;
        [SerializeField] private TestData testData;
        [SerializeField] private InputField sourceFilePath;
        [SerializeField] private Button addDataButton;
        [SerializeField] private Button addFileButton;
        [SerializeField] private Button saveArchiveButton;
        [SerializeField] private Button newArchiveButton;

        private void Start()
        {
            addDataButton.onClick.AddListener(AddData);
            addFileButton.onClick.AddListener(AddImageFile);
            saveArchiveButton.onClick.AddListener(archiveSaver.SaveCurrentArchive);
            newArchiveButton.onClick.AddListener(NewArchive);
        }

        private void NewArchive()
        {
            archiveSaver.NewArchive(archivePath.text, false);
        }

        private void AddImageFile()
        {
            archiveSaver.ImageEntry.UpdateData(sourceFilePath.text);
            archiveSaver.ImageEntry.SaveChanges(true);
        }

        private void AddData()
        {
            archiveSaver.DataEntry.UpdateData(testData);
            archiveSaver.DataEntry.SaveChanges(true);
        }
    }
}
#endif