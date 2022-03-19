using AuldShiteburn.ArtData;
using AuldShiteburn.MapData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AuldShiteburn.OptionsData.Options.Saving
{
    internal class LoadSlot4Option : Option
    {
        public override string DisplayString => ASCIIArt.menuSaveSlot4;

        public override void OnUse()
        {
            if (Directory.Exists($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot4}"))
            {
                if (Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot4}").Length > 0)
                {
                    string saveName = Path.GetFileName(Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot4}")[0]);
                    Map.Instance = JsonConvert.DeserializeObject<Map>(File.ReadAllText($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot4}\\{saveName}"));
                }
                else
                {
                    Console.WriteLine("No saves available in Slot 4.");
                }
            }
        }
    }
}
