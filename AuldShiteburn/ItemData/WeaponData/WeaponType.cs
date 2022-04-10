using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData.Classes;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    class WeaponType
    {
        #region DextrousWeapons
        public static WeaponType Dagger
        {
            get { return new WeaponType("Dagger", WeaponFamily.DextrousSmallArms, PhysicalDamageType.Pierce, 1, 3, PhysicalDamageType.Slash); }
        }
        public static WeaponType Rapier
        {
            get { return new WeaponType("Rapier", WeaponFamily.DextrousSmallArms, PhysicalDamageType.Pierce, 1, 3, PhysicalDamageType.Slash); }
        }
        public static WeaponType Shortsword
        {
            get { return new WeaponType("Shortsword", WeaponFamily.DextrousSmallArms, PhysicalDamageType.Slash, 1, 3, PhysicalDamageType.Pierce); }
        }
        #endregion DextrousWeapons
        #region PrimitiveWeapons
        public static WeaponType HandAxe
        {
            get { return new WeaponType("Hand Axe", WeaponFamily.PrimitiveArms, PhysicalDamageType.Slash, 2, 4, PhysicalDamageType.Strike); }
        }
        public static WeaponType Cudgel
        {
            get { return new WeaponType("Cudgel", WeaponFamily.PrimitiveArms, PhysicalDamageType.Strike, 2, 4); }
        }
        public static WeaponType Spear
        {
            get { return new WeaponType("Spear", WeaponFamily.PrimitiveArms, PhysicalDamageType.Pierce, 2, 4, PhysicalDamageType.Slash); }
        }
        #endregion PrimitiveWeapons
        #region MartialWeapons
        public static WeaponType Longsword
        {
            get { return new WeaponType("Longsword", WeaponFamily.MartialArms, PhysicalDamageType.Slash, 3, 5, PhysicalDamageType.Pierce); }
        }
        public static WeaponType BattleAxe
        {
            get { return new WeaponType("Battle Axe", WeaponFamily.MartialArms, PhysicalDamageType.Slash, 3, 5, PhysicalDamageType.Strike); }
        }
        public static WeaponType Mace
        {
            get { return new WeaponType("Mace", WeaponFamily.MartialArms, PhysicalDamageType.Strike, 3, 5); }
        }
        public static WeaponType Warhammer
        {
            get { return new WeaponType("Warhammer", WeaponFamily.MartialArms, PhysicalDamageType.Strike, 3, 5, PhysicalDamageType.Pierce); }
        }
        #endregion MartialWeapons
        #region StrengthWeapons
        public static WeaponType Greatsword
        {
            get { return new WeaponType("Greatsword", WeaponFamily.StrengthLargeArms, PhysicalDamageType.Slash, 5, 7, PhysicalDamageType.Pierce); }
        }
        public static WeaponType Greataxe
        {
            get { return new WeaponType("Greataxe", WeaponFamily.StrengthLargeArms, PhysicalDamageType.Slash, 5, 7, PhysicalDamageType.Strike); }
        }
        public static WeaponType Greathammer
        {
            get { return new WeaponType("Greathammer", WeaponFamily.StrengthLargeArms, PhysicalDamageType.Strike, 5, 7); }
        }
        #endregion StrengthWeapons
        public static WeaponType GodSword
        {
            get { return new WeaponType("God Sword", WeaponFamily.MartialArms, PhysicalDamageType.Slash, 1000, 1000, PhysicalDamageType.Pierce); }
        }

        public static List<WeaponType> AllWeaponTypes
        {
            get
            {
                return new List<WeaponType>()
                {
                    Dagger,
                    Rapier,
                    Shortsword,
                    HandAxe,
                    Cudgel,
                    Spear,
                    Longsword,
                    BattleAxe,
                    Mace,
                    Warhammer,
                    Greatsword,
                    Greataxe,
                    Greathammer
                };
            }
        }
        public static List<WeaponType> DextrousWeaponTypes
        {
            get
            {
                return new List<WeaponType>()
                {
                    Dagger,
                    Rapier,
                    Shortsword
                };
            }
        }
        public static List<WeaponType> PrimitiveWeaponTypes
        {
            get
            {
                return new List<WeaponType>()
                {
                    HandAxe,
                    Cudgel,
                    Spear
                };
            }
        }
        public static List<WeaponType> MartialWeaponTypes
        {
            get
            {
                return new List<WeaponType>()
                {
                    Longsword,
                    BattleAxe,
                    Mace,
                    Warhammer
                };
            }
        }
        public static List<WeaponType> StrengthWeaponTypes
        {
            get
            {
                return new List<WeaponType>()
                {
                    Greatsword,
                    Greataxe,
                    Greathammer
                };
            }
        }
        public string Name { get; }
        public WeaponFamily Family { get; }
        public PhysicalDamageType PrimaryAttack { get; }
        public PhysicalDamageType SecondaryAttack { get; }
        private int minDamage, maxDamage;
        public int MinDamage
        {
            get
            {
                if (IsProficient)
                {
                    if (PlayerEntity.Instance.Class.GetType() == typeof(FighterClass))
                    {
                        return minDamage + Combat.PROFICIENCY_DAMAGE_BONUS_MODERATE;
                    }
                    else
                    {
                        return minDamage + Combat.PROFICIENCY_DAMAGE_BONUS_MINOR;
                    }
                }
                return minDamage;
            }
            private set
            {
                minDamage = value;
            }
        }
        public int MaxDamage
        {
            get
            {
                if (IsProficient)
                {
                    if (PlayerEntity.Instance.Class.GetType() == typeof(FighterClass))
                    {
                        return maxDamage + Combat.PROFICIENCY_DAMAGE_BONUS_MODERATE;
                    }
                    else
                    {
                        return maxDamage + Combat.PROFICIENCY_DAMAGE_BONUS_MINOR;
                    }
                }
                return maxDamage;
            }
            private set
            {
                maxDamage = value;
            }
        }
        public bool IsProficient
        {
            get
            {
                return PlayerEntity.Instance.Class.Proficiencies.WeaponProficiency == Family;
            }
        }

        public WeaponType(string name, WeaponFamily family, PhysicalDamageType primaryAttack, int minDamage, int maxDamage, PhysicalDamageType secondaryAttack = PhysicalDamageType.None)
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
