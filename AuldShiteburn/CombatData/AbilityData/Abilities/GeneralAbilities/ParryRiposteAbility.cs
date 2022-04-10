using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData;
using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.GeneralAbilities
{
    [Serializable]
    internal class ParryRiposteAbility : Ability
    {
        public override string Name => "Parry and Riposte";
        public override string Description => "Gain Major physical Mitigation and stagger enemies for 2 turns if they fail to do damage.";
        public override int Cooldown => 3;
        public override int ResourceCost => 4;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown == 0)
            {
                PlayerEntity.Instance.Stamina -= ResourceCost;
                PlayerEntity.Instance.AbilityStatusEffect = DefenseStatusEffect.ParryAndRiposte;
                ActiveCooldown = Cooldown;
                return new CombatPayload(isAttack: false, isUtility: true);
            }
            return new CombatPayload(false);
        }
    }
}
