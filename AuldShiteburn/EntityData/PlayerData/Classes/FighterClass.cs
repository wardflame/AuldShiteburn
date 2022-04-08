using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.FighterAbilities;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.PlayerData.Classes
{
    [Serializable]
    internal class FighterClass : CharacterClass
    {
        public FighterClass() : base
            ("Fighter", ClassType.Fighter,
            new TitleData(PlayerGenerationData.FighterTitlesMale, PlayerGenerationData.FighterTitlesFemale),
            new ClassStatistics(22, 20, 0, 2),
            new ProficiencyData(ArmourFamily.HeavyArmour, WeaponFamily.MartialArms, PropertyDamageType.None, GeneralMaterials.Steel),
            new List<Ability>()
            {
                new DefensiveStanceAbility()
            }
            )
        { }
    }
}
