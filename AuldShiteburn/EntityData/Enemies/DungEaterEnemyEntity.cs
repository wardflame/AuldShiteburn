using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.DungEaterAbilities;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.Enemies
{
    [Serializable]
    internal class DungEaterEnemyEntity : EnemyEntity
    {
        public DungEaterEnemyEntity()
        {
            int health = 80;
            Name = "The Dung Eater";
            MaxHP = health;
            HP = health;
            PhysicalWeaknesses = new List<PhysicalDamageType>() { PhysicalDamageType.Slash };
            PropertyWeaknesses = new List<PropertyDamageType>() { PropertyDamageType.Fire };
            StunCap = 1;
            Abilities = new List<Ability>()
            {
                new ClawedSwipeAbility(),
                new DungTossAbility(),
                new FlamingGreatswordSwingAbility()
            };
        }
    }
}
