using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData
{
    internal class Combat
    {
        public const int PROFICIENCY_DAMAGE_MODIFIER = 2;
        public const int PROFICIENCY_ARMOUR_MODIFIER_MINOR = 2;
        public const int PROFICIENCY_ARMOUR_MODIFIER = 4;
        public const int ARMOUR_RESISTANCE_MITIGATION = 2;
        public const int WEAKNESS_BONUS = 3;

        public static void CombatEncounter(List<EnemyEntity> enemies)
        {
            while (enemies.Count > 0 || PlayerEntity.Instance.HP > 0)
            {
                bool playerAttacking = true;
                while (playerAttacking)
                {
                    int index = CycleEnemies(enemies);
                    int attackChoice = AttackChoice(enemies, index);
                    if (attackChoice > 0)
                    {
                        if (attackChoice == 1)
                        {
                            Damage playerDamagePayload = CalculateWeaponDamage();
                            enemies[index].ReceiveDamage(playerDamagePayload, enemies.Count + 6);
                        }
                        else if (attackChoice == 2)
                        {

                        }
                    }
                    Console.ReadKey();
                }                
                Utils.SetCursorInteract();
                break;
            }
        }

        private static Damage CalculateWeaponDamage()
        {
            Random rand = new Random();
            WeaponItem playerWeapon = PlayerEntity.Instance.EquippedWeapon;
            int physDamage = rand.Next(playerWeapon.MinPhysDamage, playerWeapon.MaxPhysDamage);
            int propDamage = rand.Next(playerWeapon.MinPropDamage, playerWeapon.MaxPropDamage);
            return new Damage(physDamage, propDamage, playerWeapon.Type.PrimaryAttack, playerWeapon.Property.Property);
        }

        private static int AttackChoice(List<EnemyEntity> enemies, int index)
        {
            Utils.SetCursorInteract(enemies.Count + 2);
            Console.Write($"What do you want to use against ");
            Utils.WriteColour($"{enemies[index].Name} ", ConsoleColor.Cyan);
            Utils.WriteColour($"{enemies[index].HP}/{enemies[index].MaxHP}", ConsoleColor.Red);
            Console.Write("? (1/2)");
            Utils.SetCursorInteract(enemies.Count + 3);
            Console.Write("1. ");
            PlayerEntity.Instance.PrintWeapon();
            Utils.SetCursorInteract(enemies.Count + 4);
            Console.Write("2. Ability");
            do
            {
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.D1:
                        {
                            return 1;
                        }
                        break;
                    case ConsoleKey.D2:
                        {
                            return 2;
                        }
                        break;
                }
            } while (InputSystem.InputKey != ConsoleKey.D1 && InputSystem.InputKey != ConsoleKey.D2);
            return 0;
        }

        private static int CycleEnemies(List<EnemyEntity> enemies)
        {
            Utils.SetCursorInteract();
            Utils.WriteColour("Choose an enemy to attack.", ConsoleColor.DarkYellow);
            int index = 0;
            PrintEnemies(enemies, index);
            do
            {
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (index <= enemies.Count - 1 && index > 0)
                            {
                                index--;
                                Console.CursorLeft = 0;
                                Console.CursorTop = 0;
                                PrintEnemies(enemies, index);
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (index >= 0 && index < enemies.Count - 1)
                            {
                                index++;
                                Console.CursorLeft = 0;
                                Console.CursorTop = 0;
                                PrintEnemies(enemies, index);
                            }
                        }
                        break;
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter);
            return index;
        }

        private static void PrintEnemies(List<EnemyEntity> enemies, int index)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                Utils.SetCursorInteract(i + 1);
                Utils.ClearLine(30);
                Utils.SetCursorInteract(i + 1);
                if (enemies[i] == enemies[index])
                {
                    Utils.WriteColour(">>", ConsoleColor.Yellow);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.Write($"{enemies[i].Name} ");
                Console.ResetColor();
                Utils.WriteColour($"{enemies[i].HP}/{enemies[i].MaxHP} ", ConsoleColor.Red);
                Console.Write($"HP\n");
            }
        }
    }
}
