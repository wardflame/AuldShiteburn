using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.PayloadData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.StatusEffectData
{
    internal abstract class StatusEffect
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public ConsoleColor DisplayColor { get; set; }
        public virtual void EffectActive(AttackPayload attackPayload)
        {
        }
    }
}
