using System;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.AcolyteAbilities
{
    [Serializable]
    internal class ShiteflameTossAbility : Ability
    {
        public override string Name => "Shiteflame Toss";
        public override string Description => $"Hurls a pungent ball of burning shite at the target for {MinDamage} to {MaxDamage} Occult damage.";
        public override int MinDamage => 4;
        public override int MaxDamage => 8;
        public override int ResourceCost => 6;

        public override void ActivateAbility()
        {
            throw new NotImplementedException();
        }
    }
}
