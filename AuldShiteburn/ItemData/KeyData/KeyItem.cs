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
        #endregion Keys

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
