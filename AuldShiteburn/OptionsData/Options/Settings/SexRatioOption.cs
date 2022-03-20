using AuldShiteburn.BackendData;
using AuldShiteburn.MenuData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.OptionsData.Options.Settings
{
    internal class SexRatioOption : Option
    {
        public override string DisplayString => "SEX RATIO";

        public override void OnUse()
        {
            Console.WriteLine(@"SEX RATIO - Determines the frequency of getting male/female characters. E.g. at 70, the player, when generating,
has a 70% chance to be male, and 30% to be female.

Use the Up and Down arrow keys to increase/decrease the ratio.
Press Enter to confirm.

Ratio: " + GameSettings.Instance.SexRatio);
            do
            {
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (GameSettings.Instance.SexRatio >= 0 && GameSettings.Instance.SexRatio < 100)
                            {
                                Console.CursorLeft = 0;
                                Console.CursorTop = 7;
                                GameSettings.Instance.SexRatio += 1;
                                Console.Write("Ratio: " + GameSettings.Instance.SexRatio);
                            }                            
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (GameSettings.Instance.SexRatio > 0 && GameSettings.Instance.SexRatio <= 100)
                            {
                                Console.CursorLeft = 0;
                                Console.CursorTop = 7;
                                GameSettings.Instance.SexRatio -= 1;
                                Console.Write("Ratio: " + GameSettings.Instance.SexRatio);
                            }
                        }
                        break;
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter);
            
            Save.SaveGameSettings();
            Menu.Instance.InMenu();
        }
    }
}
