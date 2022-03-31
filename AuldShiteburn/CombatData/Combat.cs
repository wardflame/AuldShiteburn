﻿using AuldShiteburn.ArtData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData
{
    internal class Combat
    {
        public const int PROFICIENCY_DAMAGE_BONUS_MINOR = 2;
        public const int PROFICIENCY_DAMAGE_BONUS_MODERATE = 4;
        public const int PROFICIENCY_DAMAGE_BONUS_MAJOR = 6;
        public const int PROFICIENCY_ARMOUR_MITIGATION_MINOR = 2;
        public const int PROFICIENCY_ARMOUR_MITIGATION_MODERATE = 4;
        public const int PROFICIENCY_ARMOUR_MITIGATION_MAJOR = 6;
        public const int STATUS_MITIGATION_MINOR = 2;
        public const int STATUS_MITIGATION_MODERATE = 4;
        public const int STATUS_MITIGATION_MAJOR = 6;
        public const int ARMOUR_RESISTANCE_MITIGATION_MODIFIER = 2;
        public const int WEAKNESS_BONUS_MODIFIER = 2;

        public static bool CombatEncounter(List<EnemyEntity> enemies)
        {
            while (enemies.Count > 0 && PlayerEntity.Instance.HP > 0)
            {
                PlayerCombatTurn(enemies);
            }
            if (PlayerEntity.Instance.HP > 0)
            {
                Utils.SetCursorInteract();
                ASCIIArt.PrintASCII(ASCIIArt.VICTORY_MESSAGE, ConsoleColor.DarkGreen);
                Console.CursorTop += 1;
                Console.Write("Press any key to continue...");
                Console.ReadKey();
                return true;
            }
            else
            {
                Utils.SetCursorInteract();
                ASCIIArt.PrintASCII(ASCIIArt.DEATH_MESSAGE, ConsoleColor.DarkRed);
                Console.CursorTop += 1;
                Console.Write("Press any key to continue...");
                Console.ReadKey();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Present player with combat targets and allow them
        /// the opportunity to select and attack one in their turn.
        /// </summary>
        /// <param name="enemies">List of enemies in the area.</param>
        private static void PlayerCombatTurn(List<EnemyEntity> enemies)
        {
            bool playerTurn = true;
            while (playerTurn)
            {
                if (PlayerEntity.Instance.Stunned)
                {
                    PlayerEntity.Instance.StunTimer--;
                }
                if (PlayerEntity.Instance.StunTimer <= 0)
                {
                    int index = CycleEnemies(enemies);
                    int attackChoice = AttackChoice(enemies, index);
                    EnemyEntity enemy = enemies[index];
                    if (attackChoice > 0)
                    {
                        if (attackChoice == 1)
                        {
                            CombatPayload playerAttackPayload = CalculateWeaponDamage();
                            if (enemy.ReceiveDamage(playerAttackPayload, enemies.Count + 6))
                            {
                                enemies.Remove(enemy);
                            }
                            playerTurn = false;
                        }
                        else if (attackChoice == 2)
                        {
                            bool firing = true;
                            while (firing)
                            {
                                Utils.ClearAreaInteract(enemies.Count + 6, 10);
                                (int chosenAbility, bool chosen) = PlayerEntity.Instance.CycleAbilities(enemies.Count + 6, 0);
                                if (chosen)
                                {
                                    CombatPayload playerCombatPayload = PlayerEntity.Instance.Class.Abilities[chosenAbility].UseAbility();
                                    if (playerCombatPayload.IsAttack)
                                    {
                                        if (enemy.ReceiveDamage(playerCombatPayload, Console.CursorTop - 1))
                                        {
                                            enemies.Remove(enemy);
                                            firing = false;
                                        }
                                        else
                                        {
                                            firing = false;
                                        }
                                    }
                                    if (playerCombatPayload.IsStun)
                                    {
                                        if (!enemy.Stunned)
                                        {
                                            enemy.Stunned = true;
                                            enemy.StunTimer = enemy.StunCap;
                                            Utils.WriteColour($"{enemy.Name} stunned for {enemy.StunTimer} turns!", ConsoleColor.DarkBlue);
                                        }
                                    }
                                    playerTurn = false;
                                }
                                else
                                {
                                    Utils.SetCursorInteract(enemies.Count + 2);
                                    Utils.ClearAreaInteract(length: 20);
                                    firing = false;
                                }
                            }
                        }
                    }
                }                
            }
            Utils.SetCursorInteract(Console.CursorTop);
            Console.Write("Press any key to progress...");
            Console.ReadKey();
            Utils.SetCursorInteract(Console.CursorTop);
            Utils.ClearInteractInterface(30);
        }

        /// <summary>
        /// Generate an attack payload from the player's equipped weapon,
        /// randomising between the min and max physical and property
        /// damages.
        /// </summary>
        /// <returns>Damage payload for enemy to process.</returns>
        private static CombatPayload CalculateWeaponDamage()
        {
            CombatPayload attackPayload = new CombatPayload();
            Random rand = new Random();
            WeaponItem playerWeapon = PlayerEntity.Instance.EquippedWeapon;
            attackPayload.PhysicalDamage = rand.Next(playerWeapon.MinPhysDamage, playerWeapon.MaxPhysDamage);
            if (attackPayload.PhysicalDamage > 0)
            {
                attackPayload.PhysicalAttackType = playerWeapon.Type.PrimaryAttack;
                attackPayload.HasPhysical = true;
            }
            attackPayload.PropertyDamage = rand.Next(playerWeapon.MinPropDamage, playerWeapon.MaxPropDamage);
            if (attackPayload.PropertyDamage > 0 || playerWeapon.Property.Type == PropertyDamageType.Damaged)
            {
                attackPayload.PropertyAttackType = playerWeapon.Property.Type;
                attackPayload.HasProperty = true;
            }
            return attackPayload;
        }

        /// <summary>
        /// Offer player choice to use weapon or playerAttackPayload, return choice.
        /// </summary>
        /// <param name="enemies">List of enemies.</param>
        /// <param name="index">Index of the enemy in the list.</param>
        /// <returns>Returns option chosen.</returns>
        private static int AttackChoice(List<EnemyEntity> enemies, int index)
        {
            Utils.SetCursorInteract(enemies.Count + 2);
            Console.Write($"What do you want to use against ");
            Utils.WriteColour($"{enemies[index].Name} ", ConsoleColor.Cyan);
            Utils.WriteColour($"{enemies[index].HP}/{enemies[index].MaxHP}", ConsoleColor.Red);
            Console.Write("? (1/2)");
            Utils.SetCursorInteract(enemies.Count + 3);
            Console.Write("1. ");
            Utils.WriteColour("Equipped Weapon: ", ConsoleColor.DarkYellow);
            PlayerEntity.Instance.PrintWeapon();
            Utils.SetCursorInteract(enemies.Count + 4);
            Console.Write("2. ");
            Utils.WriteColour("Ability ", ConsoleColor.DarkYellow);
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

        /// <summary>
        /// Get a list of enemies and cycle through them, returning the index
        /// of the enemy the player's selected.
        /// </summary>
        /// <param name="enemies">List of enemies to cycle.</param>
        /// <returns>Return index of chosen enemy.</returns>
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

        /// <summary>
        /// For each enemy in a list, print them out,
        /// highlighting the one at a desired index.
        /// </summary>
        /// <param name="enemies">List of enemies to print.</param>
        /// <param name="index">Index of enemy to highlight.</param>
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

    public enum EffectLevel
    {
        None,
        Minor,
        Moderate,
        Major
    }
}
