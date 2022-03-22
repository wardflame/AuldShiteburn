using AuldShiteburn.ArtData;
using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MapData.Maps;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AuldShiteburn.OptionsData.Options.Loading
{
    internal class LoadSlot1Option : Option
    {
        public override string DisplayString => ASCIIArt.menuSaveSlot1;

        public override void OnUse()
        {
            Load.LoadSave(DirectoryName.SaveSlot1);
        }
    }
}
