using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    internal class ShiteElixirConsumableItem : ConsumableItem
    {
        public override string Name => "Shite Froth";
        public override string Description => "A foul, watery sip of shite water that provides Occult defense.";
        public int MinHeal { get; } = 4;
        public int MaxHeal { get; } = 8;
        public override void OnConsumption()
        {
            Random rand = new Random();
            int hpRestored = rand.Next(MinHeal, MaxHeal + 1);
            Utils.SetCursorInteract(Console.CursorTop);
            Utils.WriteColour($"You gag but take back the pungent sewer water.", ConsoleColor.DarkYellow);
            DefenseStatusEffect shiteFroth = new DefenseStatusEffect(propertyEffectLevel: CombatData.EffectLevel.Moderate, propertyDamageType: CombatData.PropertyDamageType.Occult);
            shiteFroth.Name = "Shite Froth";
            shiteFroth.Duration = 4;
            shiteFroth.DisplayColor = ConsoleColor.DarkYellow;
            PlayerEntity.Instance.StatusEffect = shiteFroth;
        }
    }
}
