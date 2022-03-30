using AuldShiteburn.EntityData;
using System;

namespace AuldShiteburn.CombatData.AbilityData
{
    [Serializable]
    internal abstract class Ability
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract int Cooldown { get; }
        private int activeCooldown;
        public int ActiveCooldown
        { 
            get
            {
                return activeCooldown;
            }
            set
            {
                activeCooldown = value;
                if (activeCooldown < 0)
                {
                    activeCooldown = 0;
                }
                if (activeCooldown > Cooldown)
                {
                    activeCooldown = Cooldown;
                }
            }
        }
        public abstract int ResourceCost { get; }
        public abstract int MinDamage { get; }
        public abstract int MaxDamage { get; }

        public abstract AbilityPayload UseAbility();
    }

    public struct AbilityPayload
    {
        public DamagePayload DamagePayload { get; }
        public bool Fired { get; }

        public AbilityPayload(DamagePayload damagePayload, bool fired)
        {
            DamagePayload = damagePayload;
            Fired = fired;
        }
    }
}
