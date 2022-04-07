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
            get { return new KeyItem("Hideaway Key", "Key to Ormod's hideaway in Shitebreach."); }
        }
        public static KeyItem ResidenceKey
        {
            get { return new KeyItem("Residence Key", "Use in the Living Quarter."); }
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
                    ResidenceKey,
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
