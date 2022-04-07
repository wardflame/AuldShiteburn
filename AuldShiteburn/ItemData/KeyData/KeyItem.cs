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
            get { return new KeyItem("Hideaway Key", "Key to the cell in Shitebreach."); }
        }
        public static KeyItem ResidenceKey
        {
            get { return new KeyItem("Residence Key", "Use in the Living Quarter."); }
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
                    ResidenceKey
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
