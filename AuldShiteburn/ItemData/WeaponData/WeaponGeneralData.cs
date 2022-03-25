using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    enum WeaponFamily
    {
        DextrousSmallArms,
        PrimitiveArms,
        MartialArms,
        StrengthLargeArms
    }
    enum DamageType
    {
        None,
        Strike,
        Slash,
        Pierce
    }
    enum WeaponMaterialType
    {
        Iron,
        Steel,
        Moonstone,
        Hardshite
    }
    enum WeaponPropertyType
    {
        Ruined,
        Standard,
        Fire,
        Cold,
        Holy,
        Shitty
    }
}
