using AuldShiteburn.CombatData.StatusEffectData;
using System;

namespace AuldShiteburn.CombatData.PayloadData
{
    [Serializable]
    internal class CombatPayload
    {
        public bool IsAttack { get; set; }
        public bool IsUtility { get; set; }
        public bool IsStun { get; set; }
        public bool HasStatus { get; set; }
        public bool HasPhysical { get; set; }
        public bool HasProperty { get; set; }
        public StatusEffect StatusEffect { get; set; }
        public PhysicalDamageType PhysicalAttackType { get; set; }
        public PropertyDamageType PropertyAttackType { get; set; }
        public int StunCount { get; set; }
        public int PhysicalDamage { get; set; }
        public int PropertyDamage { get; set; }

        public CombatPayload
            (bool isAttack, bool isUtility = false, bool isStun = false, bool hasStatus = false, bool hasPhysical = false, bool hasProperty = false,
            StatusEffect statusEffect = null, PhysicalDamageType physicalAttackType = PhysicalDamageType.None, PropertyDamageType propertyAttackType = PropertyDamageType.None,
            int stunCount = 0, int physicalDamage = 0, int propertyDamage = 0)
        {
            IsAttack = isAttack;
            IsUtility = isUtility;
            IsStun = isStun;
            HasStatus = hasStatus;
            HasPhysical = hasPhysical;
            HasProperty = hasProperty;
            StatusEffect = statusEffect;
            PhysicalAttackType = physicalAttackType;
            PropertyAttackType = propertyAttackType;
            StunCount = stunCount;
            PhysicalDamage = physicalDamage;
            PropertyDamage = propertyDamage;
        }
    }
}
