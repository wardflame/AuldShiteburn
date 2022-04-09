using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.GrandWarlockAbilities
{
    internal class HardshiteBoltAbility : Ability
    {
        public override string Name => "Hardshite Bolt";
        public override string Description => $"hurls a bolt of hardshite!";
        public override int Cooldown => 0;
        public override int ResourceCost => 0;
        public override PhysicalDamageType PhysicalDamageType => PhysicalDamageType.Pierce;
        public override int PhysicalMinDamage => 5;
        public override int PhysicalMaxDamage => 9;
        public override PropertyDamageType PropertyDamageType => PropertyDamageType.Occult;
        public override int PropertyMinDamage => 4;
        public override int PropertyMaxDamage => 6;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown <= 0)
            {
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
