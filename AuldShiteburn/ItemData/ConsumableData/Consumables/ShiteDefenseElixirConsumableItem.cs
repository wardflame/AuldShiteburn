using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    internal class ShiteDefenseElixirConsumableItem : ConsumableItem
    {
        public override string Name => "Shite Froth";
        public override string Description => "Applies the Shite Froth potion status effect, providing Occult defense.";
        public int MinHeal { get; } = 4;
        public int MaxHeal { get; } = 8;
        public override void OnInventoryUse(InventorySortData sortData)
        {
            Utils.SetCursorInteract();
            Utils.WriteColour($"You gag but take back the pungent sewer water.", ConsoleColor.DarkYellow);
            PlayerEntity.Instance.PotionStatusEffect = new DefenseStatusEffect
                ("Shite Froth", 4, ConsoleColor.DarkYellow,
                propertyEffectLevel: CombatData.EffectLevel.Moderate,
                propertyDamageType: CombatData.PropertyDamageType.Occult);
        }
    }
}
