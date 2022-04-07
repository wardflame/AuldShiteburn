using AuldShiteburn.EntityData.PlayerData;
using System;

namespace AuldShiteburn.ItemData
{
    [Serializable]
    internal abstract class Item
    {
        public virtual string Name { get; }
        public virtual void OnInventoryUse(InventorySortData sortData)
        {
        }
    }
}
