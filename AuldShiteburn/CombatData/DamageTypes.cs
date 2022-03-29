using System;

namespace AuldShiteburn.CombatData
{
    [Serializable]
    public struct Damage
    {
        public int physicalDamage;
        public int propertyDamage;
        public PhysicalDamageType physDamageType;
        public PropertyDamageType propertyDamageType;

        public Damage(int physDamage, int propDamage, PhysicalDamageType physDamageType, PropertyDamageType propertyDamageType = PropertyDamageType.None)
        {
            physicalDamage = physDamage;
            propertyDamage = propDamage;
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
