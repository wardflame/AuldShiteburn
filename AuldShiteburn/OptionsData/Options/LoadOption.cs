using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.OptionsData.Options
{
    internal class LoadOption : Option
    {
        public override string DisplayString => ASCIIArt.MenuLoad;

        public override void OnUse()
        {
        }
    }
}
