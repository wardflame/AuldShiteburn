using AuldShiteburn.EntityData;
using System;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    class WeaponCategory
    {
        #region DextrousWeapons
        public static WeaponCategory Dagger
        {
            get { return new WeaponCategory("Dagger", WeaponFamily.DextrousSmallArms, DamageType.Pierce, 3, 5); }
        }
        public static WeaponCategory Rapier
        {
            get { return new WeaponCategory("Rapier", WeaponFamily.DextrousSmallArms, DamageType.Pierce, 3, 5); }
        }
        public static WeaponCategory Shortsword
        {
            get { return new WeaponCategory("Shortsword", WeaponFamily.DextrousSmallArms, DamageType.Slash, 3, 5); }
        }
        #endregion DextrousWeapons
        #region PrimitiveWeapons
        public static WeaponCategory HandAxe
        {
            get { return new WeaponCategory("Greatsword", WeaponFamily.PrimitiveArms, DamageType.Slash, 4, 6); }
        }
        public static WeaponCategory Cudgel
        {
            get { return new WeaponCategory("Greatsword", WeaponFamily.PrimitiveArms, DamageType.Strike, 4, 6); }
        }
        public static WeaponCategory Spear
        {
            get { return new WeaponCategory("Spear", WeaponFamily.PrimitiveArms, DamageType.Pierce, 4, 6); }
        }
        #endregion PrimitiveWeapons
        #region MartialWeapons
        public static WeaponCategory Longsword
        {
            get { return new WeaponCategory("Longsword", WeaponFamily.MartialArms, DamageType.Slash, 5, 7); }
        }
        public static WeaponCategory BattleAxe
        {
            get { return new WeaponCategory("Battle Axe", WeaponFamily.MartialArms, DamageType.Slash, 5, 7); }
        }
        public static WeaponCategory Mace
        {
            get { return new WeaponCategory("Mace", WeaponFamily.MartialArms, DamageType.Strike, 5, 7); }
        }
        public static WeaponCategory Warhammer
        {
            get { return new WeaponCategory("Warhammer", WeaponFamily.MartialArms, DamageType.Strike, 5, 7); }
        }
        #endregion MartialWeapons
        #region StrengthWeapons
        public static WeaponCategory Greatsword
        {
            get { return new WeaponCategory("Greatsword", WeaponFamily.StrengthLargeArms, DamageType.Slash, 7, 9); }
        }
        public static WeaponCategory Greataxe
        {
            get { return new WeaponCategory("Greataxe", WeaponFamily.StrengthLargeArms, DamageType.Slash, 7, 9); }
        }
        public static WeaponCategory Greathammer
        {
            get { return new WeaponCategory("Greathammer", WeaponFamily.StrengthLargeArms, DamageType.Strike, 7, 9); }
        }
        #endregion StrengthWeapons

        public string Name { get; }
        public WeaponFamily Family { get; }
        public DamageType PrimaryAttack { get; }
        public DamageType SecondaryAttack { get; }
        public int MinDamage
        {
            get
            {
                int damageBonus = 0;
                if (Proficient)
                {
                    damageBonus += 2;
                }
                return MinDamage + damageBonus;
            }
            private set
            {
                MinDamage = value;
            }
        }
        public int MaxDamage
        {
            get
            {
                int damageBonus = 0;
                if (Proficient)
                {
                    damageBonus += 2;
                }
                return MinDamage + damageBonus;
            }
            private set
            {
                MaxDamage =  value;
            }
        }
        public bool Proficient
        {
            get
            {
                if (PlayerEntity.Instance.Class.WeaponProficiency == Family)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public WeaponCategory(string name, WeaponFamily family, DamageType primaryAttack, int minDamage, int maxDamage, DamageType secondaryAttack = DamageType.None)
        {
            Name = name;
            Family = family;
            PrimaryAttack = primaryAttack;
            SecondaryAttack = secondaryAttack;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }
}
