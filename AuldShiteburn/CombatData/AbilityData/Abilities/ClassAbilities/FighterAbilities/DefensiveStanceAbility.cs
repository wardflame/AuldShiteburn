using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData;
using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.FighterAbilities
{
    [Serializable]
    internal class DefensiveStanceAbility : Ability
    {
        public override string Name => "Defensive Stance";
        public override string Description => "Gain Major physical Mitigation and stagger enemies for 1 turn if they fail to do damage.";
        public override int Cooldown => 3;
        public override int ResourceCost => 4;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown == 0)
            {
                PlayerEntity.Instance.Stamina -= ResourceCost;
                PlayerEntity.Instance.AbilityStatusEffect = new DefenseStatusEffect
                    ("Defensive Stance", 1, ConsoleColor.Cyan, EffectType.Buff,
                    EffectLevel.Major, allPhysicalDefense: true);
                ActiveCooldown = Cooldown;
                return new CombatPayload(false, true);
            }
            return new CombatPayload(false);
        }
    }
}
