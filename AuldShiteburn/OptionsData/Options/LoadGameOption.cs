using AuldShiteburn.ArtData;
using AuldShiteburn.MapData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using System;

namespace AuldShiteburn.OptionsData.Options
{
    internal class LoadGameOption : Option
    {
        public override string DisplayString => ASCIIArt.menuLoad;

        public override void OnUse()
        {
            Load.LoadMap();
            Console.Clear();
            Map.Instance.PrintMap();
        }
    }
}
