using AuldShiteburn.ArtData;
using AuldShiteburn.OptionData;
using System;

namespace AuldShiteburn.OptionsData.Options
{
    internal class ExitOption : Option
    {
        public override string DisplayString => ASCIIArt.MenuExit;

        public override void OnUse()
        {
            if (Utilities.VerificationQuery("\nYou are about to exit the game. Any unsaved progress will be lost. Continue? (Y/N)") == true)
            {
                Environment.Exit(0);
            }
        }
    }
}
