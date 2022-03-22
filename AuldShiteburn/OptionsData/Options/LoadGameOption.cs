using AuldShiteburn.ArtData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MenuData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using System;

namespace AuldShiteburn.OptionsData.Options
{
    internal class LoadGameOption : Option
    {
        public override string DisplayString => ASCIIArt.MENU_LOAD;
        
        public override void OnUse()
        {
            Load.LoadOptions();
        }
    }
}
