﻿using AuldShiteburn.BackendData;
using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData;
using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.EntityData.PlayerData.Classes;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MapData;
using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData
{
    [Serializable]
    internal class PlayerEntity : LivingEntity
    {
        public static PlayerEntity Instance { get; set; }
        public override string EntityChar => "PL";
        public bool QuittingToMenu { get; set; } = false;
        public bool InMenu { get; set; }
        public long Playtime { get; set; }
        public Inventory Inventory { get; set; } = new Inventory();
        public CharacterClass Class { get; private set; }
        public StatusEffect StatusEffect { get; set; }
        public WeaponItem EquippedWeapon { get; set; }
        public ArmourItem EquippedArmour { get; set; }

        public override void Move()
        {
            if (!InMenu)
            {
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.W:
                        {
                            PosY--;
                        }
                        break;

                    case ConsoleKey.S:
                        {
                            PosY++;
                        }
                        break;

                    case ConsoleKey.A:
                        {
                            PosX--;
                        }
                        break;

                    case ConsoleKey.D:
                        {
                            PosX++;
                        }
                        break;
                    case ConsoleKey.I:
                        {
                            Inventory.ItemInteract();
                            Instance.PrintStats();
                            Instance.PrintInventory();
                        }
                        break;
                    case ConsoleKey.Escape:
                        {
                            Menu.Instance = new PauseMenu();
                            Menu.Instance.RunMenu();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Generate a new player character through randomly generating
        /// class (which have preset stats), names and loot.
        /// </summary>
        /// <returns>New player character.</returns>
        public static PlayerEntity GenerateCharacter()
        {
            Instance = new PlayerEntity();
            Instance.PosX = 1;
            Instance.PosY = 1;
            Random rand = new Random();

            #region Class Generation and Stat Assignments
            //Instance.Class = CharacterClass.Classes[rand.Next(CharacterClass.Classes.Count)];
            Instance.Class = new HeathenClass();
            Instance.MaxHP = Instance.Class.Statistics.hp;
            Instance.HP = Instance.Class.Statistics.hp;
            Instance.UsesStamina = Instance.Class.Statistics.usesStamina;
            Instance.UsesMana = Instance.Class.Statistics.usesMana;
            Instance.MaxStamina = Instance.Class.Statistics.stamina;
            Instance.Stamina = Instance.Class.Statistics.stamina;
            Instance.MaxMana = Instance.Class.Statistics.mana;
            Instance.Mana = Instance.Class.Statistics.mana;
            #endregion Class Generation

            #region Name Generation
            string titleStr;
            string forenameStr;
            int sexChance = rand.Next(1, 101);
            if (sexChance <= GameSettings.Instance.SexRatio)
            {
                forenameStr = PlayerGenerationData.NamesMale[rand.Next(PlayerGenerationData.NamesMale.Count)];
                int titleIndex = rand.Next(0, Instance.Class.Titles.titleMale.Count);
                titleStr = Instance.Class.Titles.titleMale[titleIndex];
                Instance.Name = $"{titleStr} {forenameStr}";

            }
            else
            {
                forenameStr = PlayerGenerationData.NameFemale[rand.Next(PlayerGenerationData.NameFemale.Count)];
                int titleIndex = rand.Next(0, Instance.Class.Titles.titleFemale.Count);
                titleStr = Instance.Class.Titles.titleFemale[titleIndex];
                Instance.Name = $"{titleStr} {forenameStr}";
            }
            #endregion Name Generation

            #region Loot Assignment
            Instance.EquippedWeapon = WeaponItem.GenerateSpawnWeapon(Instance.Class.ClassType);
            //Instance.Inventory.ItemList[0, 1] = new ArmourItem("Plate");
            #endregion Loot Assignment

            return Instance;
        }

        public override bool ReceiveDamage(CombatPayload attackPayload, int offsetY = 0)
        {
            int initialPhys = attackPayload.PhysicalDamage;
            int initialProp = attackPayload.PropertyDamage;
            int totalDamage = 0;
            Utils.SetCursorInteract();
            if (Instance.EquippedArmour != null)
            {
                ArmourItem playerArmour = Instance.EquippedArmour;
                bool physRes = playerArmour.PrimaryPhysicalResistance == attackPayload.PhysicalAttackType;
                bool propRes = playerArmour.PrimaryPropertyResistance == attackPayload.PropertyAttackType;

                Console.Write($"{Instance.Name} takes ");
                int physDamage = attackPayload.PhysicalDamage -= playerArmour.PhysicalMitigation;
                if (physRes)
                {
                    physDamage -= Combat.ARMOUR_RESISTANCE_MITIGATION_MODIFIER;
                }
                if (physDamage < 0) physDamage = 0;
                Utils.WriteColour($"{physDamage}/{initialPhys} ", ConsoleColor.Red);
                int propDamage = attackPayload.PropertyDamage -= playerArmour.PropertyMitigation;
                if (propRes)
                {
                    propDamage -= Combat.ARMOUR_RESISTANCE_MITIGATION_MODIFIER;
                }
                if (propDamage < 0) propDamage = 0;
                Console.Write("and ");
                Utils.WriteColour($"{propDamage}/{initialProp} ", ConsoleColor.Red);
                Console.Write("property damage ");
                totalDamage = physDamage + propDamage;
                Console.Write($"for a total of ");
                Utils.WriteColour($"{totalDamage} ", ConsoleColor.Red);
                Console.Write($"damage.");
            }
            else
            {
                Console.Write($"{Instance.Name} takes ");
                totalDamage += attackPayload.PhysicalDamage;
                Utils.WriteColour($"{attackPayload.PhysicalDamage} ", ConsoleColor.Red);
                Console.Write($"physical damage and ");
                totalDamage += attackPayload.PropertyDamage;
                Utils.WriteColour($"{attackPayload.PropertyDamage} ", ConsoleColor.Red);
                Console.Write($"for a total of ");
                Utils.WriteColour($"{totalDamage} ", ConsoleColor.Red);
                Console.Write($"damage.");
            }
            Instance.HP -= totalDamage;
            if (Instance.HP <= 0)
            {
                return true;
                // WE GOT A LOT TO DO!
            }
            Instance.PrintStats();
            return false;
        }

        /// <summary>
        /// Print the player's inventory underneath player stats.
        /// </summary>
        public void PrintInventory()
        {
            Utils.ClearInventoryInterface();
            Utils.SetCursorInventory();
            Inventory.PrintInventory(Inventory.PLAYER_WEAPON_OFFSET, Inventory.PLAYER_ARMOUR_OFFSET, Inventory.PLAYER_CONSUMABLE_OFFSET, Inventory.PLAYER_KEY_OFFSET);
        }

        /// <summary>
        /// Print the player's name and stats beneath the area map.
        /// </summary>
        public void PrintStats()
        {
            Utils.ClearPlayerStatInterface();
            Utils.SetCursorPlayerStat();
            Console.Write($"{Instance.Name} the {Instance.Class.Name}");

            Utils.SetCursorPlayerStat(1);
            Console.Write("- - - - - - - -");
            Utils.SetCursorPlayerStat(2);
            Console.Write($"Health: ");
            Utils.WriteColour($"{Instance.HP}", ConsoleColor.Red);

            if (Instance.UsesStamina)
            {
                Utils.SetCursorPlayerStat(3);
                Console.Write($"Stamina: ");
                Utils.WriteColour($"{Instance.Stamina}", ConsoleColor.Green);
            }
            if (Instance.UsesMana)
            {
                Utils.SetCursorPlayerStat(3);
                Console.Write($"Mana: ");
                Utils.WriteColour($"{Instance.Mana}", ConsoleColor.Blue);
            }
            Utils.SetCursorPlayerStat(4);
            Console.Write("Status Effect: ");
            if (Instance.StatusEffect != null)
            {
                Utils.WriteColour($"{Instance.StatusEffect.Name}: ", Instance.StatusEffect.DisplayColor);
                Console.Write($"{Instance.StatusEffect.Duration}");
            }
            else
            {
                Console.Write("--");
            }
            Utils.SetCursorPlayerStat(5);
            Console.Write("- - - - - - - -");

            Utils.SetCursorPlayerStat(6);
            Utils.WriteColour("Equipped Weapon", ConsoleColor.DarkYellow);
            Utils.SetCursorPlayerStat(7);
            Console.Write(">> ");
            PrintWeapon();
            Utils.SetCursorPlayerStat(9);
            Utils.WriteColour("Equipped Armour", ConsoleColor.DarkYellow);
            Utils.SetCursorPlayerStat(10);
            Console.Write(">> ");
            PrintArmour();
        }

        /// <summary>
        /// Print the player's weapon with highlights for
        /// proficiency.
        /// </summary>
        public void PrintWeapon()
        {
            if (Instance.EquippedWeapon != null)
            {
                if (Instance.EquippedWeapon.Property.HasAffinity)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write($"{Instance.EquippedWeapon.Property.Name} ");
                Console.ResetColor();

                if (Instance.EquippedWeapon.Material.HasAffinity)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write($"{Instance.EquippedWeapon.Material.Name} ");
                Console.ResetColor();

                if (Instance.EquippedWeapon.Type.IsProficient)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write($"{Instance.EquippedWeapon.Type.Name} ");
                Console.ResetColor();
            }
            else
            {
                Console.Write("--");
            }
        }

        /// <summary>
        /// Print the player's armour with highlights for
        /// proficiency.
        /// </summary>
        public void PrintArmour()
        {
            if (Instance.EquippedArmour != null)
            {
                if (Instance.Class.Proficiencies.armourProficiency == Instance.EquippedArmour.ArmourFamily)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write($"{Instance.EquippedArmour.Name}");
                Console.ResetColor();
            }
            else
            {
                Console.Write("--");
            }
        }

        public (int, bool) CycleAbilities(int offsetY, int index)
        {
            PrintAbilitiesOptions(offsetY, index);
            do
            {
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (index <= Instance.Class.Abilities.Count - 1 && index > 0)
                            {
                                index--;
                                Utils.ClearAreaInteract(offsetY, Instance.Class.Abilities.Count + 4);
                                PrintAbilitiesOptions(offsetY, index);
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (index >= 0 && index < Instance.Class.Abilities.Count - 1)
                            {
                                index++;
                                Utils.ClearAreaInteract(offsetY, Instance.Class.Abilities.Count + 4);
                                PrintAbilitiesOptions(offsetY, index);
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            return (0, false);
                        }
                        break;
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter);
            return (index, true);
        }

        public void PrintAbilitiesOptions(int offsetY, int index)
        {
            Utils.SetCursorInteract(offsetY);
            Utils.WriteColour("Abilities", ConsoleColor.DarkYellow);
            int offset = 0;
            for (int i = 0; i < Instance.Class.Abilities.Count; i++)
            {
                offset = offsetY + i + 1;
                Utils.SetCursorInteract(offset);
                Ability ability = Instance.Class.Abilities[i];
                if (ability == Instance.Class.Abilities[index])
                {
                    Utils.WriteColour(">>", ConsoleColor.Yellow);
                    Utils.WriteColour($"{ability.Name}", ConsoleColor.Cyan);
                    Utils.SetCursorInteract(offsetY + 1, 20);
                    Console.Write(ability.Description);
                    Utils.SetCursorInteract(offsetY + 2, 20);
                    Console.Write($"Cooldown: {ability.Cooldown}");
                    Utils.SetCursorInteract(offsetY + 3, 20);
                    Console.Write($"Resource Cost: {ability.ResourceCost}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Utils.SetCursorInteract(offset);
                    Console.Write(ability.Name);
                    Console.ResetColor();
                }
            }
            Utils.SetCursorInteract(offset + 1);
            Console.Write("[");
            Utils.WriteColour("Backspace", ConsoleColor.DarkGray);
            Console.Write("] Return");
        }
    }
}
