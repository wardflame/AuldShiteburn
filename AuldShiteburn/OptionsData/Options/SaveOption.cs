using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.OptionsData.Options
{
    internal class SaveOption : Option
    {
        public override string DisplayString => ASCIIArt.menuSave;

        public override void OnUse()
        {
        }
    }
}
