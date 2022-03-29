using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.PlayerData
{
    [Serializable]
    internal class CharacterClass
    {
        #region Classes
        public static CharacterClass Heathen
        {
            get
            { 
                return new CharacterClass(
                "Heathen",
                ClassType.Heathen,
                new TitleData(PlayerGenerationData.HeathenTitles),
                new ClassStatistics(12, 0, 30),
                new ProficiencyData(ArmourFamily.LightArmour, WeaponFamily.PrimitiveArms, PropertyDamageType.Occult, GeneralMaterials.Hardshite));
            }
        }
        public static CharacterClass Fighter
        {
            get
            {
                return new CharacterClass(
                "Fighter",
                ClassType.Fighter,
                new TitleData(PlayerGenerationData.FighterTitlesMale, PlayerGenerationData.FighterTitlesFemale),
                new ClassStatistics(28, 20, 0),
                new ProficiencyData(ArmourFamily.HeavyArmour, WeaponFamily.MartialArms, PropertyDamageType.None, GeneralMaterials.None));
            }
        }
        public static CharacterClass Marauder
        {
            get
            {
                return new CharacterClass(
                "Marauder",
                ClassType.Marauder,
                new TitleData(PlayerGenerationData.MarauderTitles),
                new ClassStatistics(34, 20, 0),
                new ProficiencyData(ArmourFamily.MediumArmour, WeaponFamily.StrengthLargeArms, PropertyDamageType.Fire, GeneralMaterials.None));
            }
        }
        public static CharacterClass Monk
        {
            get
            {
                return new CharacterClass(
                "Monk",
                ClassType.Monk,
                new TitleData(PlayerGenerationData.MonkTitlesMale, PlayerGenerationData.MonkTitlesFemale),
                new ClassStatistics(22, 0, 20),
                new ProficiencyData(ArmourFamily.LightArmour, WeaponFamily.PrimitiveArms, PropertyDamageType.Holy, GeneralMaterials.Moonstone));
            }
        }
        public static CharacterClass Rogue
        {
            get
            {
                return new CharacterClass(
                "Rogue",
                ClassType.Rogue,
                new TitleData(PlayerGenerationData.RogueTitles),
                new ClassStatistics(16, 30, 0),
                new ProficiencyData(ArmourFamily.LightArmour, WeaponFamily.DextrousSmallArms, PropertyDamageType.Cold, GeneralMaterials.None));
            }
        }
        #endregion Classes

        public static List<CharacterClass> Classes { get; } = new List<CharacterClass>()
        {
            Heathen,
            Fighter,
            Marauder,
            Monk,
            Rogue
        };
        public string Name { get; }
        public ClassType ClassType { get; }
        public List<string> TitlesMale { get; set; }
        public List<string> TitlesFemale { get; set; }
        public ClassStatistics Statistics { get; }
        public ProficiencyData Proficiencies { get; set; }
        public List<Ability> Abilities { get; }

        public CharacterClass(string name, ClassType classType, TitleData titleData, ClassStatistics classStatistics, ProficiencyData proficiencies)
        {
            Name = name;
            ClassType = classType;
            TitlesMale = titleData.titleMale;
            TitlesFemale = titleData.titleFemale;
            Statistics = classStatistics;
            Proficiencies = proficiencies;
        }
    }

    enum ClassType
    {
        None,
        Heathen,
        Fighter,
        Marauder,
        Monk,
        Rogue
    }

    struct TitleData
    {
        public List<string> titleMale;
        public List<string> titleFemale;

        public TitleData(List<string> titleMale, List<string> titleFemale)
        {
            this.titleMale = titleMale;
            this.titleFemale = titleFemale;
        }

        public TitleData(List<string> unisexTitles) : this(unisexTitles, unisexTitles)
        {
        }
    }

    struct ClassStatistics
    {
        public float hp;
        public bool usesMana;
        public bool usesStamina;
        public float stamina;
        public float mana;

        public ClassStatistics(float hp, float stamina, float mana)
        {
            usesMana = mana > 0;
            usesStamina = stamina > 0;
            this.hp = hp;
            this.stamina = stamina;
            this.mana = mana;
        }
    }

    struct ProficiencyData
    {
        public ArmourFamily armourProficiency;
        public WeaponFamily weaponProficiency;
        public PropertyDamageType propertyAffinity;
        public GeneralMaterials materialAffinity;

        public ProficiencyData(ArmourFamily armourProficiency, WeaponFamily weaponProficiency, PropertyDamageType propertyAffinity, GeneralMaterials materialAffinity)
        {
            this.armourProficiency = armourProficiency;
            this.weaponProficiency = weaponProficiency;
            this.propertyAffinity = propertyAffinity;
            this.materialAffinity = materialAffinity;
        }
    }
}
