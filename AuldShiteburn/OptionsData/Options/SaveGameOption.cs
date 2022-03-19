using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;

namespace AuldShiteburn.OptionsData.Options
{
    internal class SaveGameOption : Option
    {
        public override string DisplayString => ASCIIArt.menuSave;

        public override void OnUse()
        {
        }
    }
}
