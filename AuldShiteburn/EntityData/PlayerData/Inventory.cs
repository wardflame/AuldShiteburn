using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData.PlayerData.Classes;
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
        public static int WeaponOffset { get; } = 0;
        public static int ArmourOffset { get; } = 34;
        public static int ConsumableOffset { get; } = 68;
        public static int KeyOffset { get; } = 102;

        public Item[,] ItemList { get; set; }
        public int Row { get; }
        public int Column { get; }

        public Inventory(int row, int column)
        {
            Row = row;
            Column = column;
            ItemList = new Item[Row, Column];
        }

        public bool AddItem(Item item, bool playerElseInteract)
        {
            int typeColumn = GetItemTypeColumn(item);
            int typeOffset = GetItemTypeUIOffset(item);
            if (playerElseInteract)
            {
                Utils.SetCursorInventory(-1);
                Utils.ClearLine(60);
                Utils.SetCursorInventory(-1);
            }
            else
            {
                Utils.SetCursorInteract(-1);
                Utils.ClearLine(40);
                Utils.SetCursorInteract(-1);
            }
            Utils.WriteColour($"Choose a slot to place {item.Name}.", ConsoleColor.Yellow);

            InventorySortData sortData = NavigateInventory(playerElseInteract, typeColumn, typeOffset);
            if (sortData.index < 0)
            {
                return false;
            }
            if (InputSystem.InputKey == ConsoleKey.Enter)
            {
                if (ItemList[sortData.index, typeColumn] != null)
                {
                    int emptySlot = CheckForEmptySlot(typeColumn);
                    if (emptySlot > 0)
                    {
                        ItemList[emptySlot, typeColumn] = item;
                    }
                    else
                    {
                        Item previousItem = ItemList[sortData.index, typeColumn];
                        if (playerElseInteract)
                        {
                            Utils.SetCursorInventory(-1);
                            Utils.ClearLine(60);
                            Utils.SetCursorInventory(-1);
                        }
                        else
                        {
                            Utils.SetCursorInteract(-1);
                            Utils.ClearLine(40);
                            Utils.SetCursorInteract(-1);
                        }                        
                        Utils.WriteColour($"No empty slots available. Drop {previousItem.Name}? (Y/N)", ConsoleColor.Red);
                        if (Utils.VerificationQuery(null))
                        {
                            if (DropItem(previousItem, sortData))
                            {
                                ItemList[sortData.index, typeColumn] = item;
                            }
                            PrintInventory(playerElseInteract);
                            return true;
                        }
                        else
                        {
                            PrintInventory(playerElseInteract);
                            return false;
                        }
                    }                        
                }
                else
                {
                    ItemList[sortData.index, typeColumn] = item;
                }
                PrintInventory(playerElseInteract);
                return true;
            }
            PrintInventory(playerElseInteract);
            return false;
        }

        public void PrintInventory(bool playerElseInteract)
        {
            int offset = 0;
            for (int x = 0; x < Column; x++)
            {
                if (x == 0)
                {
                    if (playerElseInteract)
                    {
                        Utils.SetCursorInventory(offsetX: WeaponOffset);
                    }
                    else
                    {
                        Utils.SetCursorInteract(offsetX: WeaponOffset);
                    }
                    offset = WeaponOffset;
                    Console.Write("[");
                    Utils.WriteColour("WEAPONS", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 1)
                {
                    if (playerElseInteract)
                    {
                        Utils.SetCursorInventory(offsetX: ArmourOffset);
                    }
                    else
                    {
                        Utils.SetCursorInteract(offsetX: ArmourOffset);
                    }
                    offset = ArmourOffset;
                    Console.Write("[");
                    Utils.WriteColour("ARMOUR", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 2)
                {
                    if (playerElseInteract)
                    {
                        Utils.SetCursorInventory(offsetX: ConsumableOffset);
                    }
                    else
                    {
                        Utils.SetCursorInteract(offsetX: ConsumableOffset);
                    }
                    offset = ConsumableOffset;
                    Console.Write("[");
                    Utils.WriteColour("CONSUMABLES", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                if (x == 3)
                {
                    if (playerElseInteract)
                    {
                        Utils.SetCursorInventory(offsetX: KeyOffset);
                    }
                    else
                    {
                        Utils.SetCursorInteract(offsetX: KeyOffset);
                    }
                    offset = KeyOffset;
                    Console.Write("[");
                    Utils.WriteColour("KEY ITEMS", ConsoleColor.DarkYellow);
                    Console.Write("]");
                }
                for (int y = 1; y <= Row; y++)
                {
                    if (playerElseInteract)
                    {
                        Utils.SetCursorInventory(y, offset);
                    }
                    else
                    {
                        Utils.SetCursorInteract(y, offset);
                    }
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

        public InventorySortData NavigateInventory(bool playerElseInteract, int typeColumn = 0, int typeOffset = 0)
        {
            InventorySortData sortData = new InventorySortData();
            int index = 0;
            InventoryHighlight(playerElseInteract, this, index, typeColumn, typeOffset);
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
                                if (playerElseInteract)
                                {
                                    Utils.ClearInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract();
                                }
                                PrintInventory(playerElseInteract);
                                InventoryHighlight(playerElseInteract, this, index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (index >= 0 && index < Row - 1)
                            {
                                index++;
                                if (playerElseInteract)
                                {
                                    Utils.ClearInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract();
                                }
                                PrintInventory(playerElseInteract);
                                InventoryHighlight(playerElseInteract, this, index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        {
                            if (typeColumn <= Column - 1 && typeColumn > 0)
                            {
                                typeColumn--;
                                typeOffset -= 34;
                                if (playerElseInteract)
                                {
                                    Utils.ClearInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract();
                                }
                                PrintInventory(playerElseInteract);
                                InventoryHighlight(playerElseInteract, this, index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        {
                            if (typeColumn >= 0 && typeColumn < Column - 1)
                            {
                                typeColumn++;
                                typeOffset += 34;
                                if (playerElseInteract)
                                {
                                    Utils.ClearInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract();
                                }
                                PrintInventory(playerElseInteract);
                                InventoryHighlight(playerElseInteract, this, index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            PrintInventory(playerElseInteract);
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

        public void PlayerItemInteract(bool playerElseInteract)
        {
            bool interacting = true;
            while (interacting)
            {
                InventorySortData sortData = NavigateInventory(playerElseInteract);
                if (sortData.index < 0)
                {
                    return;
                }
                Item currentItem = ItemList[sortData.index, sortData.typeColumn];
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
            Tile currentTile = Map.Instance.CurrentArea.GetTile(EntityData.PlayerEntity.Instance.PosX, EntityData.PlayerEntity.Instance.PosY);
            if (currentTile is LootTile)
            {
                LootTile tile = (LootTile)currentTile;
                tile.items.Add(dropItem);
            }
            else
            {
                int spawnX = EntityData.PlayerEntity.Instance.PosX;
                int spawnY = EntityData.PlayerEntity.Instance.PosY;
                bool tileFound = false;
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }
                        int checkX = EntityData.PlayerEntity.Instance.PosX + x;
                        int checkY = EntityData.PlayerEntity.Instance.PosY + y;
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
                EntityData.PlayerEntity.Instance.Inventory.ItemList[sortData.index, sortData.typeColumn] = null;
                Map.Instance.PrintTile(spawnX, spawnY);
            }
            return true;
        }

        /// <summary>
        /// Get the ItemList[,] column by item type to
        /// ensure items are divided neatly into their
        /// respective categories.
        /// </summary>
        /// <param name="item">Item to check for type.</param>
        /// <returns>Column of the ItemList[,] to use.</returns>
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
                return WeaponOffset;
            }
            else if (item.GetType() == typeof(ArmourItem))
            {
                return ArmourOffset;
            }
            else if (item.GetType() == typeof(ConsumableItem))
            {
                return ConsumableOffset;
            }
            else if (item.GetType() == typeof(KeyItem))
            {
                return KeyOffset;
            }
            return 0;
        }

        private int CheckForEmptySlot(int typeColumn)
        {
            for (int i = 0; i < Row; i++)
            {
                if (EntityData.PlayerEntity.Instance.Inventory.ItemList[i, typeColumn] == null)
                {
                    return i;
                }
            }
            return 0;
        }

        private void InventoryHighlight(bool playerElseInteract, Inventory inventory, int index, int typeColumn, int typeOffset)
        {
            Item highlightItem = ItemList[index, typeColumn];
            for (int y = 1; y <= Row; y++)
            {
                if (playerElseInteract)
                {
                    Utils.SetCursorInventory(y, typeOffset);
                }
                else
                {
                    Utils.SetCursorInteract(y, typeOffset);
                }
                if (ItemList[y - 1, typeColumn] != null)
                {
                    if (y - 1 == index )
                    {
                        Utils.WriteColour(">>", ConsoleColor.Yellow);
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
                        Console.Write($"{ItemList[y - 1, typeColumn].Name}");
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
            if (weapon.Property.Type != PropertyDamageType.Standard && PlayerEntity.Instance.Class.GetType() != typeof(FighterClass))
            {
                if (weapon.Property.HasAffinity)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                Console.Write($"{weapon.Property.Name} ");
                Console.ResetColor();
            }

            if (weapon.Material.HasAffinity)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            Console.Write($"{weapon.Material.Name} ");
            Console.ResetColor();

            if (weapon.Type.IsProficient)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            Console.Write($"{weapon.Type.Name} ");
            Console.ResetColor();
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
