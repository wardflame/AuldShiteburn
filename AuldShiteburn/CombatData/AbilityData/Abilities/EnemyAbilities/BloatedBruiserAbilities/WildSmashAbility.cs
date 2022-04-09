using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.BloatedBruiserAbilities
{
    [Serializable]
    internal class WildSmashAbility : Ability
    {
        public override string Name => "Wild Smash";
        public override string Description => $"flails wild, puss-spitting fists at {PlayerEntity.Instance.Name}!";
        public override int Cooldown => 5;
        public override int ResourceCost => 0;
        public override PhysicalDamageType PhysicalDamageType => PhysicalDamageType.Strike;
        public override int PhysicalMinDamage => 6;
        public override int PhysicalMaxDamage => 9;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            Random rand = new Random();
            int physDamage = rand.Next(PhysicalMinDamage, PhysicalMaxDamage + 1);
            return new CombatPayload(
                isAttack: true, isStun: true,
                hasPhysical: true,
                physicalAttackType: PhysicalDamageType,
                physicalDamage: physDamage,
                stunCount: 1);
        }
    }
}
