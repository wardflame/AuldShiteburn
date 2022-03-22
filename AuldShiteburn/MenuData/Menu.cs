using AuldShiteburn.OptionData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MenuData
{
    internal abstract class Menu
    {
        public static Menu Instance { get; set; }
        public List<Option> options;
        public abstract string Banner { get; }
        public bool menuActive = true;
        public void InMenu()
        {
            while (menuActive)
            {
                Console.Clear();
                if (Banner != null)
                {
                    Utils.WriteColour(ConsoleColor.DarkYellow, Banner);
                }
                Option.SelectRunOption(options, Banner);
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
