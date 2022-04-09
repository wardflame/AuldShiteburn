using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.ClassAbilities.RogueAbilities
{
    [Serializable]
    internal class SmokeBombAbility : Ability
    {
        public override string Name => "Smoke Bomb";
        public override string Description => $"Blind enemies, stunning them for 1 turn, staggering for 2 turns, and recuperate 6-10 Stamina.";
        public override int Cooldown => 5;
        public override int ResourceCost => 0;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            int offsetY = Console.CursorTop + 1;
            Utils.SetCursorInteract(Console.CursorTop);
            if (ActiveCooldown <= 0)
            {
                PlayerEntity.Instance.Stamina -= ResourceCost;
                Random rand = new Random();
                for (int i = 0; i < enemies.Count; i++)
                {
                    Utils.ClearInteractArea(offsetY, 10);
                    Utils.SetCursorInteract(offsetY - 2);
                    if (enemies[i].ReceiveAttack
                        (new CombatPayload(
                            isAttack: false, isUtility: true, isStun: true,
                            hasStatus: true,
                            statusEffect: DefenseStatusEffect.Staggered2,
                            stunCount: 1), Console.CursorTop + 1))
                    {
                        enemies.Remove(enemies[i]);
                        i = -1;
                    }
                    Utils.SetCursorInteract(Console.CursorTop);
                    if (i < enemies.Count - 1)
                    {
                        Utils.WriteColour("Press any key to continue.");
                        Console.ReadKey(true);
                    }
                }
                int staminaRecovery = rand.Next(6, 11);
                PlayerEntity.Instance.Stamina += staminaRecovery;
                Utils.ClearInteractArea(Console.CursorTop - 3, 1);
                Utils.SetCursorInteract(Console.CursorTop - 4);
                ActiveCooldown = Cooldown;
                return new CombatPayload(false, true);
            }
            return new CombatPayload(false);
        }
    }
}
