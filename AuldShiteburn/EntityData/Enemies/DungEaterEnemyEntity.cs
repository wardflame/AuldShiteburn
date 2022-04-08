﻿using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.DungEaterAbilities;
using AuldShiteburn.ItemData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.EntityData.Enemies
{
    [Serializable]
    internal class DungEaterEnemyEntity : EnemyEntity
    {
        public DungEaterEnemyEntity()
        {
            int health = 60;
            Name = "The Dung Eater";
            MaxHP = health;
            HP = health;
            PhysicalWeakness = PhysicalDamageType.Pierce;
            MaterialWeakness = GeneralMaterials.Moonstone;
            PropertyWeakness = PropertyDamageType.Fire;
            StunCap = 2;
            Abilities = new List<Ability>()
            {
                new ClawedSwipeAbility(),
                new DungTossAbility(),
                new ShitepukeAbility()
            };
        }
    }
}
