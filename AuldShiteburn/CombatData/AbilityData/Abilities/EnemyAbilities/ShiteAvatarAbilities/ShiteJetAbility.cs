using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.ShiteAvatarAbilities
{
    [Serializable]
    internal class ShiteJetAbility : Ability
    {
        public override string Name => "Shite Jet";
        public override string Description => $"heaves and wretches and lurches forward, jetting boiling shite!";
        public override int Cooldown => 4;
        public override int ResourceCost => 0;
        public override PropertyDamageType PropertyDamageType => PropertyDamageType.Occult;
        public override int PropertyMinDamage => 11;
        public override int PropertyMaxDamage => 13;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown <= 0)
            {
                Random rand = new Random();
                int propDamage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
                ActiveCooldown = Cooldown;
                return new CombatPayload(
                    isAttack: true,
                    hasProperty: true,
                    propertyAttackType: PropertyDamageType,
                    propertyDamage: propDamage);
            }
            return new CombatPayload(false);
        }
    }
}
