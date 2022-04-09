using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData;
using AuldShiteburn.ItemData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData
{
    [Serializable]
    internal class EnemyEntity : LivingEntity
    {
        public override string EntityChar => "";
        public List<Ability> Abilities { get; protected set; }
        public StatusEffect StatusEffect { get; set; }
        public List<PhysicalDamageType> PhysicalWeaknesses { get; protected set; }
        public List<PropertyDamageType> PropertyWeaknesses { get; protected set; }

        /// <summary>
        /// Reduce all active cooldowns by one and then filter through abilities
        /// and pick one at random to use. If the ability is on cooldown, try again.
        /// </summary>
        public CombatPayload PerformAttack(List<EnemyEntity> enemies)
        {
            bool coolingDown = true;
            while (coolingDown)
            {
                foreach (Ability abilityToCooldown in Abilities)
                {
                    if (abilityToCooldown.ActiveCooldown > 0)
                    {
                        abilityToCooldown.ActiveCooldown--;
                    }
                }
                coolingDown = false;
            }
            Random rand = new Random();
            bool attacking = true;
            while (attacking)
            {
                Ability ability;
                if (Abilities.Count < 1)
                {
                    break;
                }
                ability = Abilities[rand.Next(Abilities.Count)];
                if (ability.ActiveCooldown == 0)
                {
                    Utils.SetCursorInteract();
                    Utils.WriteColour($"{Name} ", ConsoleColor.DarkYellow);
                    Utils.WriteColour($"{HP}/{MaxHP} ", ConsoleColor.Red);
                    Utils.WriteColour($"{ability.Description}", ConsoleColor.DarkYellow);
                    return ability.UseAbility(enemies);
                }
            }
            return new CombatPayload(false);
        }

        public override bool ReceiveAttack(CombatPayload combatPayload, int offsetY, LivingEntity aggressor = null)
        {
            Utils.SetCursorInteract(offsetY);
            Utils.WriteColour($"{Name} ");
            Utils.WriteColour($"{HP}/{MaxHP} ", ConsoleColor.Red);

            #region Status and Stun Check
            if (combatPayload.HasStatus)
            {
                StatusEffect = combatPayload.StatusEffect;
                Utils.WriteColour($"is afflicted with ");
                Utils.WriteColour($"{StatusEffect.Name}", StatusEffect.DisplayColor);
                Utils.WriteColour($",");
                Utils.SetCursorInteract(Console.CursorTop - 1);
                combatPayload = StatusEffect.EffectActive(combatPayload);
                JustAfflicted = true;
            }

            if (combatPayload.IsStun)
            {
                if (!Stunned)
                {
                    StunTimer = combatPayload.StunCount;
                    Utils.WriteColour($"is stunned for ");
                    Utils.WriteColour($"{StunTimer} ", ConsoleColor.Blue);
                    Utils.WriteColour($"rounds,");
                    JustStunned = true;
                }
                else
                {
                    Utils.WriteColour($"{Name} ", ConsoleColor.DarkYellow);
                    Utils.WriteColour($"{HP}/{MaxHP} ", ConsoleColor.Red);
                    Utils.WriteColour($"is already stunned and cannot be again for ", ConsoleColor.DarkYellow);
                    Utils.WriteColour($"{StunTimer} ", ConsoleColor.Blue);
                    Utils.WriteColour($"rounds,", ConsoleColor.DarkYellow);
                }
                Utils.SetCursorInteract(Console.CursorTop - 1);
            }
            #endregion Status and Stun Check

            #region Damage Calculation
            int physDamage = combatPayload.PhysicalDamage;
            if (combatPayload.HasPhysical)
            {
                bool physTypeWeakness = false;
                foreach (PhysicalDamageType physWeakness in PhysicalWeaknesses)
                {
                    if (physWeakness == combatPayload.PhysicalAttackType)
                    {
                        physTypeWeakness = true;
                        break;
                    }
                }
                if (physTypeWeakness)
                {
                    physDamage += Combat.WEAKNESS_BONUS_MODIFIER;
                    Utils.WriteColour($"is weak to ");
                    Utils.WriteColour($"{combatPayload.PhysicalAttackType}", ConsoleColor.DarkCyan);
                    Utils.WriteColour(", taking ");
                    Utils.WriteColour($"{physDamage} ", ConsoleColor.Green);
                    Utils.WriteColour($"{combatPayload.PhysicalAttackType} ", ConsoleColor.DarkCyan);
                    Utils.WriteColour($"damage, ");
                }
                else
                {
                    Utils.WriteColour("takes ");
                    Utils.WriteColour($"{physDamage} ", ConsoleColor.DarkYellow);
                    Utils.WriteColour($"{combatPayload.PhysicalAttackType} damage, ");
                }
                Utils.SetCursorInteract(Console.CursorTop - 1);
            }
            int propDamage = combatPayload.PropertyDamage;
            if (combatPayload.HasProperty && combatPayload.PropertyAttackType != PropertyDamageType.Standard)
            {
                bool propTypeWeakness = false;
                foreach (PropertyDamageType propWeakness in PropertyWeaknesses)
                {
                    if (propWeakness == combatPayload.PropertyAttackType)
                    {
                        propTypeWeakness = true;
                        break;
                    }
                }
                if (propTypeWeakness)
                {
                    propDamage += Combat.WEAKNESS_BONUS_MODIFIER;
                    Utils.WriteColour($"is weak to ");
                    Utils.WriteColour($"{combatPayload.PropertyAttackType}", ConsoleColor.DarkCyan);
                    Utils.WriteColour(", taking ");
                    Utils.WriteColour($"{propDamage} ", ConsoleColor.Green);
                    Utils.WriteColour($"{combatPayload.PropertyAttackType} ", ConsoleColor.DarkCyan);
                    Utils.WriteColour($"damage,");
                }
                else
                {
                    if (combatPayload.PropertyAttackType == PropertyDamageType.Damaged)
                    {
                        Utils.WriteColour("but ");
                        Utils.WriteColour($"{propDamage} ", ConsoleColor.DarkYellow);
                        Utils.WriteColour($"due to weapon being {combatPayload.PropertyAttackType}, ");
                    }
                    else
                    {
                        if (combatPayload.HasPhysical)
                        {
                            Utils.WriteColour("and ");
                        }
                        else
                        {
                            Utils.WriteColour("takes ");
                        }
                        Utils.WriteColour($"{propDamage} ", ConsoleColor.DarkYellow);
                        Utils.WriteColour($"{combatPayload.PropertyAttackType} damage, ");
                    }
                }
                Utils.SetCursorInteract(Console.CursorTop - 1);
            }
            #endregion Damage Calculation

            int totalDamage = physDamage + propDamage;
            if (totalDamage < 0) totalDamage = 0;
            if (totalDamage > 0)
            {
                Utils.WriteColour($"for a total of ");
                Utils.WriteColour($"{totalDamage} ", ConsoleColor.Red);
                Utils.WriteColour($"damage.");
                HP -= totalDamage;
            }
            if (HP <= 0)
            {
                Utils.SetCursorInteract(Console.CursorTop - 1);
                Utils.WriteColour($"{Name} is slain by the blow!", ConsoleColor.Green);
                return true;
            }
            return false;
        }

        public override void Move()
        {
        }
    }
}
