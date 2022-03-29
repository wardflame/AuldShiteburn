using AuldShiteburn.CombatData.AbilityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.Enemies
{
    [Serializable]
    internal class ShiteHuskEnemyEntity : EnemyEntity
    {
        public ShiteHuskEnemyEntity()
        {
            Name = "Shite Husk";
            Abilities = new List<Ability>
                (
                    
                );
        }
    }
}
