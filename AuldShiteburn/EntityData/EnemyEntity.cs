using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData
{
    [Serializable]
    internal class EnemyEntity : LivingEntity
    {
        public override string EntityChar => "";
        public List<Ability> Abilities { get; protected set; }
        public List<PhysicalDamageType> PhysicalWeaknesses { get; } = new List<PhysicalDamageType>();
        public List<PropertyDamageType> PropertyWeaknesses { get; } = new List<PropertyDamageType>();

        public override void Move()
        {
        }
    }
}
