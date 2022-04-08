using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.DungEaterAbilities
{
    internal class ClawedSwipeAbility : Ability
    {
        public override string Name => "Clawed Swipe";
        public override string Description => $"swipes {PlayerEntity.Instance.Name} with sharp claws!";
        public override int Cooldown => 0;
        public override int ResourceCost => 0;
        public override PhysicalDamageType PhysicalDamageType => PhysicalDamageType.Slash;
        public override int PhysicalMinDamage => 5;
        public override int PhysicalMaxDamage => 7;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies)
        {
            Random rand = new Random();
            int physDamage = rand.Next(PhysicalMinDamage, PhysicalMaxDamage + 1);
            return new CombatPayload(
                isAttack: true,
                hasPhysical: true,
                physicalAttackType: PhysicalDamageType,
                physicalDamage: physDamage);
        }
    }
}
