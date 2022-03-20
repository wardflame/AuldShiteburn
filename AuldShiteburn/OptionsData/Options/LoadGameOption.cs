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
        public override string DisplayString => ASCIIArt.menuLoad;

        public override void OnUse()
        {            
            if (Load.LoadSave())
            {
                if (Game.mainMenu)
                {
                    Game.mainMenu = false;
                }
                Menu.Instance.menuActive = false;
                PlayerEntity.Instance.inMenu = false;
                Game.playing = true;
                Console.Clear();
                Map.Instance.PrintMap();
            }            
        }
    }
}
