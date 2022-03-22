using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
using AuldShiteburn.OptionData;
using AuldShiteburn.OptionsData.Options.Loading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            Directory.CreateDirectory($"{Directories.NAME_GAMESETTINGS}");

            FileStream stream = File.Create($"{Directories.NAME_GAMESETTINGS}\\{Directories.NAME_GAMESETTINGS}.dat");
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
                File.Delete($"{Directories.NAME_GAMESETTINGS}\\{Directories.NAME_GAMESETTINGS}.dat");
            }
        }

        /// <summary>
        /// Save player character.
        /// If a directory doesn't exist within Saves that has
        /// the character's name, create one. Set the player's
        /// play time and then serialize the player to .json.
        /// Save the file name as the total time played in DateTime.
        /// </summary>
        public static bool SaveGame(int saveSlot)
        {
            Directory.CreateDirectory($"{Directories.NAME_SAVES}\\{saveSlot}");

            PlayerEntity.Instance.playtime = Playtime.GetTotalPlayTime();
            Map.Instance.player = PlayerEntity.Instance;

            string saveName = $"{saveSlot}.dat";

            FileStream stream = File.Create($"{Directories.NAME_SAVES}\\{saveSlot}\\{saveName}");
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, Map.Instance);
                stream.Close();
                return true;
            }
            catch (Exception e)
            {
                Utils.WriteColour(ConsoleColor.Red, $"Error: Save failed. " + e.Message);
                stream.Close();
                File.Delete($"{Directories.NAME_SAVES}\\{saveSlot}\\{saveName}");
            }
            return false;
        }

        /// <summary>
        /// Make a list of character directories inside main Saves directory.
        /// Have player choose character from list and access the character directory.
        /// List all saves in that character's directory and let player choose
        /// a save from there. Which save they choose, return the static player to
        /// be the player from that file.
        /// </summary>
        /// <returns></returns>
        public static void SaveOptions()
        {
            List<Option> options = new List<Option>();
            for (int i = 0; i < Directory.GetDirectories(Directories.NAME_SAVES).Length; i++)
            {
                options.Add(new SaveSlotOption(i + 1));                
            }
            Console.Clear();
            Option.SelectRunOption(options, null);
        }
    }
}
