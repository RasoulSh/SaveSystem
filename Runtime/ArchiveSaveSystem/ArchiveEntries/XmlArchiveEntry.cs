using SaveSystem.CommonSavers;

namespace SaveSystem.ArchiveSaveSystem.ArchiveEntries
{
    public class XmlArchiveEntry<T> : ArchiveEntry<T, XMLFileSaver<T>> where T : class, new()
    {
        public XmlArchiveEntry(string filePath) : base(filePath)
        {
        }
    }
}