using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;

namespace AuldShiteburn.OptionsData.Options
{
    internal class SaveGameOption : Option
    {
        public override string DisplayString => ASCIIArt.MENU_SAVE;

        public override void OnUse()
        {
            Save.SaveOptions();
        }
    }
}
