using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData
{
    [Serializable]
    public enum WeaponDamageType
    {
        None,
        Slash,
        Strike,
        Pierce,
    }

    public enum DamagePropertyType
    {
        Standard,
        Fire,
        Cold,
        Holy,
        Occult
    }
}
