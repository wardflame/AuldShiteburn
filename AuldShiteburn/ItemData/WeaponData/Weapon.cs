using System;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    internal class Weapon : Item
    {
        public Weapon GenerateWeapon()
        {
            Weapon weapon = new Weapon();
            return weapon;
        }
    }
}
