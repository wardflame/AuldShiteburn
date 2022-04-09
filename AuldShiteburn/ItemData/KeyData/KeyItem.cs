using System;
using System.Collections.Generic;

namespace AuldShiteburn.ItemData.KeyData
{
    [Serializable]
    internal class KeyItem : Item
    {
        #region Keys
        public static KeyItem HideawayKey
        {
            get { return new KeyItem("Hideaway Key", "Unlocks Ormod's hideaway in Shitebreach."); }
        }
        public static KeyItem EastResidenceKey
        {
            get { return new KeyItem("East Residence Key", "Unlocks the East residence in Living Quarters."); }
        }
        public static KeyItem WestResidenceKey
        {
            get { return new KeyItem("West Residence Key", "Unlocks the West residence in Living Quarters."); }
        }
        public static KeyItem ShitebreachSouthKey
        {
            get { return new KeyItem("Shitebreach South Key", "Unlocks the door in the South of Shitebreach."); }
        }
        public static KeyItem DrainCellKey
        {
            get { return new KeyItem("Drain Cells Key", "Unlocks the cells in The Drain."); }
        }
        public static KeyItem DrainGateKey
        {
            get { return new KeyItem("Drain Gate Key", "Unlocks the gate in the South of The Drain."); }
        }
        public static KeyItem GuildMastersKey
        {
            get { return new KeyItem("Guild Master's Key", "Unlocks the private quarters in the Guild Hall."); }
        }
        public static KeyItem GuildHallEastKey
        {
            get { return new KeyItem("Guild Hall East Key", "Unlocks the east door in the Guild Hall."); }
        }
        public static KeyItem GuildHallWestKey
        {
            get { return new KeyItem("Guild Hall West Key", "Unlocks the west door in the Guild Hall."); }
        }
        public static KeyItem MillHouseKey
        {
            get { return new KeyItem("Mill House Key", "Unlocks the Mill House door."); }
        }
        #endregion Keys
        #region Notes
        public static KeyItem ScrappyNote
        {
            get { return new KeyItem("Scrappy Note", "'Rodor, if you receive this, I've left the cell master key in my quarters of the Guild Hall.'"); }
        }
        #endregion Notes
        #region Amulets
        public static KeyItem OrmodsAmulet
        {
            get { return new KeyItem("Ormod's Amulet", "A simple wooden amulet carved crudely by an amateur. It is warm to the touch."); }
        }
        public static KeyItem ShitestainedAmulet
        {
            get { return new KeyItem("Shite-stained Amulet", "A marble amulet once pale like moonlight, now dull and dormant."); }
        }
        public static KeyItem MoonlitAmulet
        {
            get { return new KeyItem("Moonlit Amulet", "Visions of the heart brimming with light."); }
        }
        #endregion Amulets

        public override string Name { get; }
        public string Description { get; }
        public static List<KeyItem> AllKeys
        {
            get
            {
                return new List<KeyItem>()
                {
                    HideawayKey,
                    EastResidenceKey,
                    WestResidenceKey,
                    ShitebreachSouthKey,
                    DrainCellKey,
                    DrainGateKey,
                    GuildMastersKey,
                    GuildHallEastKey,
                    GuildHallWestKey,
                    MillHouseKey
                };
            }
        }

        public KeyItem(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
