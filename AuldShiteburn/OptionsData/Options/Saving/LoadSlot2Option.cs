using AuldShiteburn.ArtData;
using AuldShiteburn.MapData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AuldShiteburn.OptionsData.Options.Saving
{
    internal class LoadSlot2Option : Option
    {
        public override string DisplayString => ASCIIArt.menuSaveSlot2;

        public override void OnUse()
        {
            if (Directory.Exists($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot2}"))
            {
                if (Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot2}").Length > 0)
                {
                    string saveName = Path.GetFileName(Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot2}")[0]);
                    Map.Instance = JsonConvert.DeserializeObject<Map>(File.ReadAllText($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot2}\\{saveName}"));
                }
                else
                {
                    Console.WriteLine("No saves available in Slot 2.");
                }
            }
        }
    }
}
