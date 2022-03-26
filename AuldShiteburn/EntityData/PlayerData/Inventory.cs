using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.ConsumableData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.ItemData.WeaponData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.EntityData.PlayerData
{
    internal class Inventory
    {
        public Item[,] ItemList { get; set; } = new Item[Width, Height];
        public static int Width { get; } = 5;
        public static int Height { get; } = 4;

        public bool AddItem(Item item)
        {
            int typeColumn = 0;
            if (item.GetType() == typeof(WeaponItem))
            {
                typeColumn = 0;
            }
            else if (item.GetType() == typeof(ArmourItem))
            {
                typeColumn = 1;
            }
            else if (item.GetType() == typeof(ConsumableItem))
            {
                typeColumn = 2;
            }
            else if (item.GetType() == typeof(KeyItem))
            {
                typeColumn = 3;
            }

            int index = 0;
            PrintInventoryOfType(typeColumn, index);
            bool choosing = true;
            while (choosing)
            {
                bool quit = false;
                do
                {
                    InputSystem.GetInput();
                    switch (InputSystem.InputKey)
                    {
                        case ConsoleKey.LeftArrow:
                            {
                                if (index <= Width - 1 && index > 0)
                                {
                                    index--;
                                    Console.CursorLeft = 0;
                                    Console.CursorTop = 0;
                                    PrintInventoryOfType(typeColumn, index);
                                }
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            {
                                if (index >= 0 && index < Width - 1)
                                {
                                    index++;
                                    Console.CursorLeft = 0;
                                    Console.CursorTop = 0;
                                    PrintInventoryOfType(typeColumn, index);
                                }
                            }
                            break;
                        case ConsoleKey.Backspace:
                            {
                                quit = true;
                            }
                            break;
                    }
                } while (InputSystem.InputKey != ConsoleKey.Enter && !quit);
                if (InputSystem.InputKey == ConsoleKey.Enter)
                {
                    PlayerEntity.Instance.Inventory.ItemList[index, typeColumn] = item;
                    return true;
                }
                choosing = false;
            }
            return false;
        }

        public static void PrintInventoryOfType(int typeColumn, int indexHighlight)
        {
            Utils.ClearInventoryInterface();
            Utils.SetCursorInventory();
            if (typeColumn == 0)
            {
                Console.Write("Weapons: ");
            }
            if (typeColumn == 1)
            {
                Utils.SetCursorInventory(offsetY: 1);
                Console.Write("Armour: ");
            }
            if (typeColumn == 2)
            {
                Utils.SetCursorInventory(offsetY: 2);
                Console.Write("Consumables: ");
            }
            if (typeColumn == 3)
            {
                Utils.SetCursorInventory(offsetY: 3);
                Console.Write("Key Items: ");
            }
            for (int i = 0; i < Width; i++)
            {
                if (PlayerEntity.Instance.Inventory.ItemList[i, typeColumn] != null)
                {
                    if (i == indexHighlight)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write($"{PlayerEntity.Instance.Inventory.ItemList[i, typeColumn].Name} . ");
                    Console.ResetColor();
                }
                else
                {
                    if (i == indexHighlight)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write($"[Empty] . ");
                    Console.ResetColor();
                }
            }
        }
    }
}
