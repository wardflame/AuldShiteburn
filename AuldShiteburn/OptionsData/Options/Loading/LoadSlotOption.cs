using AuldShiteburn.ArtData;
using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MenuData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using System;

namespace AuldShiteburn.OptionsData.Options.Loading
{
    internal class LoadSlotOption : Option
    {
        public override string DisplayString => ASCIIArt.ASCIIAppendToEnd(ASCIIArt.MENU_SAVESLOT, ASCIIArt.NumberToASCII(slot)) + $" {playerName}: {playtime.ToString("H:mm:ss")}";
        private int slot;
        private readonly string playerName;
        private readonly DateTime playtime;

        public LoadSlotOption(int slot, string playerName, long playtime)
        {
            this.slot = slot;
            this.playerName = playerName;
            this.playtime = new DateTime(playtime);
        }

        public override void OnUse()
        {
            if (Load.LoadSave(slot))
            {
                if (Game.mainMenu)
                {
                    Game.mainMenu = false;
                    Game.playing = true;
                }
                Console.Clear();
                Menu.Instance.menuActive = false;
                PlayerEntity.Instance.InMenu = false;
                Playtime.StartPlaytime();
                Map.Instance.PrintMap();
            }
        }
    }
}
