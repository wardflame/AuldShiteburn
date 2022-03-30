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
    internal class RogueClass : CharacterClass
    {
        public RogueClass() : base
            ("Rogue", ClassType.Rogue,
            new TitleData(PlayerGenerationData.RogueTitles),
            new ClassStatistics(16, 30, 0),
            new ProficiencyData(ArmourFamily.LightArmour, WeaponFamily.DextrousSmallArms, PropertyDamageType.Cold, GeneralMaterials.None),
            new List<Ability>()
            {
            }
            )
        { }
    }
}
