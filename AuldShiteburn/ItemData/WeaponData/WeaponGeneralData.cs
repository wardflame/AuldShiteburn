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
    enum WeaponMaterialType
    {
        Iron,
        Steel,
        Moonstone,
        Hardshite
    }
    public enum WeaponPropertyType
    {
        Ruined,
        Standard,
        Fire,
        Cold,
        Holy,
        Occult
    }
}
