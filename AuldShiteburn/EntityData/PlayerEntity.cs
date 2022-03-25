using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.ItemData;
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
        public bool InMenu { get; set; }
        public long Playtime { get; set; }
        public ClassData Class { get; private set; }
        public ClassType ClassType { get; private set; }
        public List<Item> Inventory { get; set; } = new List<Item>();
        public WeaponItem EquippedWeapon { get; set; }


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
                    case ConsoleKey.Escape:
                        {
                            Menu.Instance = new PauseMenu();
                            Menu.Instance.RunMenu();
                        }
                        break;
                }
            }
        }

        public static PlayerEntity GenerateCharacter()
        {
            Instance = new PlayerEntity();
            Instance.PosX = 1;
            Instance.PosY = 1;
            Random rand = new Random();

            #region Class Generation and Stat Assignments
            Instance.Class = ClassData.Classes[rand.Next(ClassData.Classes.Count)];
            Instance.ClassType = Instance.Class.ClassType;
            Instance.maxHP = Instance.Class.HP;
            Instance.HP = Instance.Class.HP;
            Instance.UsesStamina = Instance.Class.UsesStamina;
            Instance.UsesMana = Instance.Class.UsesMana;
            Instance.maxStamina = Instance.Class.Stamina;
            Instance.Stamina = Instance.Class.Stamina;
            Instance.maxMana = Instance.Class.Mana;
            Instance.Mana = Instance.Class.Mana;
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
                int titleIndex = rand.Next(0, Instance.Class.TitlesMale.Count);
                if (Instance.Class.TitlesFemale != null)
                {
                    titleStr = Instance.Class.TitlesFemale[titleIndex];
                }
                else
                {
                    titleStr = Instance.Class.TitlesMale[titleIndex];
                }
                Instance.Name = $"{titleStr} {forenameStr}";
            }
            #endregion Name Generation

            #region Loot Assignment
            Instance.Inventory.Add(WeaponItem.GenerateWeapon());
            #endregion Loot Assignment

            return Instance;
        }
    }
}
