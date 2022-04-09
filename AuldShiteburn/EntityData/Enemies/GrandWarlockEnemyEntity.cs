using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.GrandWarlockAbilities;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.Enemies
{
    [Serializable]
    internal class GrandWarlockEnemyEntity : EnemyEntity
    {
        public GrandWarlockEnemyEntity()
        {
            int health = 110;
            Name = "Earm, Grand Warlock";
            MaxHP = health;
            HP = health;
            PhysicalWeaknesses = new List<PhysicalDamageType>() { PhysicalDamageType.Slash, PhysicalDamageType.Pierce };
            PropertyWeaknesses = new List<PropertyDamageType>() { PropertyDamageType.Fire, PropertyDamageType.Holy };
            StunCap = 2;
            Abilities = new List<Ability>()
            {
                new HardshiteBoltAbility(),
                new HardshiteFleshAbility(),
                new PungentRejuvenationAbility(),
                new ShitepukeAbility()
            };
        }
    }
}
