using System;
using System.Collections.Generic;

namespace AuldShiteburn.ItemData
{
    [Serializable]
    internal abstract class Item
    {
        public virtual string Name { get; }
        public virtual void OnInventoryUse()
        {
        }
    }
}
