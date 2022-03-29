using System;

namespace AuldShiteburn.CombatData
{
    [Serializable]
    public struct Damage
    {
        public int damage;
        public PhysicalDamageType physDamageType;
        public PropertyDamageType propertyDamageType;

        public Damage(int damage, PhysicalDamageType physDamageType, PropertyDamageType propertyDamageType = PropertyDamageType.None)
        {
            this.damage = damage;
            this.physDamageType = physDamageType;
            this.propertyDamageType = propertyDamageType;
        }
    }

    public enum PhysicalDamageType
    {
        None,
        Slash,
        Strike,
        Pierce,
    }

    public enum PropertyDamageType
    {
        None,
        Damaged,
        Standard,
        Fire,
        Cold,
        Holy,
        Occult
    }
}
