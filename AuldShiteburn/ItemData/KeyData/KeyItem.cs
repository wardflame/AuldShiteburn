using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.KeyData
{
    [Serializable]
    internal class KeyItem : Item
    {
        public static List<KeyItem> keys = new List<KeyItem>()
        {
            residentKey
        };
        public static KeyItem residentKey = new KeyItem();
    }
}
