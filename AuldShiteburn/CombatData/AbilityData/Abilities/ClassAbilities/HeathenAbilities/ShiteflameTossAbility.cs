using AuldShiteburn.EntityData;
using System;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.HeathenAbilities
{
    [Serializable]
    internal class ShiteflameTossAbility : Ability
    {
        public override string Name => "Shiteflame Toss";
        public override string Description => $"Hurls a pungent ball of burning shite at the target for {MinDamage} to {MaxDamage} Occult damage.";
        public override int Cooldown => 4;
        public override int ResourceCost => 6;
        public override int MinDamage => 4;
        public override int MaxDamage => 8;

        public override AbilityPayload UseAbility()
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown > 0)
            {
                ActiveCooldown--;
                Utils.WriteColour($"{Name} is on cooldown {ActiveCooldown}/{Cooldown}.", ConsoleColor.DarkRed);
                return new AbilityPayload(new DamagePayload(), false);
            }
            else if (ActiveCooldown <= 0)
            {
                Random rand = new Random();
                int damage = rand.Next(MinDamage, MaxDamage + 1);
                ActiveCooldown = Cooldown;
                return new AbilityPayload(new DamagePayload(0, damage, PhysicalDamageType.None, PropertyDamageType.Occult), true);
            }
            return new AbilityPayload(new DamagePayload(), false);
        }
    }
}
