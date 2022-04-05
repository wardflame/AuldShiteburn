using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.PayloadData
{
    [Serializable]
    internal class CombatPayload
    {
        public bool IsAttack { get; set; }
        public bool IsStun { get; set; }
        public bool HasPhysical { get; set; }
        public bool HasProperty { get; set; }
        public PhysicalDamageType PhysicalAttackType { get; set; }
        public PropertyDamageType PropertyAttackType { get; set; }
        public int StunCount { get; set; }
        public int PhysicalDamage { get; set; }
        public int PropertyDamage { get; set; }

        public CombatPayload
            (bool isStun = false, bool hasPhysical = false, bool hasProperty = false,
            PhysicalDamageType physicalAttackType = PhysicalDamageType.None, PropertyDamageType propertyAttackType = PropertyDamageType.None,
            int stunCount = 0, int physicalDamage = 0, int propertyDamage = 0)
        {
            IsStun = isStun;
            HasPhysical = hasPhysical;
            HasProperty = hasProperty;
            PhysicalAttackType = physicalAttackType;
            PropertyAttackType = propertyAttackType;
            StunCount = stunCount;
            PhysicalDamage = physicalDamage;
            PropertyDamage = propertyDamage;
        }
    }
}
