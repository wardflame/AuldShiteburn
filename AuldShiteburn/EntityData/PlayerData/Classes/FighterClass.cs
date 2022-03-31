﻿using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.EntityData.PlayerData.Classes
{
    internal class FighterClass : CharacterClass
    {
        public FighterClass() : base
            ("Fighter", ClassType.Fighter,
            new TitleData(PlayerGenerationData.FighterTitlesMale, PlayerGenerationData.FighterTitlesFemale),
            new ClassStatistics(28, 20, 0, 2),
            new ProficiencyData(ArmourFamily.HeavyArmour, WeaponFamily.MartialArms, PropertyDamageType.Standard, GeneralMaterials.None),
            new List<Ability>()
            {
            }
            )
        { }
    }
}
