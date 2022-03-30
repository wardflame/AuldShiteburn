using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
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

        public override bool ReceiveDamage(DamagePayload incomingDamage, int offsetY)
        {
            Utils.SetCursorInteract(offsetY);
            Console.Write($"{Name} ");
            int physDamage = incomingDamage.physicalDamage;
            if (PhysicalWeakness == incomingDamage.physicalDamageType)
            {
                physDamage += Combat.WEAKNESS_BONUS_MODIFIER;
                Console.Write($"is weak to ");
                Utils.WriteColour($"{incomingDamage.physicalDamageType}", ConsoleColor.DarkCyan);
                Console.Write(", taking ");
                Utils.WriteColour($"{physDamage} ", ConsoleColor.Green);
                Utils.WriteColour($"{incomingDamage.physicalDamageType} ", ConsoleColor.DarkCyan);
                Console.Write($"damage, ");
            }
            else
            {
                Console.Write("takes ");
                Utils.WriteColour($"{physDamage} ", ConsoleColor.DarkYellow);
                Console.Write($"{incomingDamage.physicalDamageType} damage, ");
            }
            int propDamage = incomingDamage.propertyDamage;
            Utils.SetCursorInteract(offsetY + 1);
            if (PropertyWeakness == incomingDamage.propertyDamageType)
            {
                propDamage += Combat.WEAKNESS_BONUS_MODIFIER;
                Console.Write($"is weak to ");
                Utils.WriteColour($"{incomingDamage.propertyDamageType}", ConsoleColor.DarkCyan);
                Console.Write(", taking ");
                Utils.WriteColour($"{propDamage} ", ConsoleColor.Green);
                Utils.WriteColour($"{incomingDamage.propertyDamageType} ", ConsoleColor.DarkCyan);
                Console.Write($" damage,");
            }
            else
            {
                if (incomingDamage.propertyDamageType == PropertyDamageType.Damaged)
                {
                    Console.Write("but ");
                    Utils.WriteColour($"{propDamage} ", ConsoleColor.DarkYellow);
                    Console.Write($"due to weapon being {incomingDamage.propertyDamageType}, ");
                }
                else
                {
                    Console.Write("and ");
                    Utils.WriteColour($"{propDamage} ", ConsoleColor.DarkYellow);
                    Console.Write($"{incomingDamage.propertyDamageType} damage, ");
                }
            }
            Utils.SetCursorInteract(offsetY + 2);
            int totalDamage = physDamage + propDamage;
            if (totalDamage < 0) totalDamage = 0;
            Console.Write($"for a total of ");
            Utils.WriteColour($"{totalDamage} ", ConsoleColor.Red);
            Console.Write($"damage.");
            HP -= totalDamage;
            if (HP <= 0)
            {
                Utils.SetCursorInteract(offsetY + 3);
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
