using AuldShiteburn.OptionData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MenuData
{
    internal abstract class Menu
    {
        public static Menu Instance { get; set; }
        protected static List<Option> options;
        public void InMenu()
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
        protected abstract void InitMenu();

        public Menu()
        {
            options = new List<Option>();
            InitMenu();
        }
    }
}
