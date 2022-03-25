using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.EntityData.PlayerData
{
    [Serializable]
    internal class ClassData
    {
        #region Classes
        public static ClassData Acolyte
        {
            get { return new ClassData(PlayerGenerationData.AcolyteTitles, null, false, true, 12, 0, 30, WeaponFamily.PrimitiveArms) ; }
        }
        public static ClassData Fighter
        {
            get { return new ClassData(PlayerGenerationData.FighterTitlesMale, PlayerGenerationData.FighterTitlesFemale, true, false, 28, 20, 0, WeaponFamily.MartialArms); }
        }
        public static ClassData Marauder
        {
            get { return new ClassData(PlayerGenerationData.MarauderTitles, null, true, false, 34, 20, 0, WeaponFamily.StrengthLargeArms); }
        }
        public static ClassData Monk
        {
            get { return new ClassData(PlayerGenerationData.MonkTitlesMale, PlayerGenerationData.MonkTitlesFemale, false, true, 22, 10, 20, WeaponFamily.PrimitiveArms); }
        }
        public static ClassData Rogue
        {
            get { return new ClassData(PlayerGenerationData.RogueTitles, null, true, false, 16, 30, 0, WeaponFamily.DextrousSmallArms); }
        }
        #endregion Classes

        public static List<ClassData> Classes { get; } = new List<ClassData>()
        {
            Acolyte,
            Fighter,
            Marauder,
            Monk,
            Rogue
        };
        public List<string> TitlesMale { get; set; }
        public List<string> TitlesFemale { get; set; }
        public bool UsesStamina { get; }
        public bool UsesMana { get; }
        public float HP { get; }
        public float Stamina { get; }
        public float Mana { get; }
        public WeaponFamily WeaponProficiency { get; }

        public ClassData(List<string> titlesMale, List<string> titlesFemale, bool usesStamina, bool usesMana, float hp, float stamina, float mana, WeaponFamily weaponProficiency)
        {
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
}
