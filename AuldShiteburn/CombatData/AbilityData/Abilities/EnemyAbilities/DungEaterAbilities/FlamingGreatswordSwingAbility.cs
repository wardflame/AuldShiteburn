using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.DungEaterAbilities
{
    [Serializable]
    internal class FlamingGreatswordSwingAbility : Ability
    {
        public override string Name => "Flaming Greatsword Swing";
        public override string Description => $"swings its flaming greatsword violently in a flurry of blows.";
        public override int Cooldown => 5;
        public override int ResourceCost => 0;
        public override PhysicalDamageType PhysicalDamageType => PhysicalDamageType.Slash;
        public override int PhysicalMinDamage => 6;
        public override int PhysicalMaxDamage => 12;
        public override PropertyDamageType PropertyDamageType => PropertyDamageType.Fire;
        public override int PropertyMinDamage => 4;
        public override int PropertyMaxDamage => 6;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown <= 0)
            {
                Random rand = new Random();
                int physDamage = rand.Next(PhysicalMinDamage, PhysicalMaxDamage + 1);
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
