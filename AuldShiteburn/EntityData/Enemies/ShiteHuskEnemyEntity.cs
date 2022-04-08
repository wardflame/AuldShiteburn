using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.ShiteHuskAbilities;
using AuldShiteburn.ItemData;
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
            int health = rand.Next(10, 17);
            Name = "Shite Husk";
            MaxHP = health;
            HP = health;
            PhysicalWeakness = PhysicalDamageType.Slash;
            MaterialWeakness = GeneralMaterials.Moonstone;
            PropertyWeakness = PropertyDamageType.Fire;
            StunCap = 6;
            Abilities = new List<Ability>()
            {
                new ShiteHuskBiteAbility()
            };
        }
    }
}
