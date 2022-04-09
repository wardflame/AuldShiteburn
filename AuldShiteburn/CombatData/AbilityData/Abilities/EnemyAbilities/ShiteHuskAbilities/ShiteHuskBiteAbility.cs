using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.ShiteHuskAbilities
{
    [Serializable]
    internal class ShiteHuskBiteAbility : Ability
    {
        public override string Name => "Shitehusk Bite";
        public override string Description => $"bites at {PlayerEntity.Instance.Name} with shite-covered fangs!";
        public override int Cooldown => 0;
        public override int ResourceCost => 0;
        public override PhysicalDamageType PhysicalDamageType => PhysicalDamageType.Pierce;
        public override PropertyDamageType PropertyDamageType => PropertyDamageType.Occult;
        public override int PhysicalMinDamage => 3;
        public override int PhysicalMaxDamage => 4;
        public override int PropertyMinDamage => 2;
        public override int PropertyMaxDamage => 3;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies)
        {
            Random rand = new Random();
            int physDamage = rand.Next(PhysicalMinDamage, PhysicalMaxDamage + 1);
            int propDamage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
            return new CombatPayload(
                isAttack: true,
                hasPhysical: true, hasProperty: true,
                physicalAttackType: PhysicalDamageType, propertyAttackType: PropertyDamageType,
                physicalDamage: physDamage, propertyDamage: propDamage);
        }
    }
}
