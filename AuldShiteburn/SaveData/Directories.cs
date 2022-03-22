using System.IO;

namespace AuldShiteburn.SaveData
{
    internal class Directories
    {
        const int SAVE_SLOTS = 5;
        /// <summary>
        /// If a directory with a name from the enumerator DirectoryName
        /// does not exist, create one.
        /// </summary>
        /// <param name="directory"></param>
        public static void DirectoryExistCheck(DirectoryName directoryName)
        {
            Directory.CreateDirectory($"{directoryName}");            
        }

        /// <summary>
        /// If the default save directories do not exist, make them.
        /// </summary>
        public static void SaveDirectoryInit()
        {
            Directory.CreateDirectory($"{DirectoryName.Saves}");
            for (int i = 1; i <= SAVE_SLOTS; i++)
            {
                Directory.CreateDirectory($"{DirectoryName.Saves}\\{i}");
            }
            Directory.CreateDirectory($"{DirectoryName.GameSettings}");
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
