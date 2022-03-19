using System.IO;

namespace AuldShiteburn.SaveData
{
    internal class Directories
    {
        /// <summary>
        /// If a directory with a name from the enumerator DirectoryName
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

        /// <summary>
        /// If the default save directories do not exist, make them.
        /// </summary>
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
            if (!Directory.Exists($"{DirectoryName.Saves}\\{DirectoryName.GameSettings}"))
            {
                Directory.CreateDirectory($"{DirectoryName.Saves}\\{DirectoryName.GameSettings}");
            }
        }
    }

    /// <summary>
    /// An enum designed to keep directory names safe and consistent.
    /// </summary>
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
