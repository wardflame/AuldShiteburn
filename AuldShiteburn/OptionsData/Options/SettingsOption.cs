using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.OptionsData.Options
{
    internal class SettingsOption : Option
    {
        public override string DisplayString => ASCIIArt.menuSettings;

        public override void OnUse()
        {
        }
    }
}
