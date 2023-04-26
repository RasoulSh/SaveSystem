using SaveSystem.CommonSavers;

namespace SaveSystem.ArchiveSaveSystem.ArchiveEntries
{
    public class FileArchiveEntry : ArchiveEntry<string, FileCopySaver>
    {
        public FileArchiveEntry(string filePath) : base(filePath)
        {
        }
    }
}