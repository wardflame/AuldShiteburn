using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.ConsumableData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MapData;
using AuldShiteburn.MapData.TileData;
using AuldShiteburn.MapData.TileData.Tiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.EntityData.PlayerData
{
    [Serializable]
    internal class Inventory
    {
        public const int WEAPON_OFFSET = 0;
        public const int ARMOUR_OFFSET = 35;
        public const int CONSUMABLE_OFFSET = 70;
        public const int KEY_OFFSET = 105;

        public Item[,] ItemList { get; set; } = new Item[Row, Column];
        public static int Row { get; } = 6;
        public static int Column { get; } = 4;

        public bool AddItem(Item item)
        {
            int typeColumn = GetItemTypeColumn(item);
            int typeOffset = GetItemTypeUIOffset(item);

            Utils.SetCursorInventory(offsetY: -1);
            Utils.ClearLine(60);
            Utils.SetCursorInventory(offsetY: -1);
            Utils.WriteColour($"Choose a slot to place {item.Name}.", ConsoleColor.Yellow);

            int index = 0;
            InventoryHighlight(index, typeColumn, typeOffset);
            bool choosing = true;
            while (choosing)
            {
                do
                {
                    InputSystem.GetInput();
                    switch (InputSystem.InputKey)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                if (index <= Row - 1 && index > 0)
                                {
                                    index--;
                                    Console.CursorLeft = 0;
                                    Console.CursorTop = 0;
                                    PlayerEntity.Instance.PrintInventory();
                                    InventoryHighlight(index, typeColumn, typeOffset);
                                }
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            {
                                if (index >= 0 && index < Row - 1)
                                {
                                    index++;
                                    Console.CursorLeft = 0;
                                    Console.CursorTop = 0;
                                    PlayerEntity.Instance.PrintInventory();
                                    InventoryHighlight(index, typeColumn, typeOffset);
                                }
                            }
                            break;
                        case ConsoleKey.Backspace:
                            {
                                PlayerEntity.Instance.PrintInventory();
                                return false;
                            }
                            break;
                    }
                } while (InputSystem.InputKey != ConsoleKey.Enter);
                if (InputSystem.InputKey == ConsoleKey.Enter)
                {
                    if (PlayerEntity.Instance.Inventory.ItemList[index, typeColumn] != null)
                    {
                        int emptySlot = CheckForEmptySlot(typeColumn);
                        if (emptySlot > 0)
                        {
                            PlayerEntity.Instance.Inventory.ItemList[emptySlot, typeColumn] = item;
                        }
                        else
                        {
                            Item previousItem = PlayerEntity.Instance.Inventory.ItemList[index, typeColumn];
                            Utils.SetCursorInventory(offsetY: -1);
                            Utils.ClearLine(60);
                            Utils.SetCursorInventory(offsetY: -1);
                            Utils.WriteColour($"No empty slots available. Drop {previousItem.Name}? (Y/N)", ConsoleColor.Red);
                            if (Utils.VerificationQuery(null))
                            {
                                PlayerEntity.Instance.Inventory.ItemList[index, typeColumn] = item;
                                DropItem(previousItem);
                                PlayerEntity.Instance.PrintInventory();
                                return true;
                            }
                            else
                            {
                                PlayerEntity.Instance.PrintInventory();
                                return false;
                            }
                        }                        
                    }
                    else
                    {
                        PlayerEntity.Instance.Inventory.ItemList[index, typeColumn] = item;
                    }
                    PlayerEntity.Instance.PrintInventory();
                    return true;
                }
                PlayerEntity.Instance.PrintInventory();
                choosing = false;
            }
            return false;
        }

        private void DropItem (Item dropItem)
        {
            Utils.SetCursorInventory(offsetY: -1);
            Utils.ClearLine(60);
            Utils.SetCursorInventory(offsetY: -1);
            Utils.WriteColour($"Dropped {dropItem.Name} on the floor.", ConsoleColor.Red);
            Tile currentTile = Map.Instance.CurrentArea.GetTile(PlayerEntity.Instance.PosX, PlayerEntity.Instance.PosY);
            if (currentTile is LootTile)
            {
                LootTile tile = (LootTile)currentTile;
                tile.items.Add(dropItem);
            }
            else
            {
                Map.Instance.CurrentArea.SetTile(PlayerEntity.Instance.PosX, PlayerEntity.Instance.PosY,
                new LootTile(
                    new List<Item>()
                    {
                        dropItem
                    },
                    false));
            }
        }

        private int GetItemTypeColumn(Item item)
        {
            if (item.GetType() == typeof(WeaponItem))
            {
                return 0;
            }
            else if (item.GetType() == typeof(ArmourItem))
            {
                return 1;
            }
            else if (item.GetType() == typeof(ConsumableItem))
            {
                return 2;
            }
            else if (item.GetType() == typeof(KeyItem))
            {
                return 3;
            }
            return 0;
        }

        private int GetItemTypeUIOffset(Item item)
        {
            if (item.GetType() == typeof(WeaponItem))
            {
                return WEAPON_OFFSET;
            }
            else if (item.GetType() == typeof(ArmourItem))
            {
                return ARMOUR_OFFSET;
            }
            else if (item.GetType() == typeof(ConsumableItem))
            {
                return CONSUMABLE_OFFSET;
            }
            else if (item.GetType() == typeof(KeyItem))
            {
                return KEY_OFFSET;
            }
            return 0;
        }

        private int CheckForEmptySlot(int typeColumn)
        {
            for (int i = 0; i < Row; i++)
            {
                if (PlayerEntity.Instance.Inventory.ItemList[i, typeColumn] == null)
                {
                    return i;
                }
            }
            return 0;
        }

        public static void InventoryHighlight(int index, int typeColumn, int typeOffset)
        {
            for (int y = 1; y <= Row; y++)
            {
                Utils.SetCursorInventory(typeOffset, y);
                if (PlayerEntity.Instance.Inventory.ItemList[y - 1, typeColumn] != null)
                {
                    if (y - 1 == index )
                    {
                        Utils.WriteColour(">>", ConsoleColor.Yellow);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.Write($"{PlayerEntity.Instance.Inventory.ItemList[y - 1, typeColumn].Name}");
                    Console.ResetColor();
                }
                else
                {
                    if (y - 1 == index )
                    {
                        Utils.WriteColour(">>", ConsoleColor.Yellow);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.Write($"--");
                    Console.ResetColor();
                }
            }
        }
    }
}
