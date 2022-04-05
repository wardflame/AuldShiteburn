using AuldShiteburn.BackendData;
using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData;
using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.EntityData.PlayerData.Classes;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
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
        public Inventory Inventory { get; set; }
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
                            Inventory.PlayerItemInteract(true);
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
            Instance.MaxHP = Instance.Class.Statistics.HP;
            Instance.HP = Instance.Class.Statistics.HP;
            Instance.UsesStamina = Instance.Class.Statistics.UsesStamina;
            Instance.UsesMana = Instance.Class.Statistics.UsesMana;
            Instance.MaxStamina = Instance.Class.Statistics.Stamina;
            Instance.Stamina = Instance.Class.Statistics.Stamina;
            Instance.MaxMana = Instance.Class.Statistics.Mana;
            Instance.Mana = Instance.Class.Statistics.Mana;
            #endregion Class Generation

            #region Name Generation
            string titleStr;
            string forenameStr;
            int sexChance = rand.Next(1, 101);
            if (sexChance <= GameSettings.Instance.SexRatio)
            {
                forenameStr = PlayerGenerationData.NamesMale[rand.Next(PlayerGenerationData.NamesMale.Count)];
                int titleIndex = rand.Next(0, Instance.Class.Titles.TitleMale.Count);
                titleStr = Instance.Class.Titles.TitleMale[titleIndex];
                Instance.Name = $"{titleStr} {forenameStr}";

            }
            else
            {
                forenameStr = PlayerGenerationData.NameFemale[rand.Next(PlayerGenerationData.NameFemale.Count)];
                int titleIndex = rand.Next(0, Instance.Class.Titles.TitleFemale.Count);
                titleStr = Instance.Class.Titles.TitleFemale[titleIndex];
                Instance.Name = $"{titleStr} {forenameStr}";
            }
            #endregion Name Generation

            #region Loot Assignment
            Instance.Inventory = new Inventory("Player Inventory", 6, 4);
            Instance.EquippedWeapon = WeaponItem.GenerateSpawnWeapon(Instance.Class.ClassType);
            Instance.EquippedArmour = ArmourItem.GenerateSpawnArmour(Instance.Class.ClassType);
            #endregion Loot Assignment

            return Instance;
        }

        /// <summary>
        /// Take an incoming combat payload and sort through the damage.
        /// </summary>
        /// <param name="combatPayload"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public override bool ReceiveAttack(CombatPayload combatPayload, int offsetY = 0)
        {
            int initialPhys = combatPayload.PhysicalDamage;
            int initialProp = combatPayload.PropertyDamage;
            int totalDamage = 0;
            Utils.SetCursorInteract();
            if (Instance.EquippedArmour != null)
            {
                ArmourItem playerArmour = Instance.EquippedArmour;
                bool physRes = playerArmour.PrimaryPhysicalResistance == combatPayload.PhysicalAttackType;
                bool propRes = playerArmour.PrimaryPropertyResistance == combatPayload.PropertyAttackType;

                Console.Write($"{Instance.Name} takes ");
                int physDamage = combatPayload.PhysicalDamage -= playerArmour.PhysicalMitigation;
                if (physRes)
                {
                    physDamage -= Combat.ARMOUR_RESISTANCE_MITIGATION_MODIFIER;
                }
                if (physDamage < 0)
                {
                    physDamage = 0;
                }
                Utils.WriteColour($"{physDamage}/{initialPhys} ", ConsoleColor.Red);
                int propDamage = combatPayload.PropertyDamage -= playerArmour.PropertyMitigation;
                if (propRes)
                {
                    propDamage -= Combat.ARMOUR_RESISTANCE_MITIGATION_MODIFIER;
                }
                if (propDamage < 0)
                {
                    propDamage = 0;
                }
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
                totalDamage += combatPayload.PhysicalDamage;
                Utils.WriteColour($"{combatPayload.PhysicalDamage} ", ConsoleColor.Red);
                Console.Write($"physical damage and ");
                totalDamage += combatPayload.PropertyDamage;
                Utils.WriteColour($"{combatPayload.PropertyDamage} ", ConsoleColor.Red);
                Console.Write($"for a total of ");
                Utils.WriteColour($"{totalDamage} ", ConsoleColor.Red);
                Console.Write($"damage.");
            }
            if (combatPayload.IsStun)
            {
                Utils.SetCursorInteract(Console.CursorTop - 1);
                if (!Stunned)
                {
                    Stunned = true;
                    StunTimer = combatPayload.StunCount;
                    Utils.WriteColour($"You are stunned for {StunTimer} turns!", ConsoleColor.DarkBlue);
                }
                else
                {
                    Utils.WriteColour($"You are already stunned and cannot be again for {StunTimer} turns!", ConsoleColor.DarkYellow);
                }
            }
            Instance.HP -= totalDamage;
            Instance.PrintStats();
            return false;
        }

        /// <summary>
        /// Print the player's inventory underneath player stats.
        /// </summary>
        public void PrintInventory()
        {
            Utils.ClearPlayerInventoryInterface();
            Utils.SetCursorInventory();
            Inventory.PrintInventory(true);
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
        /// Print the player's weapon with highlights to show proficiency.
        /// </summary>
        public void PrintWeapon()
        {
            Console.ResetColor();
            if (Instance.EquippedWeapon != null)
            {
                if (Instance.EquippedWeapon.Property.Type != PropertyDamageType.Standard)
                {
                    if (Instance.EquippedWeapon.Property.HasAffinity)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write($"{Instance.EquippedWeapon.Property.Name} ");
                    Console.ResetColor();
                }

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
        /// Print the player's armour with highlights to show proficiency.
        /// </summary>
        public void PrintArmour()
        {
            if (Instance.EquippedArmour != null)
            {
                if (Instance.Class.Proficiencies.ArmourProficiency == Instance.EquippedArmour.ArmourFamily)
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

        /// <summary>
        /// Print the player's abilities and allow them to navigate an index.
        /// When pressing enter, return the index to be looked for in their ability
        /// list. If pressing backspace, return index -1 to consider choice null.
        /// </summary>
        /// <param name="offsetY">How far down the interact screen we are when needing to clear if the player cancels their decision.</param>
        /// <param name="index">Index to start at in ability list.</param>
        /// <returns>Returns index of an ability or -1 if cancelling choice.</returns>
        public int CycleAbilities(int offsetY, int index)
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
                                Utils.ClearInteractArea(offsetY, Instance.Class.Abilities.Count + 4);
                                PrintAbilitiesOptions(offsetY, index);
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (index >= 0 && index < Instance.Class.Abilities.Count - 1)
                            {
                                index++;
                                Utils.ClearInteractArea(offsetY, Instance.Class.Abilities.Count + 4);
                                PrintAbilitiesOptions(offsetY, index);
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            return -1;
                        }
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter);
            return index;
        }

        /// <summary>
        /// Iterate through and print the player's abilities, highlighting the one at the
        /// given index and printing its details beside it.
        /// </summary>
        /// <param name="offsetY">The offset to place the cursor down the interact area.</param>
        /// <param name="index">Index of the ability we want to highlight.</param>
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
