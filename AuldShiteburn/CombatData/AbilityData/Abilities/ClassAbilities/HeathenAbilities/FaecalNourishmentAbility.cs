using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.HeathenAbilities
{
    [Serializable]
    internal class FaecalNourishmentAbility : Ability
    {
        public override string Name => "Faecal Nourishment";
        public override string Description => "Take nourishment in shite, restoring 6-12 HP.";
        public override int Cooldown => 3;
        public override int ResourceCost => 4;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown <= 0)
            {
                PlayerEntity.Instance.Mana -= ResourceCost;
                Random rand = new Random();
                int heal = rand.Next(6, 13);
                PlayerEntity.Instance.HP += heal;
                ActiveCooldown = Cooldown;
                return new CombatPayload(isAttack: false, isUtility: true);
            }
            return new CombatPayload(false);
        }
    }
}
