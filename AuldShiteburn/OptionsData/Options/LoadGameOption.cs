using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.OptionsData.Options
{
    internal class LoadGameOption : Option
    {
        public override string DisplayString => ASCIIArt.menuLoad;

        public override void OnUse()
        {
        }
    }
}
