using System;
using System.Collections.Generic;
using SaveSystem.ArchiveSaveSystem.ArchiveEntries;

namespace SaveSystem.Tests
{
    internal static class ProjectArchiveConfig
    {
        public static readonly IDictionary<string, Type> ArchiveFiles = new Dictionary<string, Type>()
        {
            {
                ProjectDataFileName, typeof(XmlArchiveEntry<TestData>)
            },
            {
                MissionFileName, typeof(FileArchiveEntry)
            }
        };
        public const string ProjectDataFileName = "TestProjectData.test";
        public const string MissionFileName = "MissionTest.mission";
    }
}