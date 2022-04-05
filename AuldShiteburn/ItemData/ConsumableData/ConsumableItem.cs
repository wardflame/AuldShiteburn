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
                return new List<ConsumableItem>();
                {
                    new HealthElixirConsumableItem();
                    new StaminaElixirConsumableItem();
                    new ManaElixirConsumableItem();
                    new ShiteElixirConsumableItem();
                };
            }
        }
        public abstract void OnConsumption();
    }
}
