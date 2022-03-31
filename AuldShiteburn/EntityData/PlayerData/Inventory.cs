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
        public int PLAYER_WEAPON_OFFSET = 1;
        public int PLAYER_ARMOUR_OFFSET = 35;
        public int PLAYER_CONSUMABLE_OFFSET = 70;
        public int PLAYER_KEY_OFFSET = 105;
        public int INTERACT_WEAPON_OFFSET = Utils.UIInteractOffset;
        public int INTERACT_ARMOUR_OFFSET = Utils.UIInteractOffset + 34;
        public int INTERACT_CONSUMABLE_OFFSET = Utils.UIInteractOffset + 69;
        public int INTERACT_KEY_OFFSET = Utils.UIInteractOffset + 104;

        public Item[,] ItemList { get; set; } = new Item[Row, Column];
        public static int Row { get; } = 6;
        public static int Column { get; } = 4;

        public bool AddItem(Item item)
        {
            int typeColumn = GetItemTypeColumn(item);
            int typeOffset = GetItemTypeUIOffset(item);

            Utils.SetCursorInventory(-1);
            Utils.ClearLine(60);
            Utils.SetCursorInventory(-1);
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
                        Utils.SetCursorInventory(-1);
                        Utils.ClearLine(60);
                        Utils.SetCursorInventory(-1);
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

        public void PrintInventory(int weaponOffset, int armourOffset, int consumableOffset, int keyOffset)
        {
            int offset = 0;
            for (int x = 0; x < Column; x++)
            {
                if (x == 0)
                {
                    Console.CursorLeft = weaponOffset;
                    offset = weaponOffset;
                    Console.Write("[");
                    Utils.WriteColour("WEAPONS", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 1)
                {
                    Console.CursorLeft = armourOffset;
                    offset = armourOffset;
                    Console.Write("[");
                    Utils.WriteColour("ARMOUR", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 2)
                {
                    Console.CursorLeft = consumableOffset;
                    offset = consumableOffset;
                    Console.Write("[");
                    Utils.WriteColour("CONSUMABLES", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 3)
                {
                    Console.CursorLeft = keyOffset;
                    offset = keyOffset;
                    Console.Write("[");
                    Utils.WriteColour("KEY ITEMS", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                for (int y = 1; y <= Row; y++)
                {
                    Console.CursorLeft = offset;
                    Console.CursorTop++;
                    {
                        if (ItemList[y - 1, x] != null)
                        {
                            Utils.WriteColour($"{ItemList[y - 1, x].Name}", ConsoleColor.DarkGray);
                        }
                        else
                        {
                            Utils.WriteColour("--", ConsoleColor.DarkGray);
                        }
                    }
                }
            }
        }

        private InventorySortData NavigateInventory(int typeColumn = 0, int typeOffset = 0)
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
                if (currentItem == null)
                {
                    Utils.WriteColour("No item selected.");
                    return;
                }
                Console.WriteLine($"What do you wish to do with {currentItem.Name}?");
                if (currentItem is WeaponItem)
                {
                    Utils.SetCursorInteract(1);
                    Console.WriteLine("(E) Equip Weapon");
                }
                else if (currentItem is ArmourItem)
                {
                    Utils.SetCursorInteract(1);
                    Console.WriteLine("(E) Equip Armour");
                }
                else if (currentItem is ConsumableItem)
                {
                    Utils.SetCursorInteract(1);
                    Console.WriteLine("(E) Consume");
                }
                Utils.SetCursorInteract(2);
                Console.WriteLine("(D) Drop Item");
                bool choosing = true;
                while (choosing)
                {
                    InputSystem.GetInput();
                    switch (InputSystem.InputKey)
                    {
                        case ConsoleKey.E:
                            {
                                currentItem.OnInventoryUse(sortData);
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
            Utils.SetCursorInventory(-1);
            Utils.ClearLine(60);
            Utils.SetCursorInventory(-1);
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
                return INTERACT_WEAPON_OFFSET;
            }
            else if (item.GetType() == typeof(ArmourItem))
            {
                return INTERACT_ARMOUR_OFFSET;
            }
            else if (item.GetType() == typeof(ConsumableItem))
            {
                return INTERACT_CONSUMABLE_OFFSET;
            }
            else if (item.GetType() == typeof(KeyItem))
            {
                return INTERACT_KEY_OFFSET;
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
            Item highlightItem = PlayerEntity.Instance.Inventory.ItemList[index, typeColumn];
            for (int y = 1; y <= Row; y++)
            {
                Utils.SetCursorInventory(y, typeOffset);
                if (PlayerEntity.Instance.Inventory.ItemList[y - 1, typeColumn] != null)
                {
                    if (y - 1 == index )
                    {
                        Utils.WriteColour(">>", ConsoleColor.Yellow);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    if (highlightItem is WeaponItem)
                    {
                        WeaponAffinityCheck((WeaponItem)highlightItem);
                    }
                    else if (highlightItem is ArmourItem)
                    {
                        ArmourAffinityCheck((ArmourItem)highlightItem);
                    }
                    else
                    {
                        Console.Write($"{PlayerEntity.Instance.Inventory.ItemList[y - 1, typeColumn].Name}");
                    }
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

        public static void WeaponAffinityCheck(WeaponItem weapon)
        {
            if (weapon.Property.HasAffinity)
            {
                Utils.WriteColour($"{weapon.Property.Name} ", ConsoleColor.DarkGreen);
            }
            else
            {
                Console.Write($"{weapon.Property.Name} ");
            }
            if (weapon.Material.HasAffinity)
            {
                Utils.WriteColour($"{weapon.Material.Name} ", ConsoleColor.DarkGreen);
            }
            else
            {
                Console.Write($"{weapon.Material.Name} ");
            }
            if (weapon.Type.IsProficient)
            {
                Utils.WriteColour($"{weapon.Type.Name} ", ConsoleColor.DarkGreen);
            }
            else
            {
                Console.Write($"{weapon.Type.Name} ");
            }
        }

        public static void ArmourAffinityCheck(ArmourItem armour)
        {
            if (armour.IsPhysicalProficient && armour.HasPropertyAffinity)
            {
                Utils.WriteColour($"{armour.Name} ", ConsoleColor.DarkGreen);
            }
            else
            {
                Console.Write($"{armour.Name} ");
            }
        }
    }
}
