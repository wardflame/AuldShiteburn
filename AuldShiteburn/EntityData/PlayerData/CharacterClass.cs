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
