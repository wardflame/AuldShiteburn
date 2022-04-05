using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.ItemData.ConsumableData.Consumables;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.ItemData.ConsumableData
{
    [Serializable]
    internal abstract class ConsumableItem : Item
    {
        public virtual string Description { get; }
        public int Stock { get; set; }
        public static List<ConsumableItem> AllConsumables
        {
            get
            {
                return new List<ConsumableItem>()
                {
                    new HealthRegenElixirConsumableItem(),
                    new StaminaElixirConsumableItem(),
                    new ManaElixirConsumableItem(),
                    new ShiteDefenseElixirConsumableItem()
                };
            }
        }

        public ConsumableItem()
        {
            Random rand = new Random();
            Stock = rand.Next(1, 4);
        }

        public override void OnInventoryUse(InventorySortData sortData)
        {
        }
    }
}
