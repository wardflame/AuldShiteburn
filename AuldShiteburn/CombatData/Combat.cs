using AuldShiteburn.ArtData;
using AuldShiteburn.CombatData.AbilityData;
using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.CombatData.StatusEffectData.StatusEffects;
using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.CombatData
{
    internal class Combat
    {
        #region Modifier Constants
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
        #endregion Modifier Constants
        private static int RoundNumber { get; set; } = 0;

        /// <summary>
        /// Keep player and enemies fighting in a loop until one either they are all dead
        /// or the player has died.
        /// </summary>
        /// <param name="enemies">Enemies for the player to fight.</param>
        /// <returns>True if the player wins, false if the player dies.</returns>
        public static bool CombatEncounter(List<EnemyEntity> enemies)
        {
            RoundNumber = 1;
            while (enemies.Count > 0 && PlayerEntity.Instance.HP > 0)
            {
                PlayerCombatTurn(enemies);
                EnemyCombatTurn(enemies);
                RoundNumber++;
            }
            if (PlayerEntity.Instance.HP > 0)
            {
                Utils.SetCursorInteract();
                ASCIIArt.PrintASCII(ASCIIArt.VICTORY_MESSAGE, ConsoleColor.Green);
                Console.CursorTop += 1;
                Console.Write("Press any key to continue...");
                Console.ReadKey();
                Utils.ClearInteractInterface();
                PlayerEntity.Instance.HP = PlayerEntity.Instance.MaxHP;
                if (PlayerEntity.Instance.UsesMana)
                {
                    PlayerEntity.Instance.Mana = PlayerEntity.Instance.MaxMana;
                }
                else if (PlayerEntity.Instance.UsesStamina)
                {
                    PlayerEntity.Instance.Stamina = PlayerEntity.Instance.MaxStamina;
                }
                PlayerEntity.Instance.PrintStats();
                return true;
            }
            else
            {
                Utils.SetCursorInteract();
                ASCIIArt.PrintASCII(ASCIIArt.DEATH_MESSAGE, ConsoleColor.Red);
                Console.CursorTop += 1;
                Console.Write("Press any key to continue...");
                Console.ReadKey();
                return false;
            }
        }

        /// <summary>
        /// Present the player with combat targets and have them iterate through
        /// the list and choose a target. Then, present them with their combat options
        /// and deliver a combat payload to the chosen enemy.
        /// </summary>
        /// <param name="enemies">List of enemies in the area.</param>
        private static void PlayerCombatTurn(List<EnemyEntity> enemies)
        {
            bool playerTurn = true;
            bool stunReduction = false;
            bool statusReduction = false;
            bool abilityCooldowns = false;
            int endOffset = 0;
            while (playerTurn)
            {
                if (RoundNumber > 1)
                {
                    #region Ability Cooldowns
                    if (!abilityCooldowns)
                    {
                        // Iterate through player abilities. If they have a cooldown active, decrement it.
                        int i = 1;
                        int abilitiesCoolingDown = 0;
                        Utils.SetCursorInteract();
                        Utils.WriteColour("Cooldowns", ConsoleColor.DarkYellow);
                        foreach (Ability ability in PlayerEntity.Instance.Class.Abilities)
                        {
                            if (ability.ActiveCooldown > 0)
                            {
                                ability.ActiveCooldown--;
                                Utils.SetCursorInteract(i++);
                                Utils.WriteColour($"{ability.Name} cooling down: {ability.ActiveCooldown}/{ability.Cooldown}", ConsoleColor.Magenta);
                                abilitiesCoolingDown++;
                            }
                        }
                        if (abilitiesCoolingDown > 0)
                        {
                            Console.ReadKey();
                            Utils.ClearInteractInterface();
                        }
                        abilityCooldowns = true;
                    }
                    #endregion Ability Cooldowns
                    #region Status Effects Duration
                    if (PlayerEntity.Instance.AbilityStatusEffect != null && !statusReduction)
                    {
                        PlayerEntity.Instance.AbilityStatusEffect.Duration--;
                        if (PlayerEntity.Instance.AbilityStatusEffect.Duration == 0)
                        {
                            PlayerEntity.Instance.AbilityStatusEffect = null;
                        }
                    }
                    if (PlayerEntity.Instance.PotionStatusEffect != null && !statusReduction)
                    {
                        PlayerEntity.Instance.PotionStatusEffect.Duration--;
                        if (PlayerEntity.Instance.PotionStatusEffect.Duration == 0)
                        {
                            PlayerEntity.Instance.PotionStatusEffect = null;
                        }
                    }
                    #endregion Status Effects Duration
                    #region Stun Duration
                    if (PlayerEntity.Instance.Stunned && !stunReduction)
                    {
                        PlayerEntity.Instance.StunTimer--;
                        stunReduction = true;
                    }
                    #endregion Stun Duration
                }
                PlayerEntity.Instance.PrintStats();
                if (PlayerEntity.Instance.StunTimer <= 0)
                {
                    EnemyEntity enemy = ChooseEnemy(enemies);
                    int activity = ChooseActivity();
                    endOffset = Console.CursorTop + 2;
                    if (activity >= 0)
                    {
                        #region Activity 1: Melee Combat
                        if (activity == 0)
                        {
                            CombatPayload playerMeleePayload = ChooseMeleeAttack();
                            if (playerMeleePayload.IsAttack)
                            {
                                if (enemy.ReceiveAttack(playerMeleePayload, enemies.Count + 12))
                                {
                                    enemies.Remove(enemy);
                                }
                                endOffset = Console.CursorTop + 2;
                                playerTurn = false;
                            }
                            else
                            {
                                endOffset = Console.CursorTop;
                                Utils.SetCursorInteract(enemies.Count + 2);
                                Utils.ClearInteractArea(length: 30);
                            }
                        }
                        #endregion Activity 1: Melee Combat
                        #region Activity 2: Ability Combat
                        else if (activity == 1)
                        {
                            Utils.ClearInteractArea(enemies.Count + 5, 20);
                            Utils.SetCursorInteract(enemies.Count + 4);
                            CombatPayload playerAbilityPayload = ChooseAbility();
                            if (playerAbilityPayload.IsAttack)
                            {
                                if (enemy.ReceiveAttack(playerAbilityPayload, Console.CursorTop - 1))
                                {
                                    enemies.Remove(enemy);
                                }
                                endOffset = Console.CursorTop + 2;
                                PlayerEntity.Instance.PrintStats();
                                playerTurn = false;
                            }
                            else if (playerAbilityPayload.IsUtility)
                            {
                                endOffset = Console.CursorTop - 1;
                                PlayerEntity.Instance.PrintStats();
                                playerTurn = false;
                            }
                            else
                            {
                                endOffset = Console.CursorTop - 1;
                                Utils.SetCursorInteract(enemies.Count + 2);
                                Utils.ClearInteractArea(length: 30);
                            }
                        }
                        #endregion Activity 2: Ability Combat
                    }
                    else
                    {
                        endOffset = Console.CursorTop;
                        Utils.SetCursorInteract(enemies.Count + 2);
                        Utils.ClearInteractArea(length: 30);
                    }
                }                
            }
            if (PlayerEntity.Instance.AbilityStatusEffect != null && PlayerEntity.Instance.AbilityStatusEffect.GetType() == typeof(ReplenishStatusEffect))
            {
                PlayerEntity.Instance.AbilityStatusEffect.EffectActive(new CombatPayload(false));
            }
            if (PlayerEntity.Instance.PotionStatusEffect != null && PlayerEntity.Instance.PotionStatusEffect.GetType() == typeof(ReplenishStatusEffect))
            {
                PlayerEntity.Instance.PotionStatusEffect.EffectActive(new CombatPayload(false));
            }
            // Readkey to ensure player has a chance to read the round's report.
            Console.SetCursorPosition(Utils.UIInteractOffset, endOffset);
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Utils.ClearInteractInterface(30);
        }

        /// <summary>
        /// Present the player with combat targets and have them iterate through
        /// the list and choose a target. Then, present them with their combat options
        /// and deliver a combat payload to the chosen enemy.
        /// </summary>
        /// <param name="enemies">List of enemies in the area.</param>
        private static void EnemyCombatTurn(List<EnemyEntity> enemies)
        {
            int endOffset;
            bool enemyTurn = true;
            while (enemyTurn)
            {
                foreach (EnemyEntity enemy in enemies)
                {
                    if (enemy.Stunned)
                    {
                        Utils.SetCursorInteract();
                        Utils.WriteColour($"{enemy.Name} is stunned, recovering in {enemy.StunTimer} turns.", ConsoleColor.DarkBlue);
                        endOffset = Console.CursorTop + 2;
                        enemy.StunTimer--;
                    }
                    else
                    {
                        CombatPayload enemyAttack = enemy.PerformAttack();
                        endOffset = Console.CursorTop + 3;
                        PlayerEntity.Instance.ReceiveAttack(enemyAttack);
                    }
                    // Readkey to ensure player has a chance to read the round's report.
                    Console.SetCursorPosition(Utils.UIInteractOffset, endOffset);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    Utils.ClearInteractInterface(30);
                }
                enemyTurn = false;
            }
        }

        /// <summary>
        /// Generate a combat payload from the player's equipped weapon,
        /// randomising between the min and max physical and property
        /// damages.
        /// </summary>
        /// <returns>Combat payload for enemy to process.</returns>
        private static CombatPayload CalculateWeaponDamage(bool primaryElseSecondary)
        {
            CombatPayload attackPayload = new CombatPayload(true);
            Random rand = new Random();
            WeaponItem playerWeapon = PlayerEntity.Instance.EquippedWeapon;
            attackPayload.PhysicalDamage = rand.Next(playerWeapon.MinPhysDamage, playerWeapon.MaxPhysDamage);
            if (attackPayload.PhysicalDamage > 0)
            {
                attackPayload.PhysicalAttackType = playerWeapon.Type.PrimaryAttack;
                if (!primaryElseSecondary && playerWeapon.Type.SecondaryAttack != PhysicalDamageType.None)
                {
                    attackPayload.PhysicalAttackType = playerWeapon.Type.SecondaryAttack;
                }
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
        /// Iterate through a list of enemies and highlight one at an int index.
        /// The player moves the index with the up/down arrow keys. Pressing enter
        /// returns the enemy at that index.
        /// </summary>
        /// <param name="enemies">List of enemies to navigate.</param>
        /// <returns>Returns the chosen enemy.</returns>
        private static EnemyEntity ChooseEnemy(List<EnemyEntity> enemies)
        {
            Utils.SetCursorInteract();
            Utils.WriteColour("Choose an enemy to attack.", ConsoleColor.DarkYellow);
            int index = 0;
            do
            {
                Utils.ClearInteractArea(1, enemies.Count);
                for (int i = 0; i < enemies.Count; i++)
                {
                    Utils.SetCursorInteract(i + 1);
                    if (enemies[i] == enemies[index])
                    {
                        Utils.WriteColour(">>", ConsoleColor.Yellow);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.Write($"{enemies[i].Name} ");
                    Console.ResetColor();
                    Utils.WriteColour($"{enemies[i].HP}/{enemies[i].MaxHP} ", ConsoleColor.Red);
                    Console.Write($"HP");
                }
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (index <= enemies.Count && index > 0)
                            {
                                index--;
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (index >= 0 && index < enemies.Count - 1)
                            {
                                index++;
                            }
                        }
                        break;
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter);
            return enemies[index];
        }

        /// <summary>
        /// Iterate through a list of string options and highlight one at an int index.
        /// The player moves the index with the up/down arrow keys. Pressing enter
        /// returns the index for an activity.
        /// </summary>
        /// <param name="offsetY">Offset for SetCursorInteract() first parameter.</param>
        /// <returns>Return the activity number.</returns>
        private static int ChooseActivity()
        {
            List<string> activities = new List<string>() { "Use Equipped Weapon", "Use Ability"};
            int offsetY = Console.CursorTop;
            Utils.SetCursorInteract(offsetY);
            Utils.WriteColour("Choose an activity.", ConsoleColor.DarkYellow);
            int index = 0;
            do
            {
                Utils.ClearInteractArea(offsetY + 1, 2);
                for (int i = 0; i < activities.Count; i++)
                {
                    Utils.SetCursorInteract(offsetY + 1 + i);
                    if (i == index)
                    {
                        Utils.WriteColour(">>", ConsoleColor.Yellow);
                        Utils.WriteColour(activities[i], ConsoleColor.Cyan);
                    }
                    else
                    {
                        Console.Write(activities[i]);
                    }
                }
                Utils.SetCursorInteract(offsetY + activities.Count + 1);
                Console.Write("[");
                Utils.WriteColour("BACKSPACE", ConsoleColor.DarkGray);
                Console.Write("] Return");
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (index > 0 && index < activities.Count)
                            {
                                index--;
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (index >= 0 && index < activities.Count - 1)
                            {
                                index++;
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
        /// Iterate through a list of weapon attack types the weapon can perform and have the player choose one.
        /// If, by chance, a weapon's secondary attack is None, the weapon will default to the primary attack type
        /// in the CalculateWeaponDamage() method.
        /// </summary>
        /// <returns>CombatPayload based on the attack type the player chooses.</returns>
        private static CombatPayload ChooseMeleeAttack()
        {
            WeaponItem weapon = PlayerEntity.Instance.EquippedWeapon;
            List<string> meleeAttacks = new List<string>() { weapon.Type.PrimaryAttack.ToString(), weapon.Type.SecondaryAttack.ToString()};
            int offsetY = Console.CursorTop;
            Utils.ClearInteractArea(offsetY, 2);
            Utils.SetCursorInteract(offsetY);
            Utils.WriteColour("Choose an attack technique.", ConsoleColor.DarkYellow);
            int index = 0;
            do
            {
                Utils.ClearInteractArea(offsetY + 1, 2);
                for (int i = 0; i < meleeAttacks.Count; i++)
                {
                    Utils.SetCursorInteract(offsetY + 1 + i);
                    if (i == index)
                    {
                        Utils.WriteColour(">>", ConsoleColor.Yellow);
                        Utils.WriteColour(meleeAttacks[i], ConsoleColor.Cyan);
                    }
                    else
                    {
                        Console.Write(meleeAttacks[i]);
                    }
                }
                Utils.SetCursorInteract(offsetY + meleeAttacks.Count + 1);
                Console.Write("[");
                Utils.WriteColour("BACKSPACE", ConsoleColor.DarkGray);
                Console.Write("] Return");
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (index > 0 && index < meleeAttacks.Count)
                            {
                                index--;
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (index >= 0 && index < meleeAttacks.Count - 1)
                            {
                                index++;
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            return new CombatPayload(false);
                        }
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter);
            if (index == 0)
            {
                return CalculateWeaponDamage(true);
            }
            else
            {
                return CalculateWeaponDamage(false);
            }
        }

        /// <summary>
        /// Iterate through a list of the player's class abilities. Provide details to the side
        /// of them. On pressing enter, activate the ability and return the combat payload.
        /// </summary>
        /// <returns>Returns the chosen ability's combat payload.</returns>
        private static CombatPayload ChooseAbility()
        {
            List<Ability> abilities = PlayerEntity.Instance.Class.Abilities;
            int offsetY = Console.CursorTop;
            Utils.ClearInteractArea(offsetY, 1);
            Utils.SetCursorInteract(offsetY);
            Utils.WriteColour("Choose an ability.", ConsoleColor.DarkYellow);
            int index = 0;
            do
            {
                Utils.ClearInteractArea(offsetY + 1, abilities.Count);
                for (int i = 0; i < abilities.Count; i++)
                {
                    Utils.SetCursorInteract(offsetY + 1 + i);
                    if (abilities[i] == abilities[index])
                    {
                        Utils.WriteColour(">>", ConsoleColor.Yellow);
                        Utils.WriteColour($"{abilities[i].Name}", ConsoleColor.Cyan);
                        Utils.SetCursorInteract(offsetY + 1, 20);
                        Console.Write(abilities[i].Description);
                        Utils.SetCursorInteract(offsetY + 2, 20);
                        Console.Write($"Cooldown: {abilities[i].Cooldown}");
                        Utils.SetCursorInteract(offsetY + 3, 20);
                        Console.Write($"Resource Cost: {abilities[i].ResourceCost}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Utils.SetCursorInteract(offsetY + 1 + i);
                        Console.Write(abilities[i].Name);
                    }
                }
                Utils.SetCursorInteract(offsetY + abilities.Count + 1);
                Console.Write("[");
                Utils.WriteColour("BACKSPACE", ConsoleColor.DarkGray);
                Console.Write("] Return");
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (index > 0 && index < abilities.Count)
                            {
                                index--;
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (index >= 0 && index < abilities.Count - 1)
                            {
                                index++;
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            return new CombatPayload(false);
                        }
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter);
            return PlayerEntity.Instance.Class.Abilities[index].UseAbility();
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
