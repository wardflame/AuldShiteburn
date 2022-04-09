using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.BloatedBruiserAbilities
{
    [Serializable]
    internal class BruiserPunchAbility : Ability
    {
        public override string Name => "Bruiser Punch";
        public override string Description => $"delivers a brutal punch to {PlayerEntity.Instance.Name}!";
        public override int Cooldown => 0;
        public override int ResourceCost => 0;
        public override PhysicalDamageType PhysicalDamageType => PhysicalDamageType.Strike;
        public override int PhysicalMinDamage => 5;
        public override int PhysicalMaxDamage => 7;

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
