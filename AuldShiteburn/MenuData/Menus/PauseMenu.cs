using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;
using AuldShiteburn.OptionsData.Options;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MenuData.Menus
{
    internal class PauseMenu : Menu
    {
        public static void InMenu()
        {
            Console.Clear();
            Option.PrintOptions(options, 0);
            bool browsing = true;
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

        protected override void InitMenu()
        {
            options.Add(new ResumeOption());
            options.Add(new SaveGameOption());
            options.Add(new LoadGameOption());
            options.Add(new SettingsOption());
            options.Add(new ExitOption());
        }
    }
}
