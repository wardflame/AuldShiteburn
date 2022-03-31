using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    internal class WeaponItem : Item
    {
        public override string Name
        {
            get
            {
                if (Property.Type == PropertyDamageType.Standard)
                {
                    return $"{Material.Name} {Type.Name}";
                }
                else
                {
                    return $"{Property.Name} {Material.Name} {Type.Name}";
                }
            }
        }
        public int MinPhysDamage
        {
            get { return Type.MinDamage + Material.MinDamage; }
        }
        public int MaxPhysDamage
        {
            get { return Type.MaxDamage + Material.MaxDamage; }
        }
        public int MinPropDamage
        {
            get { return Property.MinDamage; }
        }
        public int MaxPropDamage
        {
            get { return Property.MaxDamage; }
        }
        public WeaponType Type { get; set; }
        public WeaponMaterial Material { get; set; }
        public WeaponProperty Property { get; set; }

        /// <summary>
        /// Equip weapon.
        /// </summary>
        public override void OnInventoryUse(InventorySortData sortData)
        {
            WeaponItem equippedWeapon = PlayerEntity.Instance.EquippedWeapon;
            PlayerEntity.Instance.Inventory.ItemList[sortData.index, sortData.typeColumn] = equippedWeapon;            
            PlayerEntity.Instance.EquippedWeapon = this;
        }

        /// <summary>
        /// Randomly generate a weapon by category, material and property.
        /// </summary>
        /// <returns>Random-generated weapon.</returns>
        public static WeaponItem GenerateWeapon()
        {
            WeaponItem weapon = new WeaponItem();
            Random rand = new Random();
            weapon.Type = WeaponType.AllWeaponTypes[rand.Next(0, WeaponType.AllWeaponTypes.Count)];

            #region Material Generation
            if (rand.Next(1, 101) <= 20)
            {
                weapon.Material = WeaponMaterial.WeaponMaterialList[rand.Next(3, WeaponMaterial.WeaponMaterialList.Count)];
            }
            else if (rand.Next(1, 101) <= 40)
            {
                weapon.Material = WeaponMaterial.WeaponMaterialSteel;
            }
            else
            {
                weapon.Material = WeaponMaterial.WeaponMaterialIron;
            }
            #endregion Material Generation

            #region Property Generation
            if (rand.Next(1, 101) <= 8)
            {
                weapon.Property = WeaponProperty.WeaponPropertyDamaged;
            }
            else if (rand.Next(1, 101) <= 15)
            {
                weapon.Property = WeaponProperty.WeaponProperties[rand.Next(2, WeaponProperty.WeaponProperties.Count)];
            }
            else
            {
                weapon.Property = WeaponProperty.WeaponPropertyStandard;
            }
            #endregion Property Generation

            return weapon;
        }

        /// <summary>
        /// For a new character, randomly generate a weapon and then ensure
        /// the type of weapon is one the character class is proficient in.
        /// </summary>
        /// <param name="playerClass">The class of the player.</param>
        /// <returns>A randomly generated weapon with a class-proficient type.</returns>
        public static WeaponItem GenerateSpawnWeapon(ClassType playerClass)
        {
            WeaponItem weapon = GenerateWeapon();
            Random rand = new Random();
            switch (playerClass)
            {
                case ClassType.Heathen:
                    {
                        weapon.Type = WeaponType.PrimitiveWeaponTypes[rand.Next(WeaponType.PrimitiveWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Fighter:
                    {
                        weapon.Type = WeaponType.MartialWeaponTypes[rand.Next(WeaponType.MartialWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Marauder:
                    {
                        weapon.Type = WeaponType.StrengthWeaponTypes[rand.Next(WeaponType.StrengthWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Monk:
                    {
                        weapon.Type = WeaponType.PrimitiveWeaponTypes[rand.Next(WeaponType.PrimitiveWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Rogue:
                    {
                        weapon.Type = WeaponType.DextrousWeaponTypes[rand.Next(WeaponType.DextrousWeaponTypes.Count)];
                    }
                    break;
            }
            return weapon;
        }
    }
}
