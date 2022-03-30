using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.HeathenAbilities;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.EntityData.PlayerData.Classes
{
    internal class HeathenClass : CharacterClass
    {
        public HeathenClass() : base
            ("Heathen", ClassType.Heathen,
            new TitleData(PlayerGenerationData.HeathenTitles),
            new ClassStatistics(12, 0, 30),
            new ProficiencyData(ArmourFamily.LightArmour, WeaponFamily.PrimitiveArms, PropertyDamageType.Occult, GeneralMaterials.Hardshite),
            new List<Ability>()
            {
                new ShiteflameTossAbility(),
                new ShiteWardAbility()
            }
            )
        { }
    }
}
