using System;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    class WeaponProperty
    {
        public static WeaponProperty weaponPropertyStandard = new WeaponProperty("Flaming", Property.Standard, 0, 0, 0.9f);
        public static WeaponProperty weaponPropertyFlaming = new WeaponProperty("Flaming", Property.Fire, 4, 6, 0.9f);
        public static WeaponProperty weaponPropertyCold = new WeaponProperty("Cold", Property.Cold, 4, 6, 0.9f);
        public static WeaponProperty weaponPropertyHoly = new WeaponProperty("Holy", Property.Holy, 4, 6, 0.95f);
        public static WeaponProperty weaponPropertyShitty = new WeaponProperty("Shitty", Property.Shitty, 4, 6, 0.98f);
        public static WeaponProperty weaponPropertyRuined = new WeaponProperty("Flaming", Property.Ruined, -1, -3, 0.9f);

        public string name;
        public Property property;
        public int minDamage;
        public int maxDamage;
        public float genChance;

        public WeaponProperty(string name, Property property, int minDamage, int maxDamage, float genChance)
        {
            this.name = name;
            this.property = property;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
            this.genChance = genChance;
        }
    }

    enum Property
    {
        Standard,
        Fire,
        Cold,
        Holy,
        Shitty,
        Ruined
    }
}
