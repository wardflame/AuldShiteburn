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
        public int Row { get; set; }
        public int Column { get; set; }

        public Inventory(int row, int column)
        {
            Row = row;
            Column = column;
            ItemList = new Item[Row, Column];
        }

        public void EngageStorage()
        {
            /// Do we want the player inventory (true), or the non-player inventory (false)?.
            bool playerElseStorage = false;
            bool browsing = true;
            InventorySortData sortData;
            while (browsing)
            {
                Utils.SetCursorInventory(-2, 100);
                Utils.WriteColour("(BACKSPACE) Leave", ConsoleColor.DarkCyan);
                Utils.SetCursorInventory(-4, 100);
                Utils.WriteColour("(TAB) Switch between storage and inventory", ConsoleColor.DarkCyan);
                switch (playerElseStorage)
                {
                    case true:
                        {
                            PrintInventory(!playerElseStorage);
                            sortData = PlayerEntity.Instance.Inventory.NavigateInventory(playerElseStorage);
                            if (sortData.index < 0)
                            {
                                browsing = false;
                                break;
                            }
                            else
                            {
                                if (sortData.transferIntended)
                                {
                                    StorageTransfer(sortData, this);
                                }
                            }
                            Utils.ClearInventoryInterface();
                            PlayerEntity.Instance.Inventory.PrintInventory(playerElseStorage);
                            playerElseStorage = sortData.isPlayerInventory;
                        }
                        break;
                    case false:
                        {
                            PrintInventory(playerElseStorage);
                            sortData = NavigateInventory(playerElseStorage);
                            if (sortData.index < 0)
                            {
                                browsing = false;
                                break;
                            }
                            else
                            {
                                if (sortData.transferIntended)
                                {
                                    PlayerEntity.Instance.Inventory.StorageTransfer(sortData, this);
                                }
                            }
                            Utils.ClearInteractInterface();
                            PrintInventory(playerElseStorage);
                            playerElseStorage = sortData.isPlayerInventory;
                        }
                        break;
                }
                Utils.ClearAreaInteract(-1, 31);
            }
        }

        public void PrintInventory(bool playerElseStorage)
        {
            int offset = 0;
            for (int x = 0; x < Column; x++)
            {
                if (x == 0)
                {
                    if (playerElseStorage)
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
                    if (playerElseStorage)
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
                    if (playerElseStorage)
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
                    if (playerElseStorage)
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
                    if (playerElseStorage)
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

        public InventorySortData NavigateInventory(bool playerElseStorage, int typeColumn = 0, int typeOffset = 0)
        {
            InventorySortData sortData = new InventorySortData();
            int index = 0;
            InventoryHighlight(playerElseStorage, index, typeColumn, typeOffset);
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
                                if (playerElseStorage)
                                {
                                    Utils.ClearInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract();
                                }
                                PrintInventory(playerElseStorage);
                                InventoryHighlight(playerElseStorage, index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        {
                            if (index >= 0 && index < Row - 1)
                            {
                                index++;
                                if (playerElseStorage)
                                {
                                    Utils.ClearInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract();
                                }
                                PrintInventory(playerElseStorage);
                                InventoryHighlight(playerElseStorage, index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        {
                            if (typeColumn <= Column - 1 && typeColumn > 0)
                            {
                                typeColumn--;
                                typeOffset -= 34;
                                if (playerElseStorage)
                                {
                                    Utils.ClearInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract();
                                }
                                PrintInventory(playerElseStorage);
                                InventoryHighlight(playerElseStorage, index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        {
                            if (typeColumn >= 0 && typeColumn < Column - 1)
                            {
                                typeColumn++;
                                typeOffset += 34;
                                if (playerElseStorage)
                                {
                                    Utils.ClearInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract();
                                }
                                PrintInventory(playerElseStorage);
                                InventoryHighlight(playerElseStorage, index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            Utils.ClearInventoryInterface();
                            PrintInventory(playerElseStorage);
                            sortData.index = -1;
                            return sortData;
                        }
                        break;
                    case ConsoleKey.Tab:
                        {
                            if (playerElseStorage)
                            {
                                sortData.isPlayerInventory = false;
                            }
                            else
                            {
                                sortData.isPlayerInventory = true;
                            }
                            sortData.index = index;
                            sortData.typeColumn = typeColumn;
                            sortData.typeOffset = typeOffset;
                            sortData.transferIntended = false;
                            return sortData;
                        }
                        break;
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter);
            sortData.index = index;
            sortData.typeColumn = typeColumn;
            sortData.typeOffset = typeOffset;
            sortData.isPlayerInventory = playerElseStorage;
            sortData.transferIntended = true;
            return sortData;
        }

        public void StorageTransfer(InventorySortData sortData, Inventory secondaryInventory)
        {
            bool interacting = true;
            while (interacting)
            {
                Item currentItem;
                if (sortData.isPlayerInventory)
                {
                    currentItem = PlayerEntity.Instance.Inventory.ItemList[sortData.index, sortData.typeColumn];
                }
                else
                {
                    currentItem = secondaryInventory.ItemList[sortData.index, sortData.typeColumn];
                }
                Utils.SetCursorInventory(-6, 50);
                if (currentItem == null)
                {
                    Utils.ClearLine(60);
                    Utils.WriteColour("No item selected.");
                    return;
                }
                Utils.WriteColour($"What do you want to do with {currentItem.Name}?", ConsoleColor.DarkYellow);
                Utils.SetCursorInventory(-4, 50);
                Console.WriteLine("(T) Transfer to Inventory");
                Utils.SetCursorInventory(-2, 50);
                Console.WriteLine("(Backspace) Cancel");
                bool choosing = true;
                while (choosing)
                {
                    InputSystem.GetInput();
                    switch (InputSystem.InputKey)
                    {
                        case ConsoleKey.T:
                            {
                                if (sortData.isPlayerInventory)
                                {
                                    if (AddItem(currentItem, !sortData.isPlayerInventory))
                                    {
                                        PlayerEntity.Instance.Inventory.ItemList[sortData.index, sortData.typeColumn] = null;
                                    }
                                }
                                else
                                {
                                    if (PlayerEntity.Instance.Inventory.AddItem(currentItem, !sortData.isPlayerInventory))
                                    {
                                        secondaryInventory.ItemList[sortData.index, sortData.typeColumn] = null;
                                    }
                                }
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

        public void PlayerItemInteract(bool playerElseStorage)
        {
            bool interacting = true;
            while (interacting)
            {
                InventorySortData sortData = NavigateInventory(playerElseStorage);
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
                Console.Write($"What do you wish to do with {currentItem.Name}?");
                if (currentItem is WeaponItem)
                {
                    Utils.SetCursorInteract(1);
                    Console.Write("(E) Equip Weapon");
                }
                else if (currentItem is ArmourItem)
                {
                    Utils.SetCursorInteract(1);
                    Console.Write("(E) Equip Armour");
                }
                else if (currentItem is ConsumableItem)
                {
                    Utils.SetCursorInteract(1);
                    Console.Write("(E) Consume");
                }
                else if (currentItem is KeyItem)
                {
                    Utils.SetCursorInteract(1);
                    KeyItem key = (KeyItem)currentItem;
                    Utils.WriteColour($@"'{key.Description}'", ConsoleColor.DarkYellow);
                }
                Utils.SetCursorInteract(2);
                Console.WriteLine("(D) Drop Item");
                Utils.SetCursorInteract(3);
                Console.WriteLine("(Backspace) Cancel");
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

        public bool AddItem(Item item, bool playerElseStorage)
        {
            int typeColumn = GetItemTypeColumn(item);
            int typeOffset = GetItemTypeUIOffset(item);
            if (playerElseStorage)
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
            InventorySortData sortData = NavigateInventory(playerElseStorage, typeColumn, typeOffset);
            if (sortData.index < 0)
            {
                return false;
            }
            if (InputSystem.InputKey == ConsoleKey.Enter)
            {
                if (ItemList[sortData.index, typeColumn] != null)
                {
                    int emptySlot = CheckForEmptySlot(playerElseStorage, typeColumn);
                    if (emptySlot > 0)
                    {
                        ItemList[emptySlot, typeColumn] = item;
                    }
                    else
                    {
                        Item previousItem = ItemList[sortData.index, typeColumn];
                        if (playerElseStorage)
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
                            PrintInventory(playerElseStorage);
                            return true;
                        }
                        else
                        {
                            PrintInventory(playerElseStorage);
                            return false;
                        }
                    }
                }
                else
                {
                    ItemList[sortData.index, typeColumn] = item;
                }
                PrintInventory(playerElseStorage);
                return true;
            }
            PrintInventory(playerElseStorage);
            return false;
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
                new LootTile("Loot Pile",
                    new List<Item>()
                    {
                        dropItem
                    },
                    false, true));
                if (sortData.isPlayerInventory)
                {
                    PlayerEntity.Instance.Inventory.ItemList[sortData.index, sortData.typeColumn] = null;
                }
                else
                {
                    ItemList[sortData.index, sortData.typeColumn] = null;
                }
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

        private int CheckForEmptySlot(bool playerElseStorage, int typeColumn)
        {
            for (int i = 0; i < Row; i++)
            {
                if (playerElseStorage)
                {
                    if (PlayerEntity.Instance.Inventory.ItemList[i, typeColumn] == null)
                    {
                        return i;
                    }
                }
                else
                {
                    if (PlayerEntity.Instance.Inventory.ItemList[i, typeColumn] == null)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        private void InventoryHighlight(bool playerElseStorage, int index, int typeColumn, int typeOffset)
        {
            Item highlightItem = ItemList[index, typeColumn];
            for (int y = 1; y <= Row; y++)
            {
                if (playerElseStorage)
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

        public void WeaponAffinityCheck(WeaponItem weapon)
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

        public void ArmourAffinityCheck(ArmourItem armour)
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
