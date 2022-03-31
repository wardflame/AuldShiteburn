using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.EntityData.PlayerData.Classes
{
    internal class MarauderClass : CharacterClass
    {
        public MarauderClass() : base
            ("Marauder", ClassType.Marauder,
            new TitleData(PlayerGenerationData.MarauderTitles),
            new ClassStatistics(34, 30, 0, 1),
            new ProficiencyData(ArmourFamily.MediumArmour, WeaponFamily.StrengthLargeArms, PropertyDamageType.Fire, GeneralMaterials.None),
            new List<Ability>()
            {
            }
            )
        { }
    }
}
