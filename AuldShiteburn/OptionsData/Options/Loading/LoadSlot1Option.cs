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
            if (Directory.Exists($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}"))
            {
                var mapToLoad = Directory.GetFiles($"{DirectoryName.Saves}\\{DirectoryName.SaveSlot1}");
                FileStream stream = File.OpenRead($"{mapToLoad[0]}");
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Map.Instance = (Map)formatter.Deserialize(stream);
                    PlayerEntity.Instance = Map.Instance.player;
                    stream.Close();
                }
                catch
                {
                    stream.Close();
                    Map.Instance = new AuldShiteburnMap();
                    PlayerEntity.Instance = PlayerEntity.GenerateCharacter();
                }
            }
            else
            {
                Map.Instance = new AuldShiteburnMap();
                PlayerEntity.Instance = PlayerEntity.GenerateCharacter();
            }
        }
    }
}
