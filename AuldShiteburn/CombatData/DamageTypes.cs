using System;

namespace AuldShiteburn.CombatData
{
    [Serializable]
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
