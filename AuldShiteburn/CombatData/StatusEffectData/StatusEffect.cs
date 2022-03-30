using AuldShiteburn.CombatData.AbilityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.StatusEffectData
{
    internal abstract class StatusEffect
    {
        public virtual AbilityPayload EffectActive(AbilityPayload abilityPayload = new AbilityPayload())
        {
            return abilityPayload;
        }
    }
}
