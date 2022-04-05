using AuldShiteburn.CombatData.PayloadData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.ShiteHuskAbilities
{
    internal class ShiteHuskBiteAbility : Ability
    {
        public override string Name => "Shitehusk Bite";
        public override string Description => "A fast bite from the shite-dripping jaws of a husk.";
        public override int Cooldown => 0;
        public override int ResourceCost => 0;
        public override PhysicalDamageType PhysicalDamageType => PhysicalDamageType.Pierce;
        public override PropertyDamageType PropertyDamageType => PropertyDamageType.Occult;
        public override int PhysicalMinDamage => 1;
        public override int PhysicalMaxDamage => 2;
        public override int PropertyMinDamage => 1;
        public override int PropertyMaxDamage => 2;

        public override CombatPayload UseAbility()
        {
            Utils.SetCursorInteract(Console.CursorTop);
            return new CombatPayload(false);
        }
    }
}
