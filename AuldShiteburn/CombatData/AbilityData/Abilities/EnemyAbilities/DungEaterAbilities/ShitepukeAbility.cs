using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.DungEaterAbilities
{
    [Serializable]
    internal class ShitepukeAbility : Ability
    {
        public override string Name => "Shite Puke";
        public override string Description => $"wretches and vomits shite-filled fluids at {PlayerEntity.Instance.Name}!";
        public override int Cooldown => 4;
        public override int ResourceCost => 0;
        public override PropertyDamageType PropertyDamageType => PropertyDamageType.Occult;
        public override int PropertyMinDamage => 7;
        public override int PropertyMaxDamage => 11;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            Random rand = new Random();
            int propDamage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
            return new CombatPayload(
                isAttack: true,
                hasProperty: true,
                propertyAttackType: PropertyDamageType,
                propertyDamage: propDamage);
        }
    }
}
