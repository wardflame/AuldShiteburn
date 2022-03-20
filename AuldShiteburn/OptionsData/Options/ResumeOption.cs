using AuldShiteburn.ArtData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.OptionData;
using System;

namespace AuldShiteburn.OptionsData.Options
{
    internal class ResumeOption : Option
    {
        public override string DisplayString => ASCIIArt.menuResume;

        public override void OnUse()
        {
            Console.Clear();
            PlayerEntity.Instance.inMenu = false;
            Map.Instance.PrintMap();
        }
    }
}
