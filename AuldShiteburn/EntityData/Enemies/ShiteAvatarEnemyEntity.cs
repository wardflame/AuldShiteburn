using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.GrandWarlockAbilities;
using AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.ShiteAvatarAbilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.EntityData.Enemies
{
    [Serializable]
    internal class ShiteAvatarEnemyEntity : EnemyEntity
    {
        public ShiteAvatarEnemyEntity()
        {
            int health = 220;
            Name = "Avatar of Shite";
            MaxHP = health;
            HP = health;
            PhysicalWeaknesses = new List<PhysicalDamageType>() { PhysicalDamageType.None };
            PropertyWeaknesses = new List<PropertyDamageType>() { PropertyDamageType.Holy };
            StunCap = 1;
            Abilities = new List<Ability>()
            {
                new EldritchEruptionAbility(),
                new SavageBiteAbility(),
                new ShiteJetAbility(),
                new TailWhipAbility(),
                new HardshiteFleshAbility(),
            };
        }
    }
}
