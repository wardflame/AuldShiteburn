using AuldShiteburn.ArtData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MenuData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using System;

namespace AuldShiteburn.OptionsData.Options.Loading
{
    internal class SaveSlotOption : Option
    {
        public override string DisplayString => ASCIIArt.ASCIIAppendToEnd(ASCIIArt.MENU_SAVESLOT, ASCIIArt.NumberToASCII(slot));
        private int slot;

        public SaveSlotOption(int slot)
        {
            this.slot = slot;
        }

        /// Attempt to save the map to the according save slot.
        public override void OnUse()
        {
            if (!Save.SaveGame(slot))
            {
                Utils.WriteColour("Save failed.", ConsoleColor.Red);
            }
            else
            {
                Menu.Instance.menuActive = false;
                PlayerEntity.Instance.InMenu = false;
                Console.Clear();
                Map.Instance.PrintMap();
            }
        }
    }
}
