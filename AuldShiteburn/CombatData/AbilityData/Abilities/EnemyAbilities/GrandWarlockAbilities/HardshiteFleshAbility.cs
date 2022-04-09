using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData.AbilityData.Abilities.EnemyAbilities.GrandWarlockAbilities
{
    [Serializable]
    internal class HardshiteFleshAbility : Ability
    {
        public override string Name => "Hardshite Flesh";
        public override string Description => "smears their flesh in hardening shite, providing defense!.";
        public override int Cooldown => 6;
        public override int ResourceCost => 0;

        public override CombatPayload UseAbility(List<EnemyEntity> enemies, EnemyEntity enemy = null)
        {
            Utils.SetCursorInteract(Console.CursorTop + 1);
            if (ActiveCooldown == 0)
            {
                enemy.StatusEffect = DefenseStatusEffect.HardshiteFlesh;
                ActiveCooldown = Cooldown;
                return new CombatPayload(isAttack: false, isUtility: true);
            }
            return new CombatPayload(false);
        }
    }
}
