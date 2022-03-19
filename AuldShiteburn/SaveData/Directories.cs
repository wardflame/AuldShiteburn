using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AuldShiteburn.SaveData
{
    internal class Directories
    {
        /// <summary>
        /// If a save directory with a name from the enumerator
        /// does not exist, create one.
        /// </summary>
        /// <param name="directory"></param>
        public static void DirectoryExistCheck(DirectoryName directoryName)
        {
            if (!Directory.Exists($"{directoryName}"))
            {
                Directory.CreateDirectory($"{directoryName}");
            }
        }

        public static void SaveDirectoryInit()
        {
            if (!Directory.Exists($"{DirectoryName.Saves}"))
            {
                Directory.CreateDirectory($"{DirectoryName.Saves}");
            }
            if (!Directory.Exists($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}"))
            {
                Directory.CreateDirectory($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}");
            }
            if (!Directory.Exists($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot2}"))
            {
                Directory.CreateDirectory($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot2}");
            }
            if (!Directory.Exists($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot3}"))
            {
                Directory.CreateDirectory($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot3}");
            }
            if (!Directory.Exists($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot4}"))
            {
                Directory.CreateDirectory($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot4}");
            }

        }
    }
    enum DirectoryName
    {
        Saves,
        SaveSlot1,
        SaveSlot2,
        SaveSlot3,
        SaveSlot4,
        GameSettings
    }
}
