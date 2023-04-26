using UnityEngine;

namespace SaveSystem.ArchiveSaveSystem
{
    public class ArchiveGeneratorPresenter : MonoBehaviour
    {
        [SerializeField] private string rootDirectoryName = "~TempProjectArchives";
        private ArchiveGenerator _generator;

        public ArchiveGenerator Generator => _generator ??=
            new ArchiveGenerator($@"{Application.persistentDataPath}/{rootDirectoryName}");

        private void Start()
        {
            if (string.IsNullOrEmpty(rootDirectoryName))
            {
                Debug.LogError("The root directory name has not been specified");
            }
        }

        private void OnDestroy()
        {
            Generator.Dispose();
        }
    }
}