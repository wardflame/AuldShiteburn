using AuldShiteburn.CombatData.PayloadData;
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
        public virtual int ResourceCost { get; }
        public virtual PhysicalDamageType PhysicalDamageType { get; }
        public virtual PropertyDamageType PropertyDamageType { get; }
        public virtual int PhysicalMinDamage { get; }
        public virtual int PhysicalMaxDamage { get; }
        public virtual int PropertyMinDamage { get; }
        public virtual int PropertyMaxDamage { get; }

        public abstract CombatPayload UseAbility();
    }
}
