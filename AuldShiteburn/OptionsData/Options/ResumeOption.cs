using AuldShiteburn.ArtData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MenuData;
using AuldShiteburn.OptionData;
using System;

namespace AuldShiteburn.OptionsData.Options
{
    internal class ResumeOption : Option
    {
        public override string DisplayString => ASCIIArt.MENU_RESUME;

        public override void OnUse()
        {
            Console.Clear();
            Menu.Instance.menuActive = false;
            PlayerEntity.Instance.InMenu = false;
            Map.Instance.PrintMap();
        }
    }
}
