using AuldShiteburn.OptionsData.Options;

namespace AuldShiteburn.MenuData.Menus
{
    internal class PauseMenu : Menu
    {
        public override string Banner => null;
        protected override void InitMenu()
        {
            options.Add(new ResumeOption());
            options.Add(new SaveGameOption());
            options.Add(new LoadGameOption());
            options.Add(new SettingsOption());
            options.Add(new ExitOption());
        }
    }
}
