using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    class WeaponProperty
    {
        #region Weapon Properties
        public static WeaponProperty WeaponPropertyDamaged
        {
            get { return new WeaponProperty("Damaged", PropertyDamageType.Damaged, ClassType.None, -2, -1); }
        }
        public static WeaponProperty WeaponPropertyStandard
        {
            get { return new WeaponProperty("Standard", PropertyDamageType.Standard, ClassType.Fighter, 0, 0); }
        }
        public static WeaponProperty WeaponPropertyFlaming
        {
            get { return new WeaponProperty("Flaming", PropertyDamageType.Fire, ClassType.Marauder, 2, 4); }
        }
        public static WeaponProperty WeaponPropertyCold
        {
            get { return new WeaponProperty("Cold", PropertyDamageType.Cold, ClassType.Rogue, 2, 4); }
        }
        public static WeaponProperty WeaponPropertyHoly
        {
            get { return new WeaponProperty("Holy", PropertyDamageType.Holy, ClassType.Monk, 4, 6); }
        }
        public static WeaponProperty WeaponPropertyShitty
        {
            get { return new WeaponProperty("Shite-slick", PropertyDamageType.Occult, ClassType.Heathen, 4, 6); }
        }
        #endregion Weapon Properties

        public static List<WeaponProperty> WeaponProperties
        {
            get
            {
                return new List<WeaponProperty>()
                {
                    WeaponPropertyDamaged,
                    WeaponPropertyStandard,
                    WeaponPropertyFlaming,
                    WeaponPropertyCold,
                    WeaponPropertyHoly,
                    WeaponPropertyShitty
                };
            }
        }
        public string Name { get; }
        public PropertyDamageType Type { get; }
        private int minDamage, maxDamage;
        public int MinDamage
        {
            get
            {
                if (HasAffinity)
                {
                    return minDamage += Combat.PROFICIENCY_DAMAGE_BONUS_MINOR;
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
                    return maxDamage += Combat.PROFICIENCY_DAMAGE_BONUS_MINOR;
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
                return PlayerEntity.Instance.Class.Proficiencies.PropertyAffinity == Type;
            }
        }

        public WeaponProperty(string name, PropertyDamageType property, ClassType proficientClass, int minDamage, int maxDamage)
        {
            Name = name;
            Type = property;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }
}
