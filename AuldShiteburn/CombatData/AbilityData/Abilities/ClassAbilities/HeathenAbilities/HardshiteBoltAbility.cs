using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.HeathenAbilities
{
    [Serializable]
    internal class HardshiteBoltAbility : Ability
    {
        public override string Name => "Hardshite Bolt";
        public override string Description => $"Shoots a bolt of hardshite at the target for {PropertyMinDamage} - {PropertyMaxDamage} Occult damage and stuns them for 2 rounds.";
        public override int Cooldown => 4;
        public override int ResourceCost => 4;
        public override int PropertyMinDamage => 8;
        public override int PropertyMaxDamage => 12;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown <= 0)
            {
                PlayerEntity.Instance.Mana -= ResourceCost;
                Random rand = new Random();
                int damage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
                ActiveCooldown = Cooldown;
                return new CombatPayload(true, isStun: true, hasProperty: true, propertyAttackType: PropertyDamageType.Occult, propertyDamage: damage, stunCount: 2);
            }
            return new CombatPayload(false);
        }
    }
}
