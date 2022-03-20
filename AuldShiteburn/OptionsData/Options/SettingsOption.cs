using AuldShiteburn.ArtData;
using AuldShiteburn.MenuData;
using AuldShiteburn.OptionData;
using AuldShiteburn.OptionsData.Options.Settings;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.OptionsData.Options
{
    internal class SettingsOption : Option
    {
        public override string DisplayString => ASCIIArt.menuSettings;

        public override void OnUse()
        {
            List<Option> options = new List<Option>();
            options.Add(new SexRatioOption());
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
    }
}
