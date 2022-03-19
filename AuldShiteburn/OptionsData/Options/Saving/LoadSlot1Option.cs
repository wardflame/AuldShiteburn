using AuldShiteburn.ArtData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MapData.Maps;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using Newtonsoft.Json;
using System;
using System.IO;

namespace AuldShiteburn.OptionsData.Options.Saving
{
    internal class LoadSlot1Option : Option
    {
        public override string DisplayString => ASCIIArt.menuSaveSlot1;

        public override void OnUse()
        {
            
            
            
            
            
            
            /*string saveMap = Path.GetFileName(Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}")[0]);
            SaveStructure loadedSave = JsonConvert.DeserializeObject<SaveStructure>(File.ReadAllText($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}\\{saveMap}"));
            Type type = loadedSave.mapType;
            Console.WriteLine(type);

            /*string savePlayer = Path.GetFileName(Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}")[1]);
            Map.Instance = JsonConvert.DeserializeObject<AuldShiteburnMap>(File.ReadAllText($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}\\{saveMap}"));
            Console.WriteLine("Map: " + Map.Instance);
            Map.Instance.LoadArea();
            Console.WriteLine("Current Area: " + Map.Instance.CurrentArea);
            PlayerEntity.Instance = Map.Instance.Player;
            //PlayerEntity.Instance = JsonConvert.DeserializeObject<PlayerEntity>(File.ReadAllText($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}\\{savePlayer}"));
            Console.WriteLine(PlayerEntity.Instance.name);
            /*if (Directory.Exists($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}"))
            {
                if (Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}").Length > 0)
                {
                    string saveName = Path.GetFileName(Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}")[0]);
                    Map.Instance = JsonConvert.DeserializeObject<Map>(File.ReadAllText($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}\\{saveName}"));
                }
                else
                {
                    Console.WriteLine("No saves available in Slot 1.");
                }
            }*/
        }
    }
}
