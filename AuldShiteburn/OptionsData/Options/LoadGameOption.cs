using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;

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
