using System;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    class WeaponMaterial
    {
        static WeaponMaterial weaponMaterialIron = new WeaponMaterial("Iron", Material.Iron, 2, 4);
        static WeaponMaterial weaponMaterialSteel = new WeaponMaterial("Steel", Material.Steel, 4, 6);
        static WeaponMaterial weaponMaterialMoonstone = new WeaponMaterial("Moonstone", Material.Moonstone, 6, 8);
        static WeaponMaterial weaponMaterialHardshite = new WeaponMaterial("Hardshite", Material.Hardshite, 6, 8);

        string name;
        Material material;
        int minDamage;
        int maxDamage;

        public WeaponMaterial(string name, Material material, int minDamage, int maxDamage)
        {
            this.name = name;
            this.material = material;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
        }
    }

    enum Material
    {
        Iron,
        Steel,
        Moonstone,
        Hardshite
    }
}
