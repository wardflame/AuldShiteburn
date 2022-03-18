using AuldShiteburn.OptionData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MenuData
{
    internal abstract class Menu
    {
        protected static List<Option> options;
        protected abstract void InitMenu();

        public Menu()
        {
            options = new List<Option>();
            InitMenu();
        }
    }
}
