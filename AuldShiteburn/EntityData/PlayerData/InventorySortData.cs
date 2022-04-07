using System;

namespace AuldShiteburn.EntityData.PlayerData
{
    [Serializable]
    internal struct InventorySortData
    {
        public int index;
        public int typeColumn;
        public int typeOffset;
        public bool isPlayerInventory;
        public bool transferIntended;
    }
}
