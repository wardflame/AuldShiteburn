using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.BloatedBruiserAbilities
{
    [Serializable]
    internal class PussBathAbility : Ability
    {
        public override string Name => "Puss Bath";
        public override string Description => "seals some wounds with an expulsion of puss.";
        public override int Cooldown => 3;
        public override int ResourceCost => 0;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown <= 0)
            {
                Random rand = new Random();
                int heal = rand.Next(4, 9);
                enemy.HP += heal;
                ActiveCooldown = Cooldown;
                return new CombatPayload(isAttack: false, isUtility: true);
            }
            return new CombatPayload(false);
        }
    }
}
