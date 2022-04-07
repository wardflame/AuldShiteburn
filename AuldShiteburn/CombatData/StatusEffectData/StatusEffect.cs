using AuldShiteburn.CombatData.PayloadData;
using System;

namespace AuldShiteburn.CombatData.StatusEffectData
{
    [Serializable]
    internal abstract class StatusEffect
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public EffectType Type { get; set; }
        public ConsoleColor DisplayColor { get; set; }
        public virtual CombatPayload EffectActive(CombatPayload combatPayload)
        {
            return combatPayload;
        }
    }

    [Serializable]
    public enum EffectType
    {
        None,
        Buff,
        Debuff,
        Replenishment
    }
}
