using AuldShiteburn.BackendData;
using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
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

        public override bool ReceiveDamage(DamagePayload incomingDamage, int offsetY = 0)
        {
            int initialPhys = incomingDamage.physicalDamage;
            int initialProp = incomingDamage.propertyDamage;
            int totalDamage = 0;
            Utils.SetCursorInteract();
            if (Instance.EquippedArmour != null)
            {
                ArmourItem playerArmour = Instance.EquippedArmour;
                bool physRes = playerArmour.PrimaryPhysicalResistance == incomingDamage.physicalDamageType;
                bool propRes = playerArmour.PrimaryPropertyResistance == incomingDamage.propertyDamageType;

                Console.Write($"{Instance.Name} takes ");
                int physDamage = incomingDamage.physicalDamage -= playerArmour.PhysicalMitigation;
                if (physRes)
                {
                    physDamage -= Combat.ARMOUR_RESISTANCE_MITIGATION_MODIFIER;
                }
                if (physDamage < 0) physDamage = 0;
                Utils.WriteColour($"{physDamage}/{initialPhys} ", ConsoleColor.Red);
                int propDamage = incomingDamage.propertyDamage -= playerArmour.PropertyMitigation;
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
                totalDamage += incomingDamage.physicalDamage;
                Utils.WriteColour($"{incomingDamage.physicalDamage} ", ConsoleColor.Red);
                Console.Write($"physical damage and ");
                totalDamage += incomingDamage.propertyDamage;
                Utils.WriteColour($"{incomingDamage.propertyDamage} ", ConsoleColor.Red);
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
            int offset = 0;
            for (int x = 0; x < Inventory.Column; x++)
            {
                if (x == 0)
                {
                    offset = Inventory.WEAPON_OFFSET;
                    Utils.SetCursorInventory(offsetX: Inventory.WEAPON_OFFSET);
                    Console.Write("[");
                    Utils.WriteColour("WEAPONS", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 1)
                {
                    offset = Inventory.ARMOUR_OFFSET;
                    Utils.SetCursorInventory(offsetX: Inventory.ARMOUR_OFFSET);
                    Console.Write("[");
                    Utils.WriteColour("ARMOUR", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 2)
                {
                    offset = Inventory.CONSUMABLE_OFFSET;
                    Utils.SetCursorInventory(offsetX: Inventory.CONSUMABLE_OFFSET);
                    Console.Write("[");
                    Utils.WriteColour("CONSUMABLES", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 3)
                {
                    offset = Inventory.KEY_OFFSET;
                    Utils.SetCursorInventory(offsetX: Inventory.KEY_OFFSET);
                    Console.Write("[");
                    Utils.WriteColour("KEY ITEMS", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                for (int y = 1; y <= Inventory.Row; y++)
                {
                    Utils.SetCursorInventory(y, offset);
                    {
                        if (Inventory.ItemList[y - 1, x] != null)
                        {
                            Utils.WriteColour($"{Inventory.ItemList[y - 1, x].Name}", ConsoleColor.DarkGray);
                        }
                        else
                        {
                            Utils.WriteColour("--", ConsoleColor.DarkGray);
                        }
                    }                    
                }
            }
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

        public int CycleAbilities(int offsetY, int index)
        {
            PrintAbilities(offsetY, index);
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
                                PrintAbilities(offsetY, index);
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (index >= 0 && index < Instance.Class.Abilities.Count - 1)
                            {
                                index++;
                                Utils.ClearAreaInteract(offsetY, Instance.Class.Abilities.Count + 4);
                                PrintAbilities(offsetY, index);
                            }
                        }
                        break;
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter);
            return index;
        }

        public void PrintAbilities(int offsetY, int index)
        {
            Utils.SetCursorInteract(offsetY);
            Utils.WriteColour("Abilities", ConsoleColor.DarkYellow);
            for (int i = 0; i < Instance.Class.Abilities.Count; i++)
            {
                int offset = offsetY + i + 1;
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
        }
    }
}
