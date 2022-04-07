using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    [Serializable]
    internal class HealthRegenConsumable : ConsumableItem
    {
        public override string Name => "Fury Elixir";
        public override string Description => "Applies the potion status effect Fury, restoring 3-5 HP each round for 3 rounds.";
        public int MinHeal { get; } = 3;
        public int MaxHeal { get; } = 5;
        public override void OnInventoryUse(InventorySortData sortData)
        {
            Utils.SetCursorInteract(Console.CursorTop);
            Utils.WriteColour($"You feel the unfettered rage of a thousand lamenting souls.", ConsoleColor.DarkYellow);
            PlayerEntity.Instance.PotionStatusEffect = new ReplenishStatusEffect
                ("Fury", 3, ConsoleColor.Red,
                3, 5, true, false, false);
            Stock--;
            Utils.SetCursorInteract(Console.CursorTop - 1);
            Utils.WriteColour("Press any key to continue.");
            Console.ReadKey(true);
        }
    }
}
