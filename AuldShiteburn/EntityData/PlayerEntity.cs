using AuldShiteburn.ItemData;
using AuldShiteburn.MenuData.Menus;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData
{
    internal class PlayerEntity : LivingEntity
    {
        public static PlayerEntity Instance { get; private set; }
        List<Item> inventory;
        public override string EntityChar => "PL";
        public bool inMenu;

        public override void Move()
        {
            if (!inMenu)
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
                    case ConsoleKey.P:
                        {
                            inMenu = true;
                            PauseMenu.InMenu();
                            inMenu = false;
                        }
                        break;
                }
            }
        }

        public static PlayerEntity GenerateCharacter()
        {
            Instance = new PlayerEntity();
            return Instance;
        }
    }
}
