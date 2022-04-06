using AuldShiteburn.BackendData;
using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData;
using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
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
        public StatusEffect AbilityStatusEffect { get; set; }
        public StatusEffect PotionStatusEffect { get; set; }
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

            // If the player has an active status effect, run its effect.
            #region Status Effect Application
            if (Instance.AbilityStatusEffect != null && Instance.AbilityStatusEffect.GetType() == typeof(DefenseStatusEffect))
            {
                combatPayload = Instance.AbilityStatusEffect.EffectActive(combatPayload);
            }
            if (Instance.PotionStatusEffect != null && Instance.PotionStatusEffect.GetType() == typeof(DefenseStatusEffect))
            {
                combatPayload = Instance.PotionStatusEffect.EffectActive(combatPayload);
            }
            #endregion Status Effect Application

            // If player has armour equipped, mitigate where possible, else apply the damage directly.
            #region Damage Application
            Utils.SetCursorInteract(1);
            if (Instance.EquippedArmour != null)
            {
                ArmourItem playerArmour = Instance.EquippedArmour;
                bool physRes = playerArmour.PrimaryPhysicalResistance == combatPayload.PhysicalAttackType;
                bool propRes = playerArmour.PrimaryPropertyResistance == combatPayload.PropertyAttackType;

                #region Physical Damage Mitigation
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
                Console.Write($"{combatPayload.PhysicalAttackType} damage ");
                #endregion Physical Damage Mitigation

                #region Property Damage Mitigation
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
                Console.Write($"{combatPayload.PropertyAttackType} damage ");
                #endregion Property Damage Mitigation

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
            #endregion Damage Application

            // If the payload has a stun in it, apply the stun.
            #region Stun Application
            if (combatPayload.IsStun)
            {
                Utils.SetCursorInteract(Console.CursorTop - 1);
                if (!Stunned)
                {
                    StunTimer = combatPayload.StunCount;
                    Utils.WriteColour($"You are stunned for {StunTimer} turns!", ConsoleColor.DarkBlue);
                }
                else
                {
                    Utils.WriteColour($"You are already stunned and cannot be again for {StunTimer} turns!", ConsoleColor.DarkYellow);
                }
            }
            #endregion Stun Application

            Instance.HP -= totalDamage;
            Instance.PrintStats();
            return false;
        }

        /// <summary>
        /// Compare current resource to the cost of a task. Return true if resource available, else false.
        /// </summary>
        /// <param name="resourceCost">Task resource cost.</param>
        /// <returns>Whether player has the resource to perform the task.</returns>
        public bool CheckResourceLevel(int resourceCost)
        {
            if ((UsesMana && (Mana < resourceCost)) || (UsesStamina && (Stamina < resourceCost)))
            {
                return false;
            }
            else
            {
                return true;
            }
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
            int cursorTop = Console.CursorTop;
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
            Console.Write("Ability Status Effect: ");
            if (Instance.AbilityStatusEffect != null)
            {
                Utils.WriteColour($"{Instance.AbilityStatusEffect.Name}: ", Instance.AbilityStatusEffect.DisplayColor);
                Console.Write($"{Instance.AbilityStatusEffect.Duration}");
            }
            else
            {
                Console.Write("--");
            }
            Utils.SetCursorPlayerStat(5);
            Console.Write("Potion Status Effect: ");
            if (Instance.PotionStatusEffect != null)
            {
                Utils.WriteColour($"{Instance.PotionStatusEffect.Name}: ", Instance.PotionStatusEffect.DisplayColor);
                Console.Write($"{Instance.PotionStatusEffect.Duration}");
            }
            else
            {
                Console.Write("--");
            }
            Utils.SetCursorPlayerStat(6);
            Console.Write("Stun Timer: ");
            if (Instance.StunTimer > 0)
            {
                Utils.WriteColour($"{Instance.StunTimer}", ConsoleColor.Magenta);
            }
            else
            {
                Console.Write("--");
            }
            Utils.SetCursorPlayerStat(7);
            Console.Write("- - - - - - - -");

            Utils.SetCursorPlayerStat(8);
            Utils.WriteColour("Equipped Weapon", ConsoleColor.DarkYellow);
            Utils.SetCursorPlayerStat(9);
            Console.Write(">> ");
            PrintWeapon();
            Utils.SetCursorPlayerStat(11);
            Utils.WriteColour("Equipped Armour", ConsoleColor.DarkYellow);
            Utils.SetCursorPlayerStat(12);
            Console.Write(">> ");
            PrintArmour();

            Console.CursorTop = cursorTop;
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
    }
}
