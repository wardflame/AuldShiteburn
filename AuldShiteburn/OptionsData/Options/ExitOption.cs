using AuldShiteburn.ArtData;
using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using System;

namespace AuldShiteburn.OptionsData.Options
{
    internal class ExitOption : Option
    {
        public override string DisplayString => ASCIIArt.menuExit;
        public override void OnUse()
        {
            bool mainMenu = Menu.Instance.GetType() == typeof(MainMenu);
            if (mainMenu)
            {
                Console.WriteLine("\nExit to Desktop? (E)\nCancel (C)");
            }
            else
            {
                Console.WriteLine("\nExit to Main Menu? (M)\nExit to Desktop? (E)\nCancel (C)");
            }
            bool choosing = true;
            while (choosing)
            {
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.M:
                        {
                            if (!mainMenu)
                            {
                                if (Utils.VerificationQuery("\nYou are about to exit to the main menu. Any unsaved progress will be lost. Continue? (Y/N)") == true)
                                {
                                    Console.Clear();
                                    PlayerEntity.Instance.inMenu = false;
                                    Save.SaveGame(DirectoryName.SaveSlot1);
                                    Playtime.ResetPlaytime();
                                    Game.playing = false;
                                    Game.mainMenu = true;
                                    choosing = false;
                                    Menu.Instance.menuActive = false;
                                }
                                else
                                {
                                    choosing = false;
                                }
                            }
                        }
                        break;
                    case ConsoleKey.E:
                        {
                            if (Utils.VerificationQuery("\nYou are about to exit the game. Continue? (Y/N)") == true)
                            {
                                Environment.Exit(0);
                            }
                            else
                            {
                                choosing = false;
                            }
                        }
                        break;
                    case ConsoleKey.C:
                        {
                            choosing = false;
                        }
                        break;
                }
            }
        }
    }
}
