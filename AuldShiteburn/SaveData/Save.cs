using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AuldShiteburn.SaveData
{
    internal class Save
    {
        /// <summary>
        /// Save game settings, things such as text speed etc.
        /// </summary>
        public static void SaveGameSettings()
        {
            if (!Directory.Exists($"{DirectoryName.GameSettings}"))
            {
                Directory.CreateDirectory($"{DirectoryName.GameSettings}");
            }

            FileStream stream = File.Create($"{DirectoryName.GameSettings}\\{DirectoryName.GameSettings}.dat");
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, GameSettings.Instance);
                stream.Close();
            }
            catch (Exception e)
            {
                Utils.WriteColour(ConsoleColor.Red, $"Error: Save failed. " + e.Message);
                stream.Close();
                File.Delete($"{DirectoryName.GameSettings}\\{DirectoryName.GameSettings}.dat");
            }
        }

        /// <summary>
        /// Save player character.
        /// If a directory doesn't exist within Saves that has
        /// the character's name, create one. Set the player's
        /// play time and then serialize the player to .json.
        /// Save the file name as the total time played in DateTime.
        /// </summary>
        public static void SaveGame(DirectoryName saveSlot)
        {
            if (!Directory.Exists($"{DirectoryName.Saves}\\{saveSlot}"))
            {
                Directory.CreateDirectory($"{DirectoryName.Saves}\\{saveSlot}");
            }

            if (Directory.GetFiles($"{DirectoryName.Saves}\\{saveSlot}").Length > 0)
            {
                for (int i = 0; i < Directory.GetFiles($"{DirectoryName.Saves}\\{saveSlot}").Length; i++)
                {
                    File.Delete(Directory.GetFiles($"{DirectoryName.Saves}\\{saveSlot}")[i]);
                }
            }

            PlayerEntity.Instance.playtime = Playtime.GetTotalPlayTime();
            DateTime gameTime = Playtime.GetSessionLengthAsDateTime();

            Map.Instance.player = PlayerEntity.Instance;

            string saveName = $"{Map.Instance.player.name} {gameTime.ToString("H.mm.ss.ff")}.dat";

            FileStream stream = File.Create($"{DirectoryName.Saves}\\{saveSlot}\\{saveName}");
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, Map.Instance);
                stream.Close();
            }
            catch (Exception e)
            {
                Utils.WriteColour(ConsoleColor.Red, $"Error: Save failed. " + e.Message);
                stream.Close();
                File.Delete($"{DirectoryName.Saves}\\{saveSlot}\\{saveName}");
            }
        }
    }
}
