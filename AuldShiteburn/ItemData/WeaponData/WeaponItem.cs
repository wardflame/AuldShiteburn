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
        public WeaponCategory Category { get; set; }
        public WeaponMaterial Material { get; set; }
        public WeaponProperty Property { get; set; }

        public static WeaponItem GenerateWeapon()
        {
            WeaponItem weapon = new WeaponItem();
            Random rand = new Random();
            weapon.Category = WeaponCategory.AllWeaponCategories[rand.Next(0, WeaponCategory.AllWeaponCategories.Count)];
            int chance = rand.Next(1, 101);

            #region Material Generation
            if (chance <= 20)
            {
                weapon.Material = WeaponMaterial.WeaponMaterials[rand.Next(3, WeaponMaterial.WeaponMaterials.Count)];
            }
            else if (chance <= 40)
            {
                weapon.Material = WeaponMaterial.WeaponMaterialSteel;
            }
            else
            {
                weapon.Material = WeaponMaterial.WeaponMaterialIron;
            }
            #endregion Material Generation

            #region Property Generation
            chance = rand.Next(1, 101);
            if (chance <= 8)
            {
                weapon.Property = WeaponProperty.WeaponPropertyRuined;
            }
            else if (chance <= 15)
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

        public static WeaponItem GenerateSpawnWeapon(ClassType playerClass)
        {
            WeaponItem weapon = GenerateWeapon();
            Random rand = new Random();
            switch (playerClass)
            {
                case ClassType.Acolyte:
                    {
                        weapon.Category = WeaponCategory.PrimitiveWeapons[rand.Next(WeaponCategory.PrimitiveWeapons.Count)];
                    }
                    break;
                case ClassType.Fighter:
                    {
                        weapon.Category = WeaponCategory.MartialWeapons[rand.Next(WeaponCategory.MartialWeapons.Count)];
                    }
                    break;
                case ClassType.Marauder:
                    {
                        weapon.Category = WeaponCategory.StrengthWeapons[rand.Next(WeaponCategory.StrengthWeapons.Count)];
                    }
                    break;
                case ClassType.Monk:
                    {
                        weapon.Category = WeaponCategory.PrimitiveWeapons[rand.Next(WeaponCategory.PrimitiveWeapons.Count)];
                    }
                    break;
                case ClassType.Rogue:
                    {
                        weapon.Category = WeaponCategory.DextrousWeapons[rand.Next(WeaponCategory.DextrousWeapons.Count)];
                    }
                    break;
            }
            return weapon;
        }
    }
}
