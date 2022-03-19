using AuldShiteburn.OptionsData.Options;

namespace AuldShiteburn.MenuData.Menus
{
    internal class MainMenu : Menu
    {
        protected override void InitMenu()
        {
            options.Add(new NewGameOption());
            options.Add(new LoadGameOption());
            options.Add(new SettingsOption());
            options.Add(new ExitOption());
        }
    }
}
