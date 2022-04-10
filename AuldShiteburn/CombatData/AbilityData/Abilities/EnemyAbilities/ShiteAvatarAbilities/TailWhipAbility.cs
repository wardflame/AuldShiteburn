using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.ShiteAvatarAbilities
{
    [Serializable]
    internal class TailWhipAbility : Ability
    {
        public override string Name => "Tail Whip";
        public override string Description => $"spins around and rapidly whips its tail!";
        public override int Cooldown => 1;
        public override int ResourceCost => 0;
        public override PhysicalDamageType PhysicalDamageType => PhysicalDamageType.Strike;
        public override int PhysicalMinDamage => 7;
        public override int PhysicalMaxDamage => 15;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
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
