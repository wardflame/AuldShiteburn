using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;
using AuldShiteburn.OptionsData.Options;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MenuData.Menus
{
    internal class PauseMenu : Menu
    {
        public static List<Option> options = new List<Option>();
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
                                if (index <= options.Count && index > 0)
                                {
                                    index--;
                                    Console.Clear();
                                    Option.PrintOptions(options, index);
                                }
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            {
                                if (index >= 0 && index < options.Count)
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
            Console.Clear();
        }

        protected override void InitMenu()
        {
            options.Add(new ResumeOption());
            options.Add(new SaveOption());
            options.Add(new LoadOption());
            options.Add(new SettingsOption());
            options.Add(new ExitOption());
        }
    }
}
