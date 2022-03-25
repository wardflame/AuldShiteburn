using System;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    internal class WeaponItem : Item
    {
        public string Name
        {
            get { return $"{Property.Name} {Material.Name} {Category.Name}"; }
        }
        public WeaponCategory Category { get; set; }
        public WeaponMaterial Material { get; set; }
        public WeaponProperty Property { get; set; }
        public static WeaponItem GenerateWeapon()
        {
            WeaponItem weapon = new WeaponItem();
            weapon.Category = WeaponCategory.Longsword;
            weapon.Material = WeaponMaterial.WeaponMaterialSteel;
            weapon.Property = WeaponProperty.WeaponPropertyStandard;
            return weapon;
        }
    }
}
