using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.ShiteHuskAbilities;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.Enemies
{
    [Serializable]
    internal class ShiteHuskEnemyEntity : EnemyEntity
    {
        public ShiteHuskEnemyEntity()
        {
            Random rand = new Random();
            int health = rand.Next(11, 16);
            Name = "Shite Husk";
            MaxHP = health;
            HP = health;
            PhysicalWeaknesses = new List<PhysicalDamageType>() { PhysicalDamageType.Slash, PhysicalDamageType.Pierce };
            PropertyWeaknesses = new List<PropertyDamageType>() { PropertyDamageType.Fire, PropertyDamageType.Holy };
            StunCap = 3;
            Abilities = new List<Ability>()
            {
                new ShiteHuskBiteAbility()
            };
        }
    }
}
