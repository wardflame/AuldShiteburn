using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.HeathenAbilities;
using AuldShiteburn.EntityData.PlayerData.Classes;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.PlayerData
{
    [Serializable]
    internal abstract class CharacterClass
    {
        public static List<CharacterClass> Classes
        {
            get
            {
                return new List<CharacterClass>()
                {
                    new FighterClass(),
                    new HeathenClass(),
                    new MarauderClass(),
                    new MonkClass(),
                    new RogueClass(),
                };
            }
        }
        public string Name { get; protected set; }
        public ClassType ClassType { get; protected set; }
        public TitleData Titles { get; protected set; }
        public ClassStatistics Statistics { get; protected set; }
        public ProficiencyData Proficiencies { get; protected set; }
        public List<Ability> Abilities { get; protected set; }

        public CharacterClass(string name, ClassType classType, TitleData titleData, ClassStatistics classStatistics, ProficiencyData proficiencies, List<Ability> abilities)
        {
            Name = name;
            ClassType = classType;
            Titles = titleData;
            Statistics = classStatistics;
            Proficiencies = proficiencies;
            Abilities = abilities;
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
        public List<string> TitleMale { get; }
        public List<string> TitleFemale { get; }

        public TitleData(List<string> titleMale, List<string> titleFemale)
        {
            TitleMale = titleMale;
            TitleFemale = titleFemale;
        }

        public TitleData(List<string> unisexTitles) : this(unisexTitles, unisexTitles)
        {
        }
    }

    struct ClassStatistics
    {
        public float HP { get; }
        public bool UsesMana { get; }
        public bool UsesStamina { get; }
        public float Stamina { get; }
        public float Mana { get; }
        public int StunCap { get; }

        public ClassStatistics(float hp, float stamina, float mana, int stunCap)
        {
            UsesMana = mana > 0;
            UsesStamina = stamina > 0;
            HP = hp;
            Stamina = stamina;
            Mana = mana;
            StunCap = stunCap;
        }
    }

    struct ProficiencyData
    {
        public ArmourFamily ArmourProficiency { get; }
        public WeaponFamily WeaponProficiency { get; }
        public PropertyDamageType PropertyAffinity { get; }
        public GeneralMaterials MaterialAffinity { get; }

        public ProficiencyData(ArmourFamily armourProficiency, WeaponFamily weaponProficiency, PropertyDamageType propertyAffinity, GeneralMaterials materialAffinity)
        {
            ArmourProficiency = armourProficiency;
            WeaponProficiency = weaponProficiency;
            PropertyAffinity = propertyAffinity;
            MaterialAffinity = materialAffinity;
        }
    }
}
