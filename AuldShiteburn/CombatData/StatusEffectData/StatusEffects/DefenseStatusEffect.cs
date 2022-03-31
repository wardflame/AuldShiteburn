using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.PayloadData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.StatusEffectData.StatusEffects
{
    /// <summary>
    /// Defensive status which either nullifies or mitigates incoming damage.
    /// </summary>
    internal class DefenseStatusEffect : StatusEffect
    {
        public EffectLevel PhysicalEffectLevel { get; }
        public EffectLevel PropertyEffectLevel { get; }
        public PhysicalDamageType PhysicalTypeDefense { get; }
        public PropertyDamageType PropertyTypeDefense { get; }

        /// <summary>
        /// Will this nullification/mitigation apply to all physical types?
        /// </summary>
        public bool AllPhysicalDefense { get; }

        /// <summary>
        /// Will this nullification/mitigation apply to all property types?
        /// </summary>
        public bool AllPropertyDefense { get; }

        /// <summary>
        /// If true, nullify physical damage. If false, mitigate it.
        /// </summary>
        public bool PhysicalNulOrMit { get; }

        /// <summary>
        /// If true, nullify property damage. If false, mitigate it.
        /// </summary>
        public bool PropertyNulOrMit { get; }

        public DefenseStatusEffect
            (EffectLevel physicalEffectLevel = EffectLevel.None, EffectLevel propertyEffectLevel = EffectLevel.None,
            bool allPhysicalDefense = false, bool allPropertyDefense = false,
            PhysicalDamageType physicalDamageType = PhysicalDamageType.None, PropertyDamageType propertyDamageType = PropertyDamageType.None,
            bool physicalNulOrMit = false, bool propertyNulOrMit = false)
        {
            PhysicalEffectLevel = physicalEffectLevel;
            PropertyEffectLevel = propertyEffectLevel;
            AllPhysicalDefense = allPhysicalDefense;
            AllPropertyDefense = allPropertyDefense;
            PhysicalTypeDefense = physicalDamageType;
            PropertyTypeDefense = propertyDamageType;
            PhysicalNulOrMit = physicalNulOrMit;
            PropertyNulOrMit = propertyNulOrMit;
        }

        public override void EffectActive(CombatPayload attackPayload)
        {
            if (attackPayload.HasPhysical)
            {
                if (attackPayload.PhysicalAttackType == PhysicalTypeDefense || AllPhysicalDefense)
                {
                    if (PhysicalNulOrMit)
                    {
                        attackPayload.PhysicalDamage = 0;
                    }
                    else
                    {
                        switch (PhysicalEffectLevel)
                        {
                            case EffectLevel.Minor:
                                {
                                    attackPayload.PhysicalDamage -= Combat.STATUS_MITIGATION_MINOR;
                                }
                                break;
                            case EffectLevel.Moderate:
                                {
                                    attackPayload.PhysicalDamage -= Combat.STATUS_MITIGATION_MODERATE;
                                }
                                break;
                            case EffectLevel.Major:
                                {
                                    attackPayload.PhysicalDamage -= Combat.STATUS_MITIGATION_MAJOR;
                                }
                                break;
                        }
                    }
                }
            }
            if (attackPayload.HasProperty)
            {
                if (attackPayload.PropertyAttackType == PropertyTypeDefense || AllPropertyDefense)
                {
                    if (PropertyNulOrMit)
                    {
                        attackPayload.PropertyDamage = 0;
                    }
                    else
                    {
                        switch (PropertyEffectLevel)
                        {
                            case EffectLevel.Minor:
                                {
                                    attackPayload.PhysicalDamage -= Combat.STATUS_MITIGATION_MINOR;
                                }
                                break;
                            case EffectLevel.Moderate:
                                {
                                    attackPayload.PhysicalDamage -= Combat.STATUS_MITIGATION_MODERATE;
                                }
                                break;
                            case EffectLevel.Major:
                                {
                                    attackPayload.PhysicalDamage -= Combat.STATUS_MITIGATION_MAJOR;
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
