using AuldShiteburn.BackendData;
using AuldShiteburn.MenuData;
using AuldShiteburn.OptionData;
using AuldShiteburn.OptionsData.Options.Loading;
using Newtonsoft.Json;
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
            if (Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}").Length <= 0 &&
                Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot2}").Length <= 0 &&
                Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot3}").Length <= 0 &&
                Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot4}").Length <= 0)
            {
                Console.WriteLine("\nThere are no saves available.");
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
            if (File.Exists($"{DirectoryName.GameSettings}\\{DirectoryName.GameSettings}.dat"))
            {
                FileStream stream = File.OpenRead($"{DirectoryName.GameSettings}\\{DirectoryName.GameSettings}.dat");
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
        /// Make a list of character directories inside main Saves directory.
        /// Have player choose character from list and access the character directory.
        /// List all saves in that character's directory and let player choose
        /// a save from there. Which save they choose, return the static player to
        /// be the player from that file.
        /// </summary>
        /// <returns></returns>
        public static bool LoadSave()
        {
            if (GetSaves())
            {
                List<Option> options = new List<Option>();
                options.Add(new LoadSlot1Option());
                options.Add(new LoadSlot2Option());
                options.Add(new LoadSlot3Option());
                options.Add(new LoadSlot4Option());
                bool browsing = true;
                Console.Clear();
                Option.PrintOptions(options, 0);
                while (browsing)
                {
                    int index = 0;
                    do
                    {
                        InputSystem.GetInput();
                        switch (InputSystem.InputKey)
                        {
                            case ConsoleKey.UpArrow:
                                {
                                    if (index <= options.Count - 1 && index > 0)
                                    {
                                        index--;
                                        Console.Clear();
                                        Option.PrintOptions(options, index);
                                    }
                                }
                                break;
                            case ConsoleKey.DownArrow:
                                {
                                    if (index >= 0 && index < options.Count - 1)
                                    {
                                        index++;
                                        Console.Clear();
                                        Option.PrintOptions(options, index);
                                    }
                                }
                                break;
                        }
                    } while (InputSystem.InputKey != ConsoleKey.Enter);
                    options[index].OnUse();
                    browsing = false;
                }
                return true;
            }
            else
            {
                System.Threading.Thread.Sleep(2000);
                return false;
            }
        }
    }
}
