using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData
{
    [Serializable]
    internal abstract class Ability
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract int MinDamage { get; }
        public abstract int MaxDamage { get; }
        public abstract int ResourceCost { get; }
        public abstract void ActivateAbility();
    }
}
