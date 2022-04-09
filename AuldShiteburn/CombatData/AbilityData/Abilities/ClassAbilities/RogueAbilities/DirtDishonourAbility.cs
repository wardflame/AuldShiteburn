using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.RogueAbilities
{
    [Serializable]
    internal class DirtDishonourAbility : Ability
    {
        public override string Name => "Dirt and Dishonour";
        public override string Description => "Hurl dirt at an enemy and attack, applying Staggered and Stunned for 1 round.";
        public override int Cooldown => 3;
        public override int ResourceCost => 4;
        public override PhysicalDamageType PhysicalDamageType => PlayerEntity.Instance.EquippedWeapon.Type.PrimaryAttack;
        public override int PhysicalMinDamage => PlayerEntity.Instance.EquippedWeapon.MinPhysDamage;
        public override int PhysicalMaxDamage => PlayerEntity.Instance.EquippedWeapon.MaxPhysDamage;
        public override PropertyDamageType PropertyDamageType => PlayerEntity.Instance.EquippedWeapon.Property.Type;
        public override int PropertyMinDamage => PlayerEntity.Instance.EquippedWeapon.MinPropDamage;
        public override int PropertyMaxDamage => PlayerEntity.Instance.EquippedWeapon.MaxPropDamage;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (PlayerEntity.Instance.EquippedWeapon == null)
            {
                Utils.WriteColour($"No weapon to use this ability.", ConsoleColor.Red);
                Console.ReadKey(true);
                return new CombatPayload(false);
            }
            else if (ActiveCooldown <= 0)
            {
                PlayerEntity.Instance.Stamina -= ResourceCost;
                Random rand = new Random();
                int physDamage = rand.Next(PhysicalMinDamage, PhysicalMaxDamage + 1);
                int propDamage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
                ActiveCooldown = Cooldown;
                return new CombatPayload(
                    isAttack: true, isStun: true,
                    hasStatus: true, hasPhysical: true, hasProperty: true,
                    statusEffect: DefenseStatusEffect.Staggered1,
                    physicalAttackType: PhysicalDamageType, physicalDamage: physDamage,
                    propertyAttackType: PropertyDamageType, propertyDamage: propDamage,
                    stunCount: 1);
            }
            return new CombatPayload(false);
        }
    }
}
