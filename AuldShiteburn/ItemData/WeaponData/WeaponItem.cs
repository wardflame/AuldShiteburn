using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData.PlayerData;
using System;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    internal class WeaponItem : Item
    {
        public override string Name
        {
            get
            {
                if (Property.Property == PropertyDamageType.Standard)
                {
                    return $"{Material.Name} {Category.Name}";
                }
                else
                {
                    return $"{Property.Name} {Material.Name} {Category.Name}";
                }
            }
        }
        public WeaponType Category { get; set; }
        public WeaponMaterial Material { get; set; }
        public WeaponProperty Property { get; set; }

        /// <summary>
        /// Randomly generate a weapon by category, material and property.
        /// </summary>
        /// <returns>Random-generated weapon.</returns>
        public static WeaponItem GenerateWeapon()
        {
            WeaponItem weapon = new WeaponItem();
            Random rand = new Random();
            weapon.Category = WeaponType.AllWeaponTypes[rand.Next(0, WeaponType.AllWeaponTypes.Count)];

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
                weapon.Property = WeaponProperty.WeaponPropertyRuined;
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
                        weapon.Category = WeaponType.PrimitiveWeaponTypes[rand.Next(WeaponType.PrimitiveWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Fighter:
                    {
                        weapon.Category = WeaponType.MartialWeaponTypes[rand.Next(WeaponType.MartialWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Marauder:
                    {
                        weapon.Category = WeaponType.StrengthWeaponTypes[rand.Next(WeaponType.StrengthWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Monk:
                    {
                        weapon.Category = WeaponType.PrimitiveWeaponTypes[rand.Next(WeaponType.PrimitiveWeaponTypes.Count)];
                    }
                    break;
                case ClassType.Rogue:
                    {
                        weapon.Category = WeaponType.DextrousWeaponTypes[rand.Next(WeaponType.DextrousWeaponTypes.Count)];
                    }
                    break;
            }
            return weapon;
        }
    }
}
