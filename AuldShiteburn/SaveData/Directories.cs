using System.IO;

namespace AuldShiteburn.SaveData
{
    internal class Directories
    {
        public const int SAVE_SLOTS = 5;
        public const string NAME_SAVES = "Saves";
        public const string NAME_GAMESETTINGS = "GameSettings";

        /// <summary>
        /// If the default save directories do not exist, make them.
        /// </summary>
        public static void SaveDirectoryInit()
        {
            Directory.CreateDirectory($"{NAME_SAVES}");
            for (int i = 1; i <= SAVE_SLOTS; i++)
            {
                Directory.CreateDirectory($"{NAME_SAVES}\\{i}");
            }
            Directory.CreateDirectory($"{NAME_GAMESETTINGS}");
        }
    }
}
