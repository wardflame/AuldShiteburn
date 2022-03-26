using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.ItemData;
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
        public bool InMenu { get; set; }
        public long Playtime { get; set; }
        public ClassData Class { get; private set; }
        public ClassType ClassType { get; private set; }
        public Inventory Inventory { get; set; } = new Inventory();
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
            Instance.Inventory.ItemList[0,0] = WeaponItem.GenerateWeapon();
            Instance.Inventory.ItemList[1,0] = WeaponItem.GenerateWeapon();
            Instance.Inventory.ItemList[2,0] = WeaponItem.GenerateWeapon();
            Instance.Inventory.ItemList[3,0] = WeaponItem.GenerateWeapon();
            Instance.Inventory.ItemList[4,0] = WeaponItem.GenerateWeapon();
            #endregion Loot Assignment

            return Instance;
        }

        public void PrintInventory()
        {
            Utils.ClearInventoryInterface();
            Utils.SetCursorInventory();
            int typeOffset = 0;
            for (int x = 0; x < Inventory.CategoryColumns; x++)
            {
                if (x == 0)
                {
                    Console.Write("[WEAPONS]");
                }
                if (x == 1)
                {
                    typeOffset = 25;
                    Utils.SetCursorInventory(typeOffset);
                    Console.Write("[ARMOUR]");
                }
                if (x == 2)
                {
                    typeOffset = 45;
                    Utils.SetCursorInventory(typeOffset);
                    Console.Write("[CONSUMABLES]");
                }
                if (x == 3)
                {
                    typeOffset = 65;
                    Utils.SetCursorInventory(typeOffset);
                    Console.Write("[KEY ITEMS]");
                }
                for (int y = 1; y <= Inventory.RowCapacity; y++)
                {
                    Utils.SetCursorInventory(typeOffset, y);
                    {
                        if (Inventory.ItemList[y - 1, x] != null)
                        {
                            Console.Write($"{Inventory.ItemList[y - 1, x].Name}");
                        }
                        else
                        {
                            Console.Write("--");
                        }
                    }                    
                }
            }
        }
    }
}
