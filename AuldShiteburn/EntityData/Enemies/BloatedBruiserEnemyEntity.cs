using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.BloatedBruiserAbilities;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.Enemies
{
    [Serializable]
    internal class BloatedBruiserEnemyEntity : EnemyEntity
    {
        public BloatedBruiserEnemyEntity()
        {
            Random rand = new Random();
            int health = rand.Next(19, 24);
            Name = "Bloated Bruiser";
            MaxHP = health;
            HP = health;
            PhysicalWeaknesses = new List<PhysicalDamageType>() { PhysicalDamageType.Pierce };
            PropertyWeaknesses = new List<PropertyDamageType>() { PropertyDamageType.Fire, PropertyDamageType.Holy };
            StunCap = 2;
            Abilities = new List<Ability>()
            {
                new BruiserPunchAbility(),
                new PussBathAbility(),
                new WildSmashAbility()
            };
        }
    }
}
