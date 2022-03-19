using System;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    class WeaponProperty
    {
        static WeaponProperty weaponPropertyFlaming = new WeaponProperty("Flaming", Property.Fire, 4, 6);
        static WeaponProperty weaponPropertyCold = new WeaponProperty("Cold", Property.Cold, 4, 6);
        static WeaponProperty weaponPropertyHoly = new WeaponProperty("Holy", Property.Holy, 4, 6);
        static WeaponProperty weaponPropertyShitty = new WeaponProperty("Shitty", Property.Shitty, 4, 6);

        string name;
        Property property;
        int minDamage;
        int maxDamage;

        public WeaponProperty(string name, Property property, int minDamage, int maxDamage)
        {
            this.name = name;
            this.property = property;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
        }
    }

    enum Property
    {
        None,
        Fire,
        Cold,
        Holy,
        Shitty
    }
}
