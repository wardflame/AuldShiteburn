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
        public bool menuActive { get; set; } = true;
        public void RunMenu()
        {
            while (menuActive)
            {
                Console.Clear();
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
