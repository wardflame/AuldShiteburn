using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData;
using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.HeathenAbilities
{
    [Serializable]
    internal class ShiteWardAbility : Ability
    {
        public override string Name => "Shite Ward";
        public override string Description => "Negates Occult damage and provides Minor Mitigation to all physical damage for 4 turns.";
        public override int Cooldown => 6;
        public override int ResourceCost => 4;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown == 0)
            {
                PlayerEntity.Instance.Mana -= ResourceCost;
                PlayerEntity.Instance.AbilityStatusEffect = new DefenseStatusEffect
                    ("Shite Ward", 4, ConsoleColor.DarkYellow, EffectType.Buff,
                    EffectLevel.Minor, allPhysicalDefense: true,
                    propertyDamageType: PropertyDamageType.Occult,
                    propertyNulOrMit: true);
                ActiveCooldown = Cooldown;
                return new CombatPayload(isAttack: false, isUtility: true);
            }
            return new CombatPayload(false);
        }
    }
}
