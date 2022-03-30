using System;

namespace AuldShiteburn.CombatData
{
    [Serializable]
    public struct DamagePayload
    {
        public int physicalDamage;
        public int propertyDamage;
        public PhysicalDamageType physicalDamageType;
        public PropertyDamageType propertyDamageType;
        public bool IsDamageAttack => physicalDamage + propertyDamage > 0;

        public DamagePayload(int physDamage, int propDamage, PhysicalDamageType physDamageType, PropertyDamageType propertyDamageType = PropertyDamageType.None)
        {
            physicalDamage = physDamage;
            propertyDamage = propDamage;
            this.physicalDamageType = physDamageType;
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
