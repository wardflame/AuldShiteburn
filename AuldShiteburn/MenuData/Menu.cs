using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MenuData
{
    internal abstract class Menu
    {
        protected abstract void InitMenu();

        public Menu()
        {
            InitMenu();
        }
    }
}
