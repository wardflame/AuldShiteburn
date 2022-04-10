using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.ShiteAvatarAbilities
{
    [Serializable]
    internal class EldritchEruptionAbility : Ability
    {
        public override string Name => "Eldritch Eruption";
        public override string Description => $"lifts its bulk high into the air and charges a cosmic blast!";
        public override int Cooldown => 5;
        public override int ResourceCost => 0;
        public override PhysicalDamageType PhysicalDamageType => PhysicalDamageType.Strike;
        public override PropertyDamageType PropertyDamageType => PropertyDamageType.Occult;
        public override int PhysicalMinDamage => 9;
        public override int PhysicalMaxDamage => 13;
        public override int PropertyMinDamage => 7;
        public override int PropertyMaxDamage => 11;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            if (PlayerEntity.Instance.CarryingMoonlitAmulet)
            {
                Utils.SetCursorInteract(Console.CursorTop);
                Utils.WriteColour("As the cosmic ray flies, the Moonlit Amulet forms a barrier, glancing the blow!", ConsoleColor.Cyan);
                return new CombatPayload(false);
            }
            else
            {
                Random rand = new Random();
                int physDamage = rand.Next(PhysicalMinDamage, PhysicalMaxDamage + 1);
                int propDamage = rand.Next(PropertyMinDamage, PropertyMaxDamage + 1);
                return new CombatPayload(
                    isAttack: true, isStun: true,
                    hasPhysical: true, hasProperty: true,
                    physicalAttackType: PhysicalDamageType, propertyAttackType: PropertyDamageType,
                    physicalDamage: physDamage, propertyDamage: propDamage,
                    stunCount: 2);
            }
        }
    }
}
