using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    internal class HealthRegenElixirConsumableItem : ConsumableItem
    {
        public override string Name => "Fury Elixir";
        public override string Description => "Applies the potion status effect Fury, restoring 3-5 HP each round for 3 rounds.";
        public int MinHeal { get; } = 3;
        public int MaxHeal { get; } = 5;
        public override void OnInventoryUse(InventorySortData sortData)
        {
            PlayerEntity.Instance.PotionStatusEffect = new ReplenishStatusEffect("Fury", 3, ConsoleColor.DarkRed, 3, 5, true, false, false);
        }
    }
}
