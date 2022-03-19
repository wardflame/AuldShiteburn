using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;
using AuldShiteburn.OptionsData.Options;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MenuData.Menus
{
    internal class PauseMenu : Menu
    {
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
