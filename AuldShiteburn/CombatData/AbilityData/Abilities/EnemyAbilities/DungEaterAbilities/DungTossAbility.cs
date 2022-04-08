using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.DungEaterAbilities
{
    internal class DungTossAbility : Ability
    {
        public override string Name => "Dung Toss";
        public override string Description => $"hurls a ball of dung at {PlayerEntity.Instance.Name}!";
        public override int Cooldown => 7;
        public override int ResourceCost => 0;
        public override PhysicalDamageType PhysicalDamageType => PhysicalDamageType.Strike;
        public override PropertyDamageType PropertyDamageType => PropertyDamageType.Occult;
        public override int PhysicalMinDamage => 6;
        public override int PhysicalMaxDamage => 9;
        public override int PropertyMinDamage => 3;
        public override int PropertyMaxDamage => 5;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies)
        {
            Random rand = new Random();
            int physDamage = rand.Next(PhysicalMinDamage, PhysicalMaxDamage + 1);
            int propDamage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
            return new CombatPayload(
                isAttack: true, isStun: true,
                hasPhysical: true, hasProperty: true,
                physicalAttackType: PhysicalDamageType, propertyAttackType: PropertyDamageType,
                physicalDamage: physDamage, propertyDamage: propDamage,
                stunCount: 1);
        }
    }
}
