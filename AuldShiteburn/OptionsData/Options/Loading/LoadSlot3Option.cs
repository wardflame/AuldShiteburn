﻿using AuldShiteburn.ArtData;
using AuldShiteburn.MapData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AuldShiteburn.OptionsData.Options.Loading
{
    internal class LoadSlot3Option : Option
    {
        public override string DisplayString => ASCIIArt.menuSaveSlot3;

        public override void OnUse()
        {
            Load.LoadSave(DirectoryName.SaveSlot3);
        }
    }
}
