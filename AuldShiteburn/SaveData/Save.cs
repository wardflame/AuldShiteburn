using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AuldShiteburn.SaveData
{
    internal class Save
    {
        /// <summary>
        /// Save game settings, things such as text speed etc.
        /// </summary>
        public static void SaveGameSettings()
        {
            if (!Directory.Exists($"GameSettings"))
            {
                Directory.CreateDirectory($"GameSettings");
            }

            string save = JsonConvert.SerializeObject(GameSettings.Instance);

            File.WriteAllText($"GameSettings\\GameSettings.json", save);
        }

        /// <summary>
        /// Save player character.
        /// If a directory doesn't exist within Saves that has
        /// the character's name, create one. Set the player's
        /// play time and then serialize the player to .json.
        /// Save the file name as the total time played in DateTime.
        /// </summary>
        public static void SaveGame()
        {
            if (!Directory.Exists($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}"))
            {
                Directory.CreateDirectory($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}");
            }

            /*var slot1Files = Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}");
            for (int i = 0; i < slot1Files.Length - 1; i++)
            {
               File.Delete($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}\\{slot1Files[i]}");
            }*/

            PlayerEntity.Instance.inMenu = false;
            PlayerEntity.Instance.playtime = Playtime.GetTotalPlayTime();
            DateTime gameTime = Playtime.GetSessionLengthAsDateTime();
            Map.Instance.Player = PlayerEntity.Instance;
            SaveStructure save = Map.Instance.SaveMap();

            string saveMap = JsonConvert.SerializeObject(save);
            File.WriteAllText($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}\\{Map.Instance.Name}.{gameTime.ToString("H.mm.ss.ff")}.json", saveMap);

            /*string savePlayer = JsonConvert.SerializeObject(PlayerEntity.Instance);
            File.WriteAllText($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}\\{PlayerEntity.Instance.name}.{gameTime.ToString("H.mm.ss.ff")}.json", savePlayer);*/
        }
    }
}
