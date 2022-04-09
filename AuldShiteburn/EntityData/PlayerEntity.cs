using AuldShiteburn.BackendData;
using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData;
using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
using System;

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
        public bool CarryingDullAmulet
        {
            get
            {
                for (int i = 0; i < Instance.Inventory.Row; i++)
                {
                    if (Instance.Inventory.ItemList[i, 3] != null)
                    {
                        if (Instance.Inventory.ItemList[i, 3].Name == KeyItem.ShitestainedAmulet.Name) return true;
                    }
                }
                return false;
            }
        }
        public bool CarryingMoonlitAmulet
        {
            get
            {
                for (int i = 0; i < Instance.Inventory.Row; i++)
                {
                    if (Instance.Inventory.ItemList[i, 3] != null)
                    {
                        if (Instance.Inventory.ItemList[i, 3].Name == KeyItem.MoonlitAmulet.Name) return true;
                    }
                }
                return false;
            }
        }
        public bool CarryingOrmodsAmulet
        {
            get
            {
                for (int i = 0; i < Instance.Inventory.Row; i++)
                {
                    if (Instance.Inventory.ItemList[i, 3] != null)
                    {
                        if (Instance.Inventory.ItemList[i, 3].Name == KeyItem.OrmodsAmulet.Name) return true;
                    }
                }
                return false;
            }
        }
        public bool TookFromOrmod { get; set; }

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
                            Instance.PrintInventory();
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
            Instance.Class = CharacterClass.Classes[rand.Next(CharacterClass.Classes.Count)];
            //Instance.Class = new MarauderClass();
            Instance.MaxHP = Instance.Class.Statistics.HP;
            Instance.HP = Instance.Class.Statistics.HP;
            Instance.UsesStamina = Instance.Class.Statistics.UsesStamina;
            Instance.UsesMana = Instance.Class.Statistics.UsesMana;
            Instance.MaxStamina = Instance.Class.Statistics.Stamina;
            Instance.Stamina = Instance.Class.Statistics.Stamina;
            Instance.MaxMana = Instance.Class.Statistics.Mana;
            Instance.Mana = Instance.Class.Statistics.Mana;
            Instance.StunCap = Instance.Class.Statistics.StunCap;
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
            Instance.Inventory.ItemList[0, 0] = new WeaponItem()
            {
                Type = WeaponType.Greathammer,
                Material = WeaponMaterial.WeaponMaterialMoonstone,
                Property = WeaponProperty.WeaponPropertyShiteSlick
            };
            Instance.Inventory.ItemList[1, 0] = WeaponItem.GenerateSpawnWeapon(Instance.Class.ClassType);
            Instance.Inventory.ItemList[2, 0] = WeaponItem.GenerateSpawnWeapon(Instance.Class.ClassType);
            Instance.Inventory.ItemList[3, 0] = WeaponItem.GenerateSpawnWeapon(Instance.Class.ClassType);
            #endregion Loot Assignment

            Instance.Inventory.ItemList[0, 3] = KeyItem.ShitestainedAmulet;

            return Instance;
        }

        /// <summary>
        /// Take an incoming combat payload and sort through the damage.
        /// </summary>
        /// <param name="combatPayload"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public override bool ReceiveAttack(CombatPayload combatPayload, int offsetY = 0, LivingEntity aggressor = null)
        {
            EnemyEntity enemy = new EnemyEntity();
            if (aggressor != null)
            {
                enemy = (EnemyEntity)aggressor;
            }

            int initialPhys = combatPayload.PhysicalDamage;
            int initialProp = combatPayload.PropertyDamage;
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

            Utils.SetCursorInteract(1);
            Utils.WriteColour($"You ");

            //If the payload has a stun in it, apply the stun.
            #region Stun Application
            if (combatPayload.IsStun)
            {
                if (!Stunned)
                {
                    StunTimer = combatPayload.StunCount;
                    Utils.WriteColour($"are stunned for ");
                    Utils.WriteColour($"{StunTimer} ", ConsoleColor.Blue);
                    Utils.WriteColour($"turns,");
                    JustStunned = true;
                }
                else
                {
                    Utils.WriteColour($"are already stunned and cannot be again for ", ConsoleColor.DarkYellow);
                    Utils.WriteColour($"{StunTimer} ", ConsoleColor.Blue);
                    Utils.WriteColour($"turns, ", ConsoleColor.DarkYellow);
                }
                Utils.SetCursorInteract(Console.CursorTop - 1);
            }
            #endregion Stun Application

            // If player has armour equipped, mitigate where possible, else apply the damage directly.
            #region Damage Application
            int totalDamage;
            if (Instance.EquippedArmour != null)
            {
                ArmourItem playerArmour = Instance.EquippedArmour;
                #region Physical Damage Mitigation
                if (combatPayload.HasPhysical)
                {

                    combatPayload.PhysicalDamage -= playerArmour.PhysicalMitigation;
                    if (combatPayload.PhysicalDamage < 0)
                    {
                        combatPayload.PhysicalDamage = 0;
                    }
                    Utils.WriteColour($"take ");
                    Utils.WriteColour($"{combatPayload.PhysicalDamage}/{initialPhys} ", ConsoleColor.Red);
                    Utils.WriteColour($"{combatPayload.PhysicalAttackType} damage, ");
                    Utils.SetCursorInteract(Console.CursorTop - 1);

                }
                #endregion Physical Damage Mitigation
                #region Property Damage Mitigation
                if (combatPayload.HasProperty)
                {

                    combatPayload.PropertyDamage -= playerArmour.PropertyMitigation;
                    if (combatPayload.PropertyDamage < 0)
                    {
                        combatPayload.PropertyDamage = 0;
                    }
                    if (combatPayload.HasPhysical) Utils.WriteColour("and ");
                    else Utils.WriteColour("takes ");
                    Utils.WriteColour($"{combatPayload.PropertyDamage}/{initialProp} ", ConsoleColor.Red);
                    Utils.WriteColour($"{combatPayload.PropertyAttackType} damage ");
                    Utils.SetCursorInteract(Console.CursorTop - 1);
                }
                #endregion Property Damage Mitigation

            }
            else
            {
                #region Physical Damage Mitigation
                if (combatPayload.HasPhysical)
                {
                    Utils.WriteColour("takes ");
                    Utils.WriteColour($"{combatPayload.PhysicalDamage} ", ConsoleColor.Red);
                    Utils.WriteColour($"physical damage,");
                    Utils.SetCursorInteract(Console.CursorTop - 1);
                }
                #endregion Physical Damage Mitigation
                #region Property Damage Mitigation
                if (combatPayload.HasProperty)
                {
                    if (combatPayload.HasPhysical) Utils.WriteColour("and ");
                    else Utils.WriteColour("takes ");
                    Utils.WriteColour($"{combatPayload.PropertyDamage} ", ConsoleColor.Red);
                    Utils.SetCursorInteract(Console.CursorTop - 1);
                }
                #endregion Property Damage Mitigation
            }
            #region Total Damage Calculation
            totalDamage = combatPayload.PhysicalDamage + combatPayload.PropertyDamage;
            Utils.WriteColour($"for a total of ");
            Utils.WriteColour($"{totalDamage} ", ConsoleColor.Red);
            Utils.WriteColour($"damage.");
            Utils.SetCursorInteract(Console.CursorTop - 1);
            #endregion Total Damage Calculation
            #endregion Damage Application

            // If player has riposte status effect, apply debuff to enemy.
            #region Riposte Status
            if (totalDamage <= 0 && AbilityStatusEffect != null)
            {
                if (AbilityStatusEffect.Name == "Defensive Stance")
                {
                    enemy.StatusEffect = DefenseStatusEffect.Staggered;
                    Utils.SetCursorInteract(Console.CursorTop - 1);
                    Utils.WriteColour("You riposte the enemy, inflicting ", ConsoleColor.DarkYellow);
                    Utils.WriteColour($"{enemy.StatusEffect.Name}", enemy.StatusEffect.DisplayColor);
                    Utils.WriteColour($"!", ConsoleColor.DarkYellow);
                }
            }
            #endregion Riposte Status

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
            Utils.WriteColour($"{Instance.Name} the {Instance.Class.Name}");

            Utils.SetCursorPlayerStat(1);
            Utils.WriteColour("- - - - - - - -");
            Utils.SetCursorPlayerStat(2);
            Utils.WriteColour($"Health: ");
            Utils.WriteColour($"{Instance.HP}", ConsoleColor.Red);

            if (Instance.UsesStamina)
            {
                Utils.SetCursorPlayerStat(3);
                Utils.WriteColour($"Stamina: ");
                Utils.WriteColour($"{Instance.Stamina}", ConsoleColor.Green);
            }
            if (Instance.UsesMana)
            {
                Utils.SetCursorPlayerStat(3);
                Utils.WriteColour($"Mana: ");
                Utils.WriteColour($"{Instance.Mana}", ConsoleColor.Blue);
            }
            Utils.SetCursorPlayerStat(4);
            Utils.WriteColour("Ability Effect: ");
            if (Instance.AbilityStatusEffect != null)
            {
                Utils.WriteColour($"{Instance.AbilityStatusEffect.Name}: ", Instance.AbilityStatusEffect.DisplayColor);
                Utils.WriteColour($"{Instance.AbilityStatusEffect.Duration}");
            }
            else
            {
                Utils.WriteColour("--");
            }
            Utils.SetCursorPlayerStat(5);
            Utils.WriteColour("Potion Effect: ");
            if (Instance.PotionStatusEffect != null)
            {
                Utils.WriteColour($"{Instance.PotionStatusEffect.Name}: ", Instance.PotionStatusEffect.DisplayColor);
                Utils.WriteColour($"{Instance.PotionStatusEffect.Duration}");
            }
            else
            {
                Utils.WriteColour("--");
            }
            Utils.SetCursorPlayerStat(6);
            Utils.WriteColour("Stun Timer: ");
            if (Instance.StunTimer > 0)
            {
                Utils.WriteColour($"{Instance.StunTimer}", ConsoleColor.Magenta);
            }
            else
            {
                Utils.WriteColour("--");
            }
            Utils.SetCursorPlayerStat(7);
            Utils.WriteColour("- - - - - - - -");

            Utils.SetCursorPlayerStat(8);
            Utils.WriteColour("Equipped Weapon", ConsoleColor.DarkYellow);
            Utils.SetCursorPlayerStat(9);
            Utils.WriteColour(">> ", ConsoleColor.Yellow);
            Inventory.PrintWeaponWithAffinity(EquippedWeapon);
            Utils.SetCursorPlayerStat(11);
            Utils.WriteColour("Equipped Armour", ConsoleColor.DarkYellow);
            Utils.SetCursorPlayerStat(12);
            Utils.WriteColour(">> ", ConsoleColor.Yellow);
            Inventory.PrintArmourWithAffinity(EquippedArmour);

            Console.CursorTop = cursorTop;
        }
    }
}
