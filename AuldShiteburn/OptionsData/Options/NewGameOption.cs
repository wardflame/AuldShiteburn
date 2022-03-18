using AuldShiteburn.ArtData;
using AuldShiteburn.MapData;
using AuldShiteburn.MapData.Maps;
using AuldShiteburn.OptionData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.OptionsData.Options
{
    class NewGameOption : Option
    {
        public override string DisplayString => ASCIIArt.menuNewGame;

        public override void OnUse()
        {
            Game.playing = false;
            Game.newGame = true;
        }
    }
}
