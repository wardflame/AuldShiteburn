﻿using AuldShiteburn.ArtData;
using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MenuData;
using AuldShiteburn.OptionData;
using AuldShiteburn.SaveData;
using System;
using System.Collections.Generic;
using System.Text;

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
                Utils.WriteColour(ConsoleColor.DarkRed, "Save failed.");
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