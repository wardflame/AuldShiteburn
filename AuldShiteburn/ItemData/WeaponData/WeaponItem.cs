using AuldShiteburn.CombatData;
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
            weapon.Category = WeaponCategory.WeaponCategories[rand.Next(0, WeaponCategory.WeaponCategories.Count)];
            int chance = rand.Next(1, 101);

            #region Material Generation
            if (chance <= 70)
            {
                weapon.Material = WeaponMaterial.WeaponMaterialIron;
            }
            else
            {
                weapon.Material = WeaponMaterial.WeaponMaterials[rand.Next(1, WeaponMaterial.WeaponMaterials.Count)];
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
    }
}
