using AuldShiteburn.ArtData;
using AuldShiteburn.MapData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AuldShiteburn.OptionsData.Options.Loading
{
    internal class LoadSlot4Option : Option
    {
        public override string DisplayString => ASCIIArt.menuSaveSlot4;

        public override void OnUse()
        {
            Load.LoadSave(DirectoryName.SaveSlot4);
        }
    }
}
