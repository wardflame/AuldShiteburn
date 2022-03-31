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

        public override bool ReceiveDamage(AttackPayload attackPayload, int offsetY)
        {
            Utils.SetCursorInteract(offsetY);
            Console.Write($"{Name} ");
            int physDamage = attackPayload.PhysicalDamage;
            if (attackPayload.HasPhysical)
            {
                if (PhysicalWeakness == attackPayload.PhysicalAttackType)
                {
                    physDamage += Combat.WEAKNESS_BONUS_MODIFIER;
                    Console.Write($"is weak to ");
                    Utils.WriteColour($"{attackPayload.PhysicalAttackType}", ConsoleColor.DarkCyan);
                    Console.Write(", taking ");
                    Utils.WriteColour($"{physDamage} ", ConsoleColor.Green);
                    Utils.WriteColour($"{attackPayload.PhysicalAttackType} ", ConsoleColor.DarkCyan);
                    Console.Write($"damage, ");
                }
                else
                {
                    Console.Write("takes ");
                    Utils.WriteColour($"{physDamage} ", ConsoleColor.DarkYellow);
                    Console.Write($"{attackPayload.PhysicalAttackType} damage, ");
                }
                Utils.SetCursorInteract(Console.CursorTop - 1);
            }
            int propDamage = attackPayload.PropertyDamage;
            if (attackPayload.HasProperty)
            {
                if (PropertyWeakness == attackPayload.PropertyAttackType)
                {
                    propDamage += Combat.WEAKNESS_BONUS_MODIFIER;
                    Console.Write($"is weak to ");
                    Utils.WriteColour($"{attackPayload.PropertyAttackType}", ConsoleColor.DarkCyan);
                    Console.Write(", taking ");
                    Utils.WriteColour($"{propDamage} ", ConsoleColor.Green);
                    Utils.WriteColour($"{attackPayload.PropertyAttackType} ", ConsoleColor.DarkCyan);
                    Console.Write($" damage,");
                }
                else
                {
                    if (attackPayload.PropertyAttackType == PropertyDamageType.Damaged)
                    {
                        Console.Write("but ");
                        Utils.WriteColour($"{propDamage} ", ConsoleColor.DarkYellow);
                        Console.Write($"due to weapon being {attackPayload.PropertyAttackType}, ");
                    }
                    else
                    {
                        if (attackPayload.HasPhysical)
                        {
                            Console.Write("and ");
                            Utils.WriteColour($"{propDamage} ", ConsoleColor.DarkYellow);
                            Console.Write($"{attackPayload.PropertyAttackType} damage, ");
                        }
                        else
                        {
                            Console.Write("takes ");
                            Utils.WriteColour($"{propDamage} ", ConsoleColor.DarkYellow);
                            Console.Write($"{attackPayload.PropertyAttackType} damage, ");
                        }                        
                    }
                }
                Utils.SetCursorInteract(Console.CursorTop - 1);
            }
            int totalDamage = physDamage + propDamage;
            if (totalDamage < 0) totalDamage = 0;
            Console.Write($"for a total of ");
            Utils.WriteColour($"{totalDamage} ", ConsoleColor.Red);
            Console.Write($"damage.");
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
