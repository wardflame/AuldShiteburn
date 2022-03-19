using AuldShiteburn.ArtData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MapData.Maps;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.OptionsData.Options
{
    class NewGameOption : Option
    {
        public override string DisplayString => ASCIIArt.menuNewGame;

        public override void OnUse()
        {
            Console.Clear();
            AuldShiteburnMap shiteburn = new AuldShiteburnMap();
            shiteburn.RandomiseAreas();
            Map.Instance = shiteburn;
            PlayerEntity.GenerateCharacter();

            shiteburn.PrintMap();
        }
    }
}
