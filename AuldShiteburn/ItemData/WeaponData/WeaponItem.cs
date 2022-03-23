using System;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    internal class WeaponItem : Item
    {
        public static WeaponItem GenerateWeapon()
        {
            WeaponItem weapon = new WeaponItem();
            return weapon;
        }
    }
}
