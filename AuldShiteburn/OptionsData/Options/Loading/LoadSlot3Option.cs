using AuldShiteburn.ArtData;
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
            if (Directory.Exists($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot3}"))
            {
                if (Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot3}").Length > 0)
                {
                    string saveName = Path.GetFileName(Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot3}")[0]);
                    Map.Instance = JsonConvert.DeserializeObject<Map>(File.ReadAllText($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot3}\\{saveName}"));
                }
                else
                {
                    Console.WriteLine("No saves available in Slot 3.");
                }
            }
        }
    }
}
