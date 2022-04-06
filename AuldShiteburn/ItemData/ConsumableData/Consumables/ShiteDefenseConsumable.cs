using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    [Serializable]
    internal class ShiteDefenseConsumable : ConsumableItem
    {
        public override string Name => "Shite Froth";
        public override string Description => "Applies the Shite Froth potion status effect, providing Moderate Occult defense.";
        public int MinHeal { get; } = 4;
        public int MaxHeal { get; } = 8;
        public override void OnInventoryUse(InventorySortData sortData)
        {
            Utils.SetCursorInteract(Console.CursorTop);
            Utils.WriteColour($"You gag and endure the pungent sewer water.", ConsoleColor.DarkYellow);
            PlayerEntity.Instance.PotionStatusEffect = new DefenseStatusEffect
                ("Shite Froth", 4, ConsoleColor.DarkYellow,
                propertyEffectLevel: CombatData.EffectLevel.Moderate,
                propertyDamageType: CombatData.PropertyDamageType.Occult);
            Stock--;
            Utils.SetCursorInteract(Console.CursorTop - 1);
            Utils.WriteColour("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
