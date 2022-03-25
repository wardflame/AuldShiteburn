using AuldShiteburn.ArtData;
using AuldShiteburn.BackendData;
using AuldShiteburn.MenuData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using System;

namespace AuldShiteburn.OptionsData.Options.Settings
{
    internal class SexRatioOption : Option
    {
        public override string DisplayString => ASCIIArt.MENU_SEXRATIO;

        public override void OnUse()
        {
            Console.WriteLine(@"SEX RATIO - Determines the frequency of getting male/female characters. At 0, all names will be female.
At 100, all names will be male. E.g. At 70, there's a 70% chance to get a male name and a 30% to get a female name.

Use the Up and Down arrow keys to increase/decrease the ratio.
Press Enter to confirm.

Ratio: " + GameSettings.Instance.SexRatio);
            int previousSexRatio = GameSettings.Instance.SexRatio;
            bool quit = false;
            do
            {
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.UpArrow:
                        {
                            Console.CursorLeft = 7;
                            Console.CursorTop = 17;
                            Console.Write("   ");
                            Console.CursorLeft = 0;
                            GameSettings.Instance.SexRatio++;
                            Console.Write("Ratio: " + GameSettings.Instance.SexRatio);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            Console.CursorLeft = 7;
                            Console.CursorTop = 17;
                            Console.Write("   ");
                            Console.CursorLeft = 0;
                            GameSettings.Instance.SexRatio--;
                            Console.Write("Ratio: " + GameSettings.Instance.SexRatio);
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            quit = true;
                        }
                        break;
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter && !quit);
            if (InputSystem.InputKey == ConsoleKey.Enter)
            {
                Save.SaveGameSettings();
            }
            else
            {
                GameSettings.Instance.SexRatio = previousSexRatio;
            }
            Menu.Instance.RunMenu();
        }
    }
}
