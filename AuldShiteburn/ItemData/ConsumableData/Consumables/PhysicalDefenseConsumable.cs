using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    [Serializable]
    internal class PhysicalDefenseConsumable : ConsumableItem
    {
        public override string Name => "Stoneskin Elixir";
        public override string Description => "Applies the potion status effect Stoneskin, providing Major Physical defense for 3 rounds.";
        public int MinMana { get; } = 6;
        public int MaxMana { get; } = 12;
        public override void OnInventoryUse(InventorySortData sortData)
        {
            Utils.SetCursorInteract(Console.CursorTop);
            Utils.WriteColour($"Your skin becomes hard and gritty, impenetrable.", ConsoleColor.DarkYellow);
            PlayerEntity.Instance.PotionStatusEffect = new DefenseStatusEffect
                ("Stoneskin", 3, ConsoleColor.DarkGray,
                physicalEffectLevel: CombatData.EffectLevel.Major,
                allPhysicalDefense: true);
            Stock--;
            Utils.SetCursorInteract(Console.CursorTop - 1);
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
