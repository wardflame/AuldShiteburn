using AuldShiteburn.BackendData;
using AuldShiteburn.MenuData;
using AuldShiteburn.OptionData;
using AuldShiteburn.OptionsData.Options.Saving;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
            if (Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}").Length <= 0)
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
        public static GameSettings LoadGameSettings()
        {
            bool getGameSettSave = Directory.Exists("GameSettings");
            if (!getGameSettSave)
            {
                Directory.CreateDirectory("GameSettings");
                return GameSettings.Instance;
            }
            else
            {
                if (!File.Exists("GameSettings\\GameSettings.json"))
                {
                    return GameSettings.Instance;
                }
                return JsonConvert.DeserializeObject<GameSettings>(File.ReadAllText($"GameSettings\\GameSettings.json"));
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
        public static void LoadMap()
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
            }
            else
            {
                System.Threading.Thread.Sleep(4000);
                Menu.Instance.InMenu();
            }
        }
    }
}
