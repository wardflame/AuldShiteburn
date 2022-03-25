using System;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    class WeaponProperty
    {
        public static WeaponProperty WeaponPropertyRuined
        {
            get { return new WeaponProperty("Ruined", WeaponPropertyType.Ruined, -1, -3); }
        }
        public static WeaponProperty WeaponPropertyStandard
        {
            get { return new WeaponProperty("Standard", WeaponPropertyType.Standard, 0, 0); }
        }
        public static WeaponProperty WeaponPropertyFlaming
        {
            get { return new WeaponProperty("Flaming", WeaponPropertyType.Fire, 2, 4); }
        }
        public static WeaponProperty WeaponPropertyCold
        {
            get { return new WeaponProperty("Cold", WeaponPropertyType.Cold, 2, 4); }
        }
        public static WeaponProperty WeaponPropertyHoly
        {
            get { return new WeaponProperty("Holy", WeaponPropertyType.Holy, 4, 6); }
        }
        public static WeaponProperty WeaponPropertyShitty
        {
            get { return new WeaponProperty("Shitty", WeaponPropertyType.Shitty, 4, 6); }
        }

        public string Name { get; }
        public WeaponPropertyType Property { get; }
        public int MinDamage { get; }
        public int MaxDamage { get; }

        public WeaponProperty(string name, WeaponPropertyType property, int minDamage, int maxDamage)
        {
            Name = name;
            Property = property;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }
}
