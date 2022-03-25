using System;
using System.Collections.Generic;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    class WeaponMaterial
    {
        #region Weapon Materials
        public static WeaponMaterial WeaponMaterialIron
        {
            get { return new WeaponMaterial("Iron", WeaponMaterialType.Iron, 1, 2); }
        }
        public static WeaponMaterial WeaponMaterialSteel
        {
            get { return new WeaponMaterial("Steel", WeaponMaterialType.Steel, 2, 3); }
        }
        public static WeaponMaterial WeaponMaterialMoonstone
        {
            get { return new WeaponMaterial("Moonstone", WeaponMaterialType.Moonstone, 4, 6); }
        }
        public static WeaponMaterial WeaponMaterialHardshite
        {
            get { return new WeaponMaterial("Hardshite", WeaponMaterialType.Hardshite, 4, 6); }
        }
        #endregion Weapon Materials

        public static List<WeaponMaterial> WeaponMaterials = new List<WeaponMaterial>()
        {
            WeaponMaterialIron,
            WeaponMaterialSteel,
            WeaponMaterialMoonstone,
            WeaponMaterialHardshite
        };
        public string Name { get; }
        public WeaponMaterialType Material { get; }
        public int MinDamage { get; }
        public int MaxDamage { get; }

        public WeaponMaterial(string name, WeaponMaterialType material, int minDamage, int maxDamage)
        {
            Name = name;
            Material = material;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }
}
