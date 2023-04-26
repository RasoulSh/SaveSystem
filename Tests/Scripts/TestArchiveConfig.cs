using System;
using System.Collections.Generic;
using SaveSystem.ArchiveSaveSystem.ArchiveEntries;

namespace SaveSystem.Tests
{
    internal static class TestArchiveConfig
    {
        public static readonly IDictionary<string, Type> ArchiveFiles = new Dictionary<string, Type>()
        {
            {
                DataFileName, typeof(XmlArchiveEntry<TestData>)
            },
            {
                ImageFileName, typeof(FileArchiveEntry)
            }
        };
        public const string DataFileName = "TestData.test";
        public const string ImageFileName = "TestImage.img";
    }
}