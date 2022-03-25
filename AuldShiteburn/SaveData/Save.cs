using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
using AuldShiteburn.OptionData;
using AuldShiteburn.OptionsData.Options.Loading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AuldShiteburn.SaveData
{
    internal class Save
    {
        /// <summary>
        /// Save game settings, things such as text speed etc. to binary.
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
        /// Allocate player to the map singleton's player variable and save the map.
        /// If a directory doesn't exist within Saves for said save slot, create one.
        /// Set the player's play time and then serialize the map to .dat in binary.
        /// Save the file name in line with the save slot.
        /// </summary>
        public static bool SaveGame(int saveSlot)
        {
            Directory.CreateDirectory($"{Directories.NAME_SAVES}\\{saveSlot}");

            PlayerEntity.Instance.Playtime = Playtime.GetTotalPlayTime();
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
                System.Threading.Thread.Sleep(10000);
                stream.Close();
                File.Delete($"{Directories.NAME_SAVES}\\{saveSlot}\\{saveName}");
            }
            return false;
        }

        /// <summary>
        /// Check if a save exists with any of the save directories. If one does,
        /// create a new save slot option with the according name. Then, allow the
        /// player to cycle through the save slots and choose one to save into.
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
