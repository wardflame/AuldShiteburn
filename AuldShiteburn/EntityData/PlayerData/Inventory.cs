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

            InventorySortData sortData = NavigateInventory(typeColumn, typeOffset);
            if (sortData.index < 0)
            {
                return false;
            }
            if (InputSystem.InputKey == ConsoleKey.Enter)
            {
                if (PlayerEntity.Instance.Inventory.ItemList[sortData.index, typeColumn] != null)
                {
                    int emptySlot = CheckForEmptySlot(typeColumn);
                    if (emptySlot > 0)
                    {
                        PlayerEntity.Instance.Inventory.ItemList[emptySlot, typeColumn] = item;
                    }
                    else
                    {
                        Item previousItem = PlayerEntity.Instance.Inventory.ItemList[sortData.index, typeColumn];
                        Utils.SetCursorInventory(offsetY: -1);
                        Utils.ClearLine(60);
                        Utils.SetCursorInventory(offsetY: -1);
                        Utils.WriteColour($"No empty slots available. Drop {previousItem.Name}? (Y/N)", ConsoleColor.Red);
                        if (Utils.VerificationQuery(null))
                        {
                            if (DropItem(previousItem, sortData))
                            {
                                PlayerEntity.Instance.Inventory.ItemList[sortData.index, typeColumn] = item;
                            }
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
                    PlayerEntity.Instance.Inventory.ItemList[sortData.index, typeColumn] = item;
                }
                PlayerEntity.Instance.PrintInventory();
                return true;
            }
            PlayerEntity.Instance.PrintInventory();
            return false;
        }

        private InventorySortData NavigateInventory(int typeColumn = 0, int typeOffset = WEAPON_OFFSET)
        {
            InventorySortData sortData = new InventorySortData();
            int index = 0;
            InventoryHighlight(index, typeColumn, typeOffset);
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
                                PlayerEntity.Instance.PrintInventory();
                                InventoryHighlight(index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        {
                            if (typeColumn <= Column - 1 && typeColumn > 0)
                            {
                                typeColumn--;
                                typeOffset -= 35;
                                PlayerEntity.Instance.PrintInventory();
                                InventoryHighlight(index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        {
                            if (typeColumn >= 0 && typeColumn < Column - 1)
                            {
                                typeColumn++;
                                typeOffset += 35;
                                PlayerEntity.Instance.PrintInventory();
                                InventoryHighlight(index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            PlayerEntity.Instance.PrintInventory();
                            sortData.index = -1;
                            return sortData;
                        }
                        break;
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter);
            sortData.index = index;
            sortData.typeColumn = typeColumn;
            sortData.typeOffset = typeOffset;
            return sortData;
        }

        public void ItemInteract()
        {
            bool interacting = true;
            while (interacting)
            {
                InventorySortData sortData = NavigateInventory();
                if (sortData.index < 0)
                {
                    return;
                }
                Item currentItem = PlayerEntity.Instance.Inventory.ItemList[sortData.index, sortData.typeColumn];
                Utils.SetCursorInteract();
                Console.WriteLine($"What do you wish to do with {currentItem.Name}?");
                if (currentItem is WeaponItem)
                {
                    Utils.SetCursorInteract(offsetY: 1);
                    Console.WriteLine("(E) Equip Weapon");
                }
                else if (currentItem is ArmourItem)
                {
                    Utils.SetCursorInteract(offsetY: 1);
                    Console.WriteLine("(E) Equip Armour");
                }
                else if (currentItem is ConsumableItem)
                {
                    Utils.SetCursorInteract(offsetY: 1);
                    Console.WriteLine("(E) Consume");
                }
                Utils.SetCursorInteract(offsetY: 2);
                Console.WriteLine("(D) Drop Item");
                bool choosing = true;
                while (choosing)
                {
                    InputSystem.GetInput();
                    switch (InputSystem.InputKey)
                    {
                        case ConsoleKey.E:
                            {
                                currentItem.OnInventoryUse();
                                choosing = false;
                                interacting = false;
                            }
                            break;
                        case ConsoleKey.D:
                            {
                                PlayerEntity.Instance.Inventory.DropItem(currentItem, sortData);
                                choosing = false;
                                interacting = false;
                            }
                            break;
                        case ConsoleKey.Backspace:
                            {
                                choosing = false;
                                interacting = false;
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Attempt to drop an item. If the tile the player is on is a loot tile,
        /// add the dropped item to that loot tile. Otherwise, spawn a new loot tile
        /// at the player's location if there is room to do so.
        /// </summary>
        /// <param name="dropItem">Item to be dropped.</param>
        /// <returns>Returns true if item was dropped, else returns false.</returns>
        private bool DropItem (Item dropItem, InventorySortData sortData)
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
                int spawnX = PlayerEntity.Instance.PosX;
                int spawnY = PlayerEntity.Instance.PosY;
                bool tileFound = false;
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }
                        int checkX = PlayerEntity.Instance.PosX + x;
                        int checkY = PlayerEntity.Instance.PosY + y;
                        Tile emptyTile = Map.Instance.CurrentArea.GetTile(checkX, checkY);
                        if (emptyTile != null && emptyTile.DisplayChar == Tile.AirTile.DisplayChar)
                        {
                            spawnX = checkX;
                            spawnY = checkY;
                            tileFound = true;
                            break;
                        }
                    }
                    if (tileFound)
                    {
                        break;
                    }
                }
                if (!tileFound)
                {
                    return false;
                }                
                Map.Instance.CurrentArea.SetTile(spawnX, spawnY,
                new LootTile(
                    new List<Item>()
                    {
                        dropItem
                    },
                    false, true));
                PlayerEntity.Instance.Inventory.ItemList[sortData.index, sortData.typeColumn] = null;
                Map.Instance.PrintTile(spawnX, spawnY);
            }
            return true;
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
