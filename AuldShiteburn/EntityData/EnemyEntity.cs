using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.PayloadData;
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
        public PhysicalDamageType PhysicalWeakness { get; protected set; }
        public GeneralMaterials MaterialWeakness { get; protected set; }
        public PropertyDamageType PropertyWeakness { get; protected set; }
        
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
                ability = Abilities[rand.Next(0, Abilities.Count)];
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

        public override bool ReceiveAttack(CombatPayload combatPayload, int offsetY)
        {
            Utils.SetCursorInteract(offsetY);
            Utils.WriteColour($"{Name} ");
            Utils.WriteColour($"{HP}/{MaxHP} ", ConsoleColor.Red);
            int physDamage = combatPayload.PhysicalDamage;
            if (combatPayload.HasPhysical)
            {
                if (PhysicalWeakness == combatPayload.PhysicalAttackType)
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
            if (combatPayload.HasProperty)
            {
                if (PropertyWeakness == combatPayload.PropertyAttackType)
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
                            Utils.WriteColour($"{propDamage} ", ConsoleColor.DarkYellow);
                            Utils.WriteColour($"{combatPayload.PropertyAttackType} damage, ");
                        }
                        else
                        {
                            Utils.WriteColour("takes ");
                            Utils.WriteColour($"{propDamage} ", ConsoleColor.DarkYellow);
                            Utils.WriteColour($"{combatPayload.PropertyAttackType} damage, ");
                        }                        
                    }
                }
                Utils.SetCursorInteract(Console.CursorTop - 1);
            }
            int totalDamage = physDamage + propDamage;
            if (totalDamage < 0) totalDamage = 0;
            Utils.WriteColour($"for a total of ");
            Utils.WriteColour($"{totalDamage} ", ConsoleColor.Red);
            Utils.WriteColour($"damage.");
            if (combatPayload.IsStun)
            {
                Utils.SetCursorInteract(Console.CursorTop - 1);
                if (!Stunned)
                {
                    StunTimer = combatPayload.StunCount;
                    Utils.WriteColour($"{Name} ");
                    Utils.WriteColour($"{HP}/{MaxHP} ", ConsoleColor.Red);
                    Utils.WriteColour($"is stunned for ");
                    Utils.WriteColour($"{StunTimer} ", ConsoleColor.Blue);
                    Utils.WriteColour($"turns!");
                }
                else
                {
                    Utils.WriteColour($"{Name} ", ConsoleColor.DarkYellow);
                    Utils.WriteColour($"{HP}/{MaxHP} ", ConsoleColor.Red);
                    Utils.WriteColour($"is already stunned and cannot be again for ", ConsoleColor.DarkYellow);
                    Utils.WriteColour($"{StunTimer} ", ConsoleColor.Blue);
                    Utils.WriteColour($"turns!", ConsoleColor.DarkYellow);
                }
            }
            HP -= totalDamage;
            if (HP <= 0)
            {
                Utils.SetCursorInteract(Console.CursorTop);
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
