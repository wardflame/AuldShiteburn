using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.ItemData.WeaponData
{
    [Serializable]
    class WeaponCategory
    {
        #region DextrousWeapons
        public static WeaponCategory Dagger
        {
            get { return new WeaponCategory("Dagger", WeaponFamily.DextrousSmallArms, PhysicalDamageType.Pierce, 3, 5); }
        }
        public static WeaponCategory Rapier
        {
            get { return new WeaponCategory("Rapier", WeaponFamily.DextrousSmallArms, PhysicalDamageType.Pierce, 3, 5); }
        }
        public static WeaponCategory Shortsword
        {
            get { return new WeaponCategory("Shortsword", WeaponFamily.DextrousSmallArms, PhysicalDamageType.Slash, 3, 5); }
        }
        #endregion DextrousWeapons
        #region PrimitiveWeapons
        public static WeaponCategory HandAxe
        {
            get { return new WeaponCategory("Greatsword", WeaponFamily.PrimitiveArms, PhysicalDamageType.Slash, 4, 6); }
        }
        public static WeaponCategory Cudgel
        {
            get { return new WeaponCategory("Greatsword", WeaponFamily.PrimitiveArms, PhysicalDamageType.Strike, 4, 6); }
        }
        public static WeaponCategory Spear
        {
            get { return new WeaponCategory("Spear", WeaponFamily.PrimitiveArms, PhysicalDamageType.Pierce, 4, 6); }
        }
        #endregion PrimitiveWeapons
        #region MartialWeapons
        public static WeaponCategory Longsword
        {
            get { return new WeaponCategory("Longsword", WeaponFamily.MartialArms, PhysicalDamageType.Slash, 5, 7); }
        }
        public static WeaponCategory BattleAxe
        {
            get { return new WeaponCategory("Battle Axe", WeaponFamily.MartialArms, PhysicalDamageType.Slash, 5, 7); }
        }
        public static WeaponCategory Mace
        {
            get { return new WeaponCategory("Mace", WeaponFamily.MartialArms, PhysicalDamageType.Strike, 5, 7); }
        }
        public static WeaponCategory Warhammer
        {
            get { return new WeaponCategory("Warhammer", WeaponFamily.MartialArms, PhysicalDamageType.Strike, 5, 7); }
        }
        #endregion MartialWeapons
        #region StrengthWeapons
        public static WeaponCategory Greatsword
        {
            get { return new WeaponCategory("Greatsword", WeaponFamily.StrengthLargeArms, PhysicalDamageType.Slash, 7, 9); }
        }
        public static WeaponCategory Greataxe
        {
            get { return new WeaponCategory("Greataxe", WeaponFamily.StrengthLargeArms, PhysicalDamageType.Slash, 7, 9); }
        }
        public static WeaponCategory Greathammer
        {
            get { return new WeaponCategory("Greathammer", WeaponFamily.StrengthLargeArms, PhysicalDamageType.Strike, 7, 9); }
        }
        #endregion StrengthWeapons

        public static List<WeaponCategory> AllWeaponCategories = new List<WeaponCategory>()
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
        public static List<WeaponCategory> DextrousWeapons = new List<WeaponCategory>()
        {
            Dagger,
            Rapier,
            Shortsword
        };
        public static List<WeaponCategory> PrimitiveWeapons = new List<WeaponCategory>()
        {
            HandAxe,
            Cudgel,
            Spear
        };
        public static List<WeaponCategory> MartialWeapons = new List<WeaponCategory>()
        {
            Longsword,
            BattleAxe,
            Mace,
            Warhammer
        };
        public static List<WeaponCategory> StrengthWeapons = new List<WeaponCategory>()
        {
            Greatsword,
            Greataxe,
            Greathammer
        };
        public string Name { get; }
        public WeaponFamily Family { get; }
        public PhysicalDamageType PrimaryAttack { get; }
        public PhysicalDamageType SecondaryAttack { get; }
        private int minDamage, maxDamage;
        public int MinDamage
        {
            get
            {
                if (Proficient)
                {
                    return minDamage += Combat.PROFICIENCY_DAMAGE_MODIFIER;
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
                if (Proficient)
                {
                    return maxDamage += Combat.PROFICIENCY_DAMAGE_MODIFIER;
                }
                return maxDamage;
            }
            private set
            {
                maxDamage = value;
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

        public WeaponCategory(string name, WeaponFamily family, PhysicalDamageType primaryAttack, int minDamage, int maxDamage, PhysicalDamageType secondaryAttack = PhysicalDamageType.None)
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
