using AuldShiteburn.CombatData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData
{
    [Serializable]
    internal class EnemyEntity : LivingEntity
    {
        public override string EntityChar => "";
        public List<PhysicalDamageType> PhysicalWeaknesses { get; } = new List<PhysicalDamageType>();
        public List<PropertyDamageType> PropertyWeaknesses { get; } = new List<PropertyDamageType>();

        public override void Move()
        {
        }
    }
}
