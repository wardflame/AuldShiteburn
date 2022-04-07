using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.MonkAbilities;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.PlayerData.Classes
{
    [Serializable]
    internal class MonkClass : CharacterClass
    {
        public MonkClass() : base
            ("Monk", ClassType.Monk,
            new TitleData(PlayerGenerationData.MonkTitlesMale, PlayerGenerationData.MonkTitlesFemale),
            new ClassStatistics(22, 0, 20, 3),
            new ProficiencyData(ArmourFamily.LightArmour, WeaponFamily.PrimitiveArms, PropertyDamageType.Holy, GeneralMaterials.Moonstone),
            new List<Ability>()
            {
                new MoonlightBurstAbility()
            }
            )
        { }
    }
}
