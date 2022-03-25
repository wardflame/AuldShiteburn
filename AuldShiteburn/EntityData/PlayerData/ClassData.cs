using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.EntityData.PlayerData
{
    internal class ClassData
    {
        public static ClassData Fighter
        {
            get { return new ClassData(PlayerGenerationData.fighterTitleMale, PlayerGenerationData.fighterTitleFemale, 16, WeaponFamily.MartialArms); }
        }

        public List<string> TitlesMale { get; set; }
        public List<string> TitlesFemale { get; set; }
        public float HP { get; }
        public WeaponFamily WeaponProficiency { get; }

        public ClassData(List<string> titlesMale, List<string> titlesFemale, float hp, WeaponFamily weaponProficiency)
        {
            TitlesMale = titlesMale;
            TitlesFemale = titlesFemale;
            HP = hp;
            WeaponProficiency = weaponProficiency;
        }
    }
}
