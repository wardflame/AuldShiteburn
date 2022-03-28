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
        public static WeaponProperty WeaponPropertyRuined
        {
            get { return new WeaponProperty("Ruined", PropertyDamageType.Damaged, ClassType.None, -1, -3); }
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
            get { return new WeaponProperty("Shitty", PropertyDamageType.Occult, ClassType.Heathen, 4, 6); }
        }
        #endregion Weapon Properties

        public static List<WeaponProperty> WeaponProperties
        {
            get
            {
                return new List<WeaponProperty>()
                {
                    WeaponPropertyRuined,
                    WeaponPropertyStandard,
                    WeaponPropertyFlaming,
                    WeaponPropertyCold,
                    WeaponPropertyHoly,
                    WeaponPropertyShitty
                };
            }
        }
        public string Name { get; }
        public PropertyDamageType Property { get; }
        public ClassType ProficientClass { get; }
        private int minDamage, maxDamage;
        public int MinDamage
        {
            get
            {
                if (PlayerEntity.Instance.Class.ClassType == ProficientClass)
                {
                    return minDamage += Combat.PROFICIENCY_DAMAGE_MODIFIER;
                }
                return minDamage;
            }
            protected set
            {
                minDamage = value;
            }
        }
        public int MaxDamage
        {
            get
            {
                if (PlayerEntity.Instance.Class.ClassType == ProficientClass)
                {
                    return maxDamage += Combat.PROFICIENCY_DAMAGE_MODIFIER;
                }
                return maxDamage;
            }
            protected set
            {
                maxDamage = value;
            }
        }

        public WeaponProperty(string name, PropertyDamageType property, ClassType proficientClass, int minDamage, int maxDamage)
        {
            Name = name;
            Property = property;
            ProficientClass = proficientClass;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }
}
