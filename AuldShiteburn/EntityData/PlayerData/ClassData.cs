using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.PlayerData
{
    [Serializable]
    internal class ClassData
    {
        #region Classes
        public static ClassData Heathen
        {
            get { return new ClassData("Heathen", ClassType.Heathen, PlayerGenerationData.HeathenTitles, null, false, true, 12, 0, 30, WeaponFamily.PrimitiveArms); }
        }
        public static ClassData Fighter
        {
            get { return new ClassData("Fighter", ClassType.Fighter, PlayerGenerationData.FighterTitlesMale, PlayerGenerationData.FighterTitlesFemale, true, false, 28, 20, 0, WeaponFamily.MartialArms); }
        }
        public static ClassData Marauder
        {
            get { return new ClassData("Marauder", ClassType.Marauder, PlayerGenerationData.MarauderTitles, null, true, false, 34, 20, 0, WeaponFamily.StrengthLargeArms); }
        }
        public static ClassData Monk
        {
            get { return new ClassData("Monk", ClassType.Monk, PlayerGenerationData.MonkTitlesMale, PlayerGenerationData.MonkTitlesFemale, false, true, 22, 0, 20, WeaponFamily.PrimitiveArms); }
        }
        public static ClassData Rogue
        {
            get { return new ClassData("Rogue", ClassType.Rogue, PlayerGenerationData.RogueTitles, null, true, false, 16, 30, 0, WeaponFamily.DextrousSmallArms); }
        }
        #endregion Classes

        public static List<ClassData> Classes { get; } = new List<ClassData>()
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
        public bool UsesStamina { get; }
        public bool UsesMana { get; }
        public float HP { get; }
        public float Stamina { get; }
        public float Mana { get; }
        public WeaponFamily WeaponProficiency { get; }

        public ClassData(string name, ClassType classType, List<string> titlesMale, List<string> titlesFemale, bool usesStamina, bool usesMana, float hp, float stamina, float mana, WeaponFamily weaponProficiency)
        {
            Name = name;
            ClassType = classType;
            TitlesMale = titlesMale;
            TitlesFemale = titlesFemale;
            UsesStamina = usesStamina;
            UsesMana = usesMana;
            HP = hp;
            Stamina = stamina;
            Mana = mana;
            WeaponProficiency = weaponProficiency;
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
}
