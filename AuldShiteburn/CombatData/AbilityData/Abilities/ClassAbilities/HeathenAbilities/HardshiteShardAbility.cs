using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.HeathenAbilities
{
    [Serializable]
    internal class HardshiteShardAbility : Ability
    {
        public override string Name => "Hardshite Shard";
        public override string Description => $"Shoots a shard of hardshite at the target for {PropertyMinDamage} - {PropertyMaxDamage} Occult damage.";
        public override int Cooldown => 2;
        public override int ResourceCost => 3;
        public override PhysicalDamageType PhysicalDamageType => PhysicalDamageType.Pierce;
        public override int PhysicalMinDamage => 4;
        public override int PhysicalMaxDamage => 7;
        public override PropertyDamageType PropertyDamageType => PropertyDamageType.Occult;
        public override int PropertyMinDamage => 3;
        public override int PropertyMaxDamage => 5;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown <= 0)
            {
                PlayerEntity.Instance.Mana -= ResourceCost;
                Random rand = new Random();
                int physDamage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
                int propDamage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
                ActiveCooldown = Cooldown;
                return new CombatPayload(
                    isAttack: true,
                    hasPhysical: true, hasProperty: true,
                    physicalAttackType: PhysicalDamageType, propertyAttackType: PropertyDamageType,
                    physicalDamage: physDamage, propertyDamage: propDamage);
            }
            return new CombatPayload(false);
        }
    }
}
