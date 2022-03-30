using AuldShiteburn.CombatData.AbilityData;
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
        public PhysicalDamageType PhysicalDamageDefense { get; }
        public PropertyDamageType PropertyDamageDefense { get; }
        /// <summary>
        /// If true, Nullify, else Mitigate damage.
        /// </summary>
        public bool PhysicalNulOrMit { get; }
        public bool PropertyNulOrMit { get; }

        public DefenseStatusEffect(PhysicalDamageType physicalDamageType, PropertyDamageType propertyDamageType, bool physicalNulOrMit, bool propertyNulOrMit)
        {
            PhysicalDamageDefense = physicalDamageType;
            PropertyDamageDefense = propertyDamageType;
            PhysicalNulOrMit = physicalNulOrMit;
            PropertyNulOrMit = propertyNulOrMit;
        }

        public override AbilityPayload EffectActive(AbilityPayload abilityPayload = new AbilityPayload())
        {
            bool payloadFired = abilityPayload.Fired;
            DamagePayload newPayload = abilityPayload.DamagePayload;
            if (newPayload.IsDamageAttack)
            {
                if (newPayload.physicalDamageType == PhysicalDamageDefense)
                {
                    if (PhysicalNulOrMit)
                    {
                        newPayload.physicalDamage = 0;
                    }
                    else
                    {
                        newPayload.physicalDamage -= Combat.STATUS_MITIGATION_MODIFIER;
                    }
                }
                if (newPayload.propertyDamageType == PropertyDamageDefense)
                {
                    if (PropertyNulOrMit)
                    {
                        newPayload.physicalDamage = 0;
                    }
                    else
                    {
                        newPayload.physicalDamage -= Combat.STATUS_MITIGATION_MODIFIER;
                    }
                }
            }
            return abilityPayload;
        }
    }
}
