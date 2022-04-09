using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.GeneralAbilities;
using AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.RogueAbilities;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.PlayerData.Classes
{
    [Serializable]
    internal class RogueClass : CharacterClass
    {
        public RogueClass() : base
            ("Rogue", ClassType.Rogue,
            new TitleData(PlayerGenerationData.RogueTitles),
            new ClassStatistics(18, 30, 0, 2),
            new ProficiencyData(ArmourFamily.LightArmour, WeaponFamily.DextrousSmallArms, PropertyDamageType.Cold, GeneralMaterials.None),
            new List<Ability>()
            {
                new ParryRiposteAbility(),
                new DirtDishonourAbility(),
                new SmokeBombAbility()
            }
            )
        { }
    }
}
