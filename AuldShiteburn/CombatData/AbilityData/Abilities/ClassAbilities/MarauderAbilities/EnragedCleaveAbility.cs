using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.MarauderAbilities
{
    [Serializable]
    internal class EnragedCleaveAbility : Ability
    {
        public override string Name => "Enraged Cleave";
        public override string Description => $"Cleave all the enemies with your equipped weapon. Each kill returns 4 HP and 6 Stamina.";
        public override int Cooldown => 5;
        public override int ResourceCost => 5;
        public override PhysicalDamageType PhysicalDamageType => PlayerEntity.Instance.EquippedWeapon.Type.PrimaryAttack;
        public override int PhysicalMinDamage => PlayerEntity.Instance.EquippedWeapon.MinPhysDamage;
        public override int PhysicalMaxDamage => PlayerEntity.Instance.EquippedWeapon.MaxPhysDamage;
        public override PropertyDamageType PropertyDamageType => PlayerEntity.Instance.EquippedWeapon.Property.Type;
        public override int PropertyMinDamage => PlayerEntity.Instance.EquippedWeapon.MinPropDamage;
        public override int PropertyMaxDamage => PlayerEntity.Instance.EquippedWeapon.MaxPropDamage;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies)
        {
            int offsetY = Console.CursorTop + 2;
            Utils.SetCursorInteract(offsetY - 2);
            if (ActiveCooldown <= 0)
            {
                PlayerEntity.Instance.Stamina -= ResourceCost;
                Random rand = new Random();
                for (int i = 0; i < enemies.Count; i++)
                {
                    Utils.ClearInteractArea(offsetY, 10);
                    Utils.SetCursorInteract(offsetY - 2);
                    int physDamage = rand.Next(PhysicalMinDamage, PhysicalMaxDamage + 1);
                    int propDamage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
                    if (enemies[i].ReceiveAttack
                        (new CombatPayload(true, hasPhysical: true, hasProperty: true,
                        physicalAttackType: PhysicalDamageType, propertyAttackType: PropertyDamageType,
                        physicalDamage: physDamage, propertyDamage: propDamage), Console.CursorTop + 1))
                    {
                        PlayerEntity.Instance.HP += 4;
                        PlayerEntity.Instance.Stamina += 6;
                        enemies.Remove(enemies[i]);
                        i = -1;
                    }
                    Console.ReadKey(true);
                }
                ActiveCooldown = Cooldown;
                return new CombatPayload(false, true);
            }
            return new CombatPayload(false);
        }
    }
}
