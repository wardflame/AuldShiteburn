using System;
using System.Collections.Generic;

namespace AuldShiteburn.ItemData.KeyData
{
    [Serializable]
    internal class KeyItem : Item
    {
        public override string Name { get; }
        public string Description { get; }
        public static List<KeyItem> AllKeys
        {
            get
            {
                return new List<KeyItem>()
                {
                    residentKey
                };
            }
        }

        public KeyItem(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static KeyItem residentKey = new KeyItem("Residents Key", "Used in the Living Quarter.");
    }
}
