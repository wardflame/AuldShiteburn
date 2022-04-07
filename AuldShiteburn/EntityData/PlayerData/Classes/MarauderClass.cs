using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.MarauderAbilities;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.PlayerData.Classes
{
    [Serializable]
    internal class MarauderClass : CharacterClass
    {
        public MarauderClass() : base
            ("Marauder", ClassType.Marauder,
            new TitleData(PlayerGenerationData.MarauderTitles),
            new ClassStatistics(34, 30, 0, 1),
            new ProficiencyData(ArmourFamily.MediumArmour, WeaponFamily.StrengthLargeArms, PropertyDamageType.Fire, GeneralMaterials.None),
            new List<Ability>()
            {
                new EnragedCleaveAbility()
            }
            )
        { }
    }
}
