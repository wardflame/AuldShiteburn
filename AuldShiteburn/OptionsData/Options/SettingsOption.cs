using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;

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
