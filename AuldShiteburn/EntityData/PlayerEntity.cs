using AuldShiteburn.BackendData;
using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData.PlayerData;
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
            Instance.Class = CharacterClass.Classes[rand.Next(CharacterClass.Classes.Count)];
            Instance.maxHP = Instance.Class.Statistics.hp;
            Instance.HP = Instance.Class.Statistics.hp;
            Instance.UsesStamina = Instance.Class.Statistics.usesStamina;
            Instance.UsesMana = Instance.Class.Statistics.usesMana;
            Instance.maxStamina = Instance.Class.Statistics.stamina;
            Instance.Stamina = Instance.Class.Statistics.stamina;
            Instance.maxMana = Instance.Class.Statistics.mana;
            Instance.Mana = Instance.Class.Statistics.mana;
            #endregion Class Generation

            #region Name Generation
            string titleStr;
            string forenameStr;
            int sexChance = rand.Next(1, 101);
            if (sexChance <= GameSettings.Instance.SexRatio)
            {
                forenameStr = PlayerGenerationData.NamesMale[rand.Next(PlayerGenerationData.NamesMale.Count)];
                int titleIndex = rand.Next(0, Instance.Class.TitlesMale.Count);
                titleStr = Instance.Class.TitlesMale[titleIndex];
                Instance.Name = $"{titleStr} {forenameStr}";

            }
            else
            {
                forenameStr = PlayerGenerationData.NameFemale[rand.Next(PlayerGenerationData.NameFemale.Count)];
                int titleIndex = rand.Next(0, Instance.Class.TitlesFemale.Count);
                titleStr = Instance.Class.TitlesFemale[titleIndex];
                Instance.Name = $"{titleStr} {forenameStr}";
            }
            #endregion Name Generation

            #region Loot Assignment
            Instance.EquippedWeapon = WeaponItem.GenerateSpawnWeapon(Instance.Class.ClassType);
            //Instance.Inventory.ItemList[0, 1] = new ArmourItem("Plate");
            #endregion Loot Assignment

            return Instance;
        }

        public override void ReceiveDamage(Damage incomingDamage)
        {
            int initialPhys = incomingDamage.physicalDamage;
            int initialProp = incomingDamage.propertyDamage;
            int totalDamage = 0;
            Utils.SetCursorInteract();
            if (Instance.EquippedArmour != null)
            {
                ArmourItem playerArmour = Instance.EquippedArmour;
                bool physRes = playerArmour.PrimaryPhysicalResistance == incomingDamage.physDamageType;
                bool propRes = playerArmour.PrimaryPropertyResistance == incomingDamage.propertyDamageType;

                Console.Write($"{Instance.Name} takes ");
                int physDamage = incomingDamage.physicalDamage -= playerArmour.PhysicalMitigation;
                if (physRes)
                {
                    physDamage -= Combat.ARMOUR_RESISTANCE_MITIGATION;
                }
                if (physDamage < 0) physDamage = 0;
                Utils.WriteColour($"{physDamage}/{initialPhys} ", ConsoleColor.Red);
                int propDamage = incomingDamage.propertyDamage -= playerArmour.PropertyMitigation;
                if (propRes)
                {
                    propDamage -= Combat.ARMOUR_RESISTANCE_MITIGATION;
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
                // WE GOT A LOT TO DO!
            }
            Instance.PrintStats();
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
                    Utils.SetCursorInventory(Inventory.WEAPON_OFFSET);
                    Console.Write("[");
                    Utils.WriteColour("WEAPONS", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 1)
                {
                    offset = Inventory.ARMOUR_OFFSET;
                    Utils.SetCursorInventory(Inventory.ARMOUR_OFFSET);
                    Console.Write("[");
                    Utils.WriteColour("ARMOUR", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 2)
                {
                    offset = Inventory.CONSUMABLE_OFFSET;
                    Utils.SetCursorInventory(Inventory.CONSUMABLE_OFFSET);
                    Console.Write("[");
                    Utils.WriteColour("CONSUMABLES", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 3)
                {
                    offset = Inventory.KEY_OFFSET;
                    Utils.SetCursorInventory(Inventory.KEY_OFFSET);
                    Console.Write("[");
                    Utils.WriteColour("KEY ITEMS", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                for (int y = 1; y <= Inventory.Row; y++)
                {
                    Utils.SetCursorInventory(offset, y);
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

            Utils.SetCursorPlayerStat(offsetY: 1);
            Console.Write($"Health: ");
            Utils.WriteColour($"{Instance.HP}", ConsoleColor.Red);

            if (Instance.UsesStamina)
            {
                Utils.SetCursorPlayerStat(offsetY: 2);
                Console.Write($"Stamina: ");
                Utils.WriteColour($"{Instance.Stamina}", ConsoleColor.Green);
            }
            if (Instance.UsesMana)
            {
                Utils.SetCursorPlayerStat(offsetY: 2);
                Console.Write($"Mana: ");
                Utils.WriteColour($"{Instance.Mana}", ConsoleColor.Blue);
            }

            Utils.SetCursorPlayerStat(offsetY: 3);
            Console.Write("Equipped Weapon: ");
            if (Instance.EquippedWeapon != null)
            {
                if (Instance.EquippedWeapon.Property.HasAffinity)
                {
                    Utils.WriteColour($"{Instance.EquippedWeapon.Property.Name} ", ConsoleColor.DarkGreen);
                }
                else
                {
                    Console.Write($"{Instance.EquippedWeapon.Property.Name} ");
                }
                if (Instance.EquippedWeapon.Material.HasAffinity)
                {
                    Utils.WriteColour($"{Instance.EquippedWeapon.Material.Name} ", ConsoleColor.DarkGreen);
                }
                else
                {
                    Console.Write($"{Instance.EquippedWeapon.Material.Name} ");
                }
                if (Instance.EquippedWeapon.Type.IsProficient)
                {
                    Utils.WriteColour($"{Instance.EquippedWeapon.Type.Name} ", ConsoleColor.DarkGreen);
                }
                else
                {
                    Console.Write($"{Instance.EquippedWeapon.Type.Name} ");
                }
            }
            else
            {
                Console.Write("--");
            }
            Utils.SetCursorPlayerStat(offsetY: 4);
            Console.Write("Equipped Armour: ");
            if (Instance.EquippedArmour != null)
            {
                Console.Write(Instance.EquippedArmour.Name);
            }
            else
            {
                Console.Write("--");
            }
        }
    }
}
