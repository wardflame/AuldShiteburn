using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MapData.Maps;
using AuldShiteburn.OptionData;
using AuldShiteburn.OptionsData.Options.Loading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AuldShiteburn.SaveData
{
    internal class Load
    {
        /// <summary>
        /// Check to see if any character directories exist inside the main
        /// Saves directory.
        /// </summary>
        /// <returns>Return true if there is a directory available to access.</returns>
        public static bool GetSaves()
        {
            int vacantSlot = 0;
            for (int i = 0; i < Directories.SAVE_SLOTS; i++)
            {
                if (Directory.GetFiles($"{Directories.NAME_SAVES}\\{i + 1}").Length <= 0)
                {
                    vacantSlot++;
                }
            }
            if (vacantSlot >= Directories.SAVE_SLOTS)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        /// <summary>
        /// If a GameSettings save exists, return it.
        /// </summary>
        /// <returns>Game settings.</returns>
        public static void LoadGameSettings()
        {
            if (File.Exists($"{Directories.NAME_GAMESETTINGS}\\{Directories.NAME_GAMESETTINGS}.dat"))
            {
                FileStream stream = File.OpenRead($"{Directories.NAME_GAMESETTINGS}\\{Directories.NAME_GAMESETTINGS}.dat");
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    GameSettings.Instance = (GameSettings)formatter.Deserialize(stream);
                    stream.Close();
                }
                catch
                {
                    stream.Close();
                    GameSettings.Instance = new GameSettings();
                }
                
            }
            else
            {
                GameSettings.Instance = new GameSettings();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveSlotToLoad"></param>
        /// <returns></returns>
        public static bool LoadSave(int saveSlotToLoad)
        {
            if (Directory.Exists($"{Directories.NAME_SAVES}\\{saveSlotToLoad}"))
            {
                var mapToLoad = Directory.GetFiles($"{Directories.NAME_SAVES}\\{saveSlotToLoad}");
                FileStream stream = File.OpenRead($"{mapToLoad[0]}");
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Map.Instance = (Map)formatter.Deserialize(stream);
                    PlayerEntity.Instance = Map.Instance.player;
                    stream.Close();
                    return true;
                }
                catch
                {
                    stream.Close();
                    Map.Instance = new AuldShiteburnMap();
                    PlayerEntity.Instance = PlayerEntity.GenerateCharacter();
                }
            }
            else
            {
                Map.Instance = new AuldShiteburnMap();
                PlayerEntity.Instance = PlayerEntity.GenerateCharacter();
                return true;
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
        public static void LoadOptions()
        {
            List<Option> options = new List<Option>();
            Map currentMap = Map.Instance;
            PlayerEntity currentPlayer = PlayerEntity.Instance;
            for (int i = 0; i < Directories.SAVE_SLOTS; i++)
            {
                if (Directory.GetFiles($"{Directories.NAME_SAVES}\\{i + 1}").Length > 0)
                {
                    LoadSave(i + 1);
                    options.Add(new LoadSlotOption(i + 1, PlayerEntity.Instance.Name, PlayerEntity.Instance.Playtime));
                }
            }
            Map.Instance = currentMap;
            PlayerEntity.Instance = currentPlayer;
            Console.Clear();
            Option.SelectRunOption(options, null);
        }
    }
}
