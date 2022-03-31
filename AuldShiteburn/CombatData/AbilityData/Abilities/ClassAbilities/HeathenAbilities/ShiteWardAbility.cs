using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.HeathenAbilities
{
    internal class ShiteWardAbility : Ability
    {
        public override string Name => "Shite Ward";

        public override string Description => "Negates Occult damage for two turns.";

        public override int Cooldown => 4;

        public override int ResourceCost => 4;

        public override int MinDamage => 0;

        public override int MaxDamage => 0;

        public override CombatPayload UseAbility()
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown > 0)
            {
                Utils.WriteColour($"{Name} is on cooldown {ActiveCooldown}/{Cooldown}.", ConsoleColor.DarkRed);
                ActiveCooldown--;
            }
            else if (ActiveCooldown <= 0)
            {
                ActiveCooldown = Cooldown;
                DefenseStatusEffect shiteWard = new DefenseStatusEffect(EffectLevel.Moderate, allPhysicalDefense: true, propertyDamageType: PropertyDamageType.Occult, propertyNulOrMit: true);
                shiteWard.Name = "Shite Ward";
                shiteWard.Duration = 4;
                shiteWard.DisplayColor = ConsoleColor.DarkYellow;
                PlayerEntity.Instance.StatusEffect = shiteWard;
                PlayerEntity.Instance.PrintStats();
                return new CombatPayload();
            }
            return new CombatPayload();
        }
    }
}
