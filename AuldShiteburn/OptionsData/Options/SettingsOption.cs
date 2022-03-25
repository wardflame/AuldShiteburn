using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;
using AuldShiteburn.OptionsData.Options.Settings;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.OptionsData.Options
{
    internal class SettingsOption : Option
    {
        public override string DisplayString => ASCIIArt.MENU_SETTINGS;

        public override void OnUse()
        {
            List<Option> options = new List<Option>();
            options.Add(new SexRatioOption());
            Console.Clear();
            SelectRunOption(options, ASCIIArt.BANNER_SETTINGS);
        }
    }
}
