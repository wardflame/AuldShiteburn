using AuldShiteburn.ArtData;
using AuldShiteburn.OptionsData.Options;
using AuldShiteburn.SaveData;

namespace AuldShiteburn.MenuData.Menus
{
    internal class MainMenu : Menu
    {
        public override string Banner => ASCIIArt.BANNER_AULDSHITEBURN;
        protected override void InitMenu()
        {
            options.Add(new NewGameOption());
            if (Load.GetSaves())
            {
                options.Add(new LoadGameOption());
            }
            options.Add(new SettingsOption());
            options.Add(new ExitOption());
        }
    }
}
