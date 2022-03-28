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
            get { return new WeaponMaterial("Iron", GeneralMaterials.Iron, 1, 2); }
        }
        public static WeaponMaterial WeaponMaterialSteel
        {
            get { return new WeaponMaterial("Steel", GeneralMaterials.Steel, 2, 3); }
        }
        public static WeaponMaterial WeaponMaterialMoonstone
        {
            get { return new WeaponMaterial("Moonstone", GeneralMaterials.Moonstone, 4, 6); }
        }
        public static WeaponMaterial WeaponMaterialHardshite
        {
            get { return new WeaponMaterial("Hardshite", GeneralMaterials.Hardshite, 4, 6); }
        }
        #endregion Weapon Materials

        public static List<WeaponMaterial> WeaponMaterialList
        {
            get
            {
                return new List<WeaponMaterial>()
                    {
                        WeaponMaterialIron,
                        WeaponMaterialSteel,
                        WeaponMaterialMoonstone,
                        WeaponMaterialHardshite
                    };
            }
        }
        public string Name { get; }
        public GeneralMaterials Material { get; }
        public int MinDamage { get; }
        public int MaxDamage { get; }

        public WeaponMaterial(string name, GeneralMaterials material, int minDamage, int maxDamage)
        {
            Name = name;
            Material = material;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }
}
