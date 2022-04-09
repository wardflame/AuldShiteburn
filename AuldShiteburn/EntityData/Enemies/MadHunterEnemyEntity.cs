using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.MadHunterAbilities;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.Enemies
{
    [Serializable]
    internal class MadHunterEnemyEntity : EnemyEntity
    {
        public MadHunterEnemyEntity()
        {
            Random rand = new Random();
            int health = rand.Next(17, 22);
            Name = "Mad Hunter";
            MaxHP = health;
            HP = health;
            PhysicalWeaknesses = new List<PhysicalDamageType>() { PhysicalDamageType.Pierce };
            PropertyWeaknesses = new List<PropertyDamageType>() { PropertyDamageType.Occult, PropertyDamageType.Cold };
            StunCap = 3;
            Abilities = new List<Ability>()
            {
                new MadSlashAbility()
            };
        }
    }
}
