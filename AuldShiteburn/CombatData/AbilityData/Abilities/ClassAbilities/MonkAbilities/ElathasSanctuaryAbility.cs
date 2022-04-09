using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.MonkAbilities
{
    [Serializable]
    internal class ElathasSanctuaryAbility : Ability
    {
        public override string Name => "Elatha's Sanctuary";
        public override string Description => "Restore 8-14 HP and gain Moon Ward status effect for 3 rounds, providing Moderate Property Mitigation.";
        public override int Cooldown => 4;
        public override int ResourceCost => 8;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown <= 0)
            {
                PlayerEntity.Instance.Mana -= ResourceCost;
                Random rand = new Random();
                int heal = rand.Next(8, 15);
                PlayerEntity.Instance.HP += heal;
                PlayerEntity.Instance.AbilityStatusEffect = DefenseStatusEffect.MoonWard;
                ActiveCooldown = Cooldown;
                return new CombatPayload(isAttack: false, isUtility: true);
            }
            return new CombatPayload(false);
        }
    }
}
