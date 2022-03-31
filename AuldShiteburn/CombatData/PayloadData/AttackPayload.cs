using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.PayloadData
{
    [Serializable]
    internal class AttackPayload
    {
        public bool IsStun { get; set; }
        public bool HasPhysical { get; set; }
        public bool HasProperty { get; set; }
        public PhysicalDamageType PhysicalAttackType { get; set; }
        public PropertyDamageType PropertyAttackType { get; set; }
        public int PhysicalDamage { get; set; }
        public int PropertyDamage { get; set; }

        public AttackPayload(bool isStun = false, bool hasPhysical = false, bool hasProperty = false, PhysicalDamageType physicalAttackType = PhysicalDamageType.None, PropertyDamageType propertyAttackType = PropertyDamageType.None, int physicalDamage = 0, int propertyDamage = 0)
        {
            IsStun = isStun;
            HasPhysical = hasPhysical;
            HasProperty = hasProperty;
            PhysicalAttackType = physicalAttackType;
            PropertyAttackType = propertyAttackType;
            PhysicalDamage = physicalDamage;
            PropertyDamage = propertyDamage;
        }
    }
}
