using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData.Classes;
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
            get { return new WeaponMaterial("Iron", GeneralMaterials.Iron, 0, 1); }
        }
        public static WeaponMaterial WeaponMaterialSteel
        {
            get { return new WeaponMaterial("Steel", GeneralMaterials.Steel, 1, 2); }
        }
        public static WeaponMaterial WeaponMaterialMoonstone
        {
            get { return new WeaponMaterial("Moonstone", GeneralMaterials.Moonstone, 3, 5); }
        }
        public static WeaponMaterial WeaponMaterialHardshite
        {
            get { return new WeaponMaterial("Hardshite", GeneralMaterials.Hardshite, 3, 5); }
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
        private int minDamage, maxDamage;
        public int MinDamage
        {
            get
            {
                if (HasAffinity)
                {
                    if (PlayerEntity.Instance.Class.GetType() == typeof(FighterClass) && Name == WeaponMaterialSteel.Name)
                    {
                        return maxDamage + Combat.PROFICIENCY_ARMOUR_MITIGATION_MODERATE;
                    }
                    else
                    {
                        return maxDamage + Combat.PROFICIENCY_DAMAGE_BONUS_MINOR;
                    }
                }
                return minDamage;
            }
            private set
            {
                minDamage = value;
            }
        }
        public int MaxDamage
        {
            get
            {
                if (HasAffinity)
                {
                    if (PlayerEntity.Instance.Class.GetType() == typeof(FighterClass) && Name == WeaponMaterialSteel.Name)
                    {
                        return maxDamage + Combat.PROFICIENCY_ARMOUR_MITIGATION_MODERATE;
                    }
                    else
                    {
                        return maxDamage + Combat.PROFICIENCY_DAMAGE_BONUS_MINOR;
                    }
                }
                return maxDamage;
            }
            private set
            {
                maxDamage = value;
            }
        }
        public bool HasAffinity
        {
            get
            {
                return PlayerEntity.Instance.Class.Proficiencies.MaterialAffinity == Material;
            }
        }

        public WeaponMaterial(string name, GeneralMaterials material, int minDamage, int maxDamage)
        {
            Name = name;
            Material = material;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }
}
