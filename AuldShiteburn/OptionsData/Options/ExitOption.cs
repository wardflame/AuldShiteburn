using AuldShiteburn.ArtData;
using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
using AuldShiteburn.OptionData;
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
                                if (Utilities.VerificationQuery("\nYou are about to exit to the main menu. Any unsaved progress will be lost. Continue? (Y/N)") == true)
                                {
                                    Console.Clear();
                                    Menu.Instance = new MainMenu();
                                    Menu.Instance.InMenu();
                                }
                                else
                                {
                                    Console.Clear();
                                    Menu.Instance.InMenu();
                                }
                            }
                        }
                        break;
                    case ConsoleKey.E:
                        {
                            if (Utilities.VerificationQuery("\nYou are about to exit the game. Any unsaved progress will be lost. Continue? (Y/N)") == true)
                            {
                                Environment.Exit(0);
                            }
                            else
                            {
                                Console.Clear();
                                Menu.Instance.InMenu();
                            }
                        }
                        break;
                    case ConsoleKey.C:
                        {
                            Console.Clear();
                            Menu.Instance.InMenu();
                        }
                        break;
                }
            }
        }
    }
}
