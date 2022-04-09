using AuldShiteburn.CombatData;
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

namespace AuldShiteburn.EntityData.PlayerData
{
    [Serializable]
    internal class Inventory
    {
        private static int ColumnOffset { get; } = 36;
        public static int WeaponOffset { get; } = 0;
        public static int ArmourOffset { get; } = WeaponOffset + ColumnOffset;
        public static int ConsumableOffset { get; } = ArmourOffset + ColumnOffset;
        public static int KeyOffset { get; } = ConsumableOffset + ColumnOffset;

        public string Name { get; }
        public Item[,] ItemList { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Inventory(string name, int row, int column)
        {
            Name = name;
            Row = row;
            Column = column;
            ItemList = new Item[Row, Column];
        }

        /// <summary>
        /// When interacting with a storage tile, this prints both inventories and allows for
        /// the navigation of one or the other by pressing TAB and using a switch case.
        /// </summary>
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
                            Utils.ClearPlayerInventoryInterface();
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
                Utils.ClearInteractArea(-1, 33);
            }
        }

        /// <summary>
        /// Separate the items in ItemList into their type columns and print them down.
        /// </summary>
        /// <param name="playerElseStorage">True if we want to print the inventory in the player's area, else in interact.</param>
        public void PrintInventory(bool playerElseStorage)
        {
            int offset = 0;
            if (!playerElseStorage)
            {
                Utils.SetCursorInteract();
                Utils.WriteColour(Name, ConsoleColor.DarkYellow);
            }
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
                        Utils.SetCursorInteract(1, offsetX: WeaponOffset);
                    }
                    offset = WeaponOffset;
                    Utils.WriteColour("[");
                    Utils.WriteColour("WEAPONS", ConsoleColor.DarkYellow);
                    Utils.WriteColour("]");
                }
                if (x == 1)
                {
                    if (playerElseStorage)
                    {
                        Utils.SetCursorInventory(offsetX: ArmourOffset);
                    }
                    else
                    {
                        Utils.SetCursorInteract(1, offsetX: ArmourOffset);
                    }
                    offset = ArmourOffset;
                    Utils.WriteColour("[");
                    Utils.WriteColour("ARMOUR", ConsoleColor.DarkYellow);
                    Utils.WriteColour("]");
                }
                if (x == 2)
                {
                    if (playerElseStorage)
                    {
                        Utils.SetCursorInventory(offsetX: ConsumableOffset);
                    }
                    else
                    {
                        Utils.SetCursorInteract(1, offsetX: ConsumableOffset);
                    }
                    offset = ConsumableOffset;
                    Utils.WriteColour("[");
                    Utils.WriteColour("CONSUMABLES", ConsoleColor.DarkYellow);
                    Utils.WriteColour("]");
                }
                if (x == 3)
                {
                    if (playerElseStorage)
                    {
                        Utils.SetCursorInventory(offsetX: KeyOffset);
                    }
                    else
                    {
                        Utils.SetCursorInteract(1, offsetX: KeyOffset);
                    }
                    offset = KeyOffset;
                    Utils.WriteColour("[");
                    Utils.WriteColour("KEY ITEMS", ConsoleColor.DarkYellow);
                    Utils.WriteColour("]");
                }
                for (int y = 1; y <= Row; y++)
                {
                    if (playerElseStorage)
                    {
                        Utils.SetCursorInventory(y, offset);
                    }
                    else
                    {
                        Utils.SetCursorInteract(y + 1, offset);
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

        /// <summary>
        /// Print an inventory and allow the player to navigate it, highlighting the item at a player-navigable index.
        /// If the player presses enter, return the location of the item as sort data to be processed.
        /// </summary>
        /// <param name="playerElseStorage">If true, interact with player inventory area of window, else external storage.</param>
        /// <param name="typeColumn">Type column to highlight.</param>
        /// <param name="typeOffset">CursorLeft offset to display a highlighted index.</param>
        /// <returns></returns>
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
                                    Utils.ClearPlayerInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract(1);
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
                                    Utils.ClearPlayerInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract(1);
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
                                typeOffset -= ColumnOffset;
                                if (playerElseStorage)
                                {
                                    Utils.ClearPlayerInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract(1);
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
                                typeOffset += ColumnOffset;
                                if (playerElseStorage)
                                {
                                    Utils.ClearPlayerInventoryInterface();
                                    Utils.SetCursorInventory();
                                }
                                else
                                {
                                    Utils.ClearInteractInterface();
                                    Utils.SetCursorInteract(1);
                                }
                                PrintInventory(playerElseStorage);
                                InventoryHighlight(playerElseStorage, index, typeColumn, typeOffset);
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            Utils.ClearPlayerInventoryInterface();
                            PrintInventory(playerElseStorage);
                            sortData.index = -1;
                            return sortData;
                        }
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
                }
            } while (InputSystem.InputKey != ConsoleKey.Enter);
            sortData.index = index;
            sortData.typeColumn = typeColumn;
            sortData.typeOffset = typeOffset;
            sortData.isPlayerInventory = playerElseStorage;
            sortData.transferIntended = true;
            return sortData;
        }

        /// <summary>
        /// After selecting an item from an inventory, prompt player to choose what to do
        /// with it. Pressing T will allow the player to transfer the item to a slot in
        /// the other inventory.
        /// </summary>
        /// <param name="sortData">Location of the item to move.</param>
        /// <param name="secondaryInventory">The secondary inventory we're engaging with.</param>
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
                Utils.WriteColour("(T) Transfer to Inventory");
                Utils.SetCursorInventory(-2, 50);
                Utils.WriteColour("(Backspace) Cancel");
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

        /// <summary>
        /// Display important characteristics about the item depending on type
        /// in the interact area and offer the player different prompts to interact
        /// with the item. Run the OnInventoryUse() method if they choose E, drop the
        /// item into a LootTile with D or cancel with Backspace.
        /// </summary>
        /// <param name="playerElseStorage">True if we want to navigate the player inventory, else storage inventory.</param>
        public void PlayerItemInteract(bool playerElseStorage)
        {
            bool interacting = true;
            while (interacting)
            {
                Utils.ClearInteractInterface();
                int offset = 2;
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
                Utils.WriteColour($"What do you want to do with {currentItem.Name}?");
                if (currentItem is WeaponItem)
                {
                    Utils.SetCursorInteract(1);
                    WeaponItem weapon = (WeaponItem)currentItem;
                    Utils.WriteColour("Physical Damage: ");
                    Utils.WriteColour($"{weapon.MinPhysDamage} - {weapon.MaxPhysDamage}");
                    Utils.SetCursorInteract(2);
                    Utils.WriteColour("Property Damage: ");
                    Utils.WriteColour($"{weapon.MinPropDamage} - {weapon.MaxPropDamage}");
                    Utils.SetCursorInteract(3);
                    Utils.WriteColour("(E) Equip Weapon");
                    offset = 4;
                }
                else if (currentItem is ArmourItem)
                {
                    Utils.SetCursorInteract(1);
                    ArmourItem armour = (ArmourItem)currentItem;
                    Utils.WriteColour("Physical Mitigation: ");
                    Utils.WriteColour($"{armour.PhysicalMitigation}", ConsoleColor.Yellow);
                    Utils.SetCursorInteract(2);
                    Utils.WriteColour("Property Mitigation: ");
                    Utils.WriteColour($"{armour.PropertyMitigation}", ConsoleColor.Magenta);
                    Utils.SetCursorInteract(3);
                    Utils.WriteColour("(E) Equip Armour");
                    offset = 4;
                }
                else if (currentItem is ConsumableItem || currentItem.GetType().IsSubclassOf(typeof(ConsumableItem)))
                {
                    ConsumableItem consumable = (ConsumableItem)currentItem;
                    Utils.SetCursorInteract(1);
                    Utils.WriteColour($@"'{consumable.Description}'", ConsoleColor.DarkYellow);
                    Utils.SetCursorInteract(2);
                    Utils.WriteColour("Stock: ");
                    Utils.WriteColour($"{consumable.Stock}", ConsoleColor.DarkYellow);
                    Utils.SetCursorInteract(3);
                    Utils.WriteColour("(E) Consume");
                    offset = 4;
                }
                else if (currentItem is KeyItem)
                {
                    Utils.SetCursorInteract(1);
                    KeyItem key = (KeyItem)currentItem;
                    Utils.WriteColour($@"'{key.Description}'", ConsoleColor.DarkYellow);
                    offset = 2;
                }
                Utils.SetCursorInteract(offset);
                Utils.WriteColour("(D) Drop Item");
                Utils.SetCursorInteract(offset + 1);
                Utils.WriteColour("(Backspace) Cancel");
                bool choosing = true;
                while (choosing)
                {
                    InputSystem.GetInput();
                    switch (InputSystem.InputKey)
                    {
                        case ConsoleKey.E:
                            {
                                currentItem.OnInventoryUse(sortData);
                                if (currentItem.GetType() == typeof(ConsumableItem) || currentItem.GetType().IsSubclassOf(typeof(ConsumableItem)))
                                {
                                    ConsumableItem consumable = (ConsumableItem)currentItem;
                                    if (consumable.Stock <= 0)
                                    {
                                        PlayerEntity.Instance.Inventory.ItemList[sortData.index, sortData.typeColumn] = null;
                                    }
                                }
                                PlayerEntity.Instance.PrintStats();
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
        /// Take an item and navigate the player or storage tile inventory for a desired
        /// slot and place the item there. If there are no slots available, offer the
        /// player to drop the item into a new loot tile.
        /// </summary>
        /// <param name="item">Item to add to inventory.</param>
        /// <param name="playerElseStorage">If true, player inventory, else storage tile inventory.</param>
        /// <returns>Whether the item was added/moved from the original inventory.</returns>
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
                    if (item.GetType() == typeof(ConsumableItem) || item.GetType().IsSubclassOf(typeof(ConsumableItem)))
                    {
                        for (int i = 0; i < Row; i++)
                        {
                            if (ItemList[i, 2] != null && ItemList[i, 2].Name == item.Name)
                            {
                                ConsumableItem itemToAdd = (ConsumableItem)item;
                                ConsumableItem target = (ConsumableItem)ItemList[i, 2];
                                target.Stock += itemToAdd.Stock;
                                return true;
                            }
                        }
                    }
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
                    if (item.GetType() == typeof(ConsumableItem) || item.GetType().IsSubclassOf(typeof(ConsumableItem)))
                    {
                        for (int i = 0; i < Row; i++)
                        {
                            if (ItemList[i, 2] != null && ItemList[i, 2].Name == item.Name)
                            {
                                ConsumableItem itemToAdd = (ConsumableItem)item;
                                ConsumableItem target = (ConsumableItem)ItemList[i, 3];
                                target.Stock += itemToAdd.Stock;
                                return true;
                            }
                        }
                    }
                    ItemList[sortData.index, typeColumn] = item;
                }
                Utils.ClearPlayerInventoryInterface();
                PrintInventory(playerElseStorage);
                return true;
            }
            Utils.ClearPlayerInventoryInterface();
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
        private bool DropItem(Item dropItem, InventorySortData sortData)
        {
            Utils.SetCursorInventory(-1);
            Utils.ClearLine(60);
            Utils.SetCursorInventory(-1);
            Utils.WriteColour($"Dropped {dropItem.Name} on the floor.", ConsoleColor.Red);
            Tile currentTile = Map.Instance.CurrentArea.GetTile(EntityData.PlayerEntity.Instance.PosX, EntityData.PlayerEntity.Instance.PosY);
            if (currentTile is LootTile)
            {
                LootTile tile = (LootTile)currentTile;
                tile.Items.Add(dropItem);
                if (sortData.isPlayerInventory)
                {
                    PlayerEntity.Instance.Inventory.ItemList[sortData.index, sortData.typeColumn] = null;
                }
                else
                {
                    ItemList[sortData.index, sortData.typeColumn] = null;
                }
            }
            else
            {
                LootTile.GenerateLootTile(false, new List<Item>() { dropItem });
                if (sortData.isPlayerInventory)
                {
                    PlayerEntity.Instance.Inventory.ItemList[sortData.index, sortData.typeColumn] = null;
                }
                else
                {
                    ItemList[sortData.index, sortData.typeColumn] = null;
                }
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
            else if (item.GetType() == typeof(ConsumableItem) || item.GetType().IsSubclassOf(typeof(ConsumableItem)))
            {
                return 2;
            }
            else if (item.GetType() == typeof(KeyItem))
            {
                return 3;
            }
            return 0;
        }

        /// <summary>
        /// Check which type the item is and return a CursorLeft offset accordingly.
        /// </summary>
        /// <param name="item">Item whose type we want to check.</param>
        /// <returns>CursorLeft offset we need.</returns>
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
            else if (item.GetType() == typeof(ConsumableItem) || item.GetType().IsSubclassOf(typeof(ConsumableItem)))
            {
                return ConsumableOffset;
            }
            else if (item.GetType() == typeof(KeyItem))
            {
                return KeyOffset;
            }
            return 0;
        }

        /// <summary>
        /// Iterate through the player or storage inventory for an empty slot in the
        /// desired type column. Return an index if there is an empty slot.
        /// </summary>
        /// <param name="playerElseStorage">If true, player inventory, else storage tile inventory.</param>
        /// <param name="typeColumn">Type column of item to find a slot for.</param>
        /// <returns></returns>
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
                    if (ItemList[i, typeColumn] == null)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Iterate through key items in this inventory and return true if the key
        /// passed in has the same name.
        /// </summary>
        /// <param name="requiredKey">Key we wanted to look for.</param>
        /// <returns>Returns true if requiredKey matches a key in this inventory.</returns>
        public bool CheckForKey(KeyItem requiredKey)
        {
            for (int i = 0; i < Row; i++)
            {
                if (ItemList[i, 3] != null)
                {
                    if (ItemList[i, 3].Name == requiredKey.Name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// When highlighting an item when iterate through the inventory, if it happens to
        /// meet the index we want, highlight it depending on the item type.
        /// </summary>
        /// <param name="playerElseStorage">If true, player inventory, else storage tile inventory.</param>
        /// <param name="index">Index to highlight.</param>
        /// <param name="typeColumn">Type column of item.</param>
        /// <param name="typeOffset">Which CursorLeft offset to print at.</param>
        private void InventoryHighlight(bool playerElseStorage, int index, int typeColumn, int typeOffset)
        {
            for (int y = 1; y <= Row; y++)
            {
                if (playerElseStorage)
                {
                    Utils.SetCursorInventory(y, typeOffset);
                }
                else
                {
                    Utils.SetCursorInteract(y + 1, typeOffset);
                }
                if (ItemList[y - 1, typeColumn] != null)
                {
                    if (y - 1 == index)
                    {
                        Utils.WriteColour(">>", ConsoleColor.Yellow);
                        if (ItemList[y - 1, typeColumn] is WeaponItem)
                        {
                            PrintWeaponWithAffinity((WeaponItem)ItemList[y - 1, typeColumn]);
                        }
                        else if (ItemList[y - 1, typeColumn] is ArmourItem)
                        {
                            PrintArmourWithAffinity((ArmourItem)ItemList[y - 1, typeColumn]);
                        }
                        else
                        {
                            Utils.WriteColour($"{ItemList[y - 1, typeColumn].Name}", ConsoleColor.Cyan);
                        }
                    }
                    else
                    {
                        Utils.WriteColour($"{ItemList[y - 1, typeColumn].Name}");
                    }
                }
                else
                {
                    if (y - 1 == index)
                    {
                        Utils.WriteColour(">>", ConsoleColor.Yellow);
                    }
                    Utils.WriteColour($"--");
                }
            }
        }

        /// <summary>
        /// Check the different parts of a weapon and print the part in green
        /// if the player class is proficient with that part.
        /// </summary>
        /// <param name="weapon">Weapon to check affinities on.</param>
        public void PrintWeaponWithAffinity(WeaponItem weapon)
        {
            if (weapon != null)
            {
                ConsoleColor proficient = ConsoleColor.Gray;
                if (weapon.Property.Type != PropertyDamageType.Standard)
                {
                    if (weapon.Property.HasAffinity)
                    {
                        proficient = ConsoleColor.DarkGreen;
                    }
                    Utils.WriteColour($"{weapon.Property.Name} ", proficient);
                    proficient = ConsoleColor.Gray;
                }

                if (weapon.Material.HasAffinity)
                {
                    proficient = ConsoleColor.DarkGreen;
                }
                Utils.WriteColour($"{weapon.Material.Name} ", proficient);
                proficient = ConsoleColor.Gray;

                if (weapon.Type.IsProficient)
                {
                    proficient = ConsoleColor.DarkGreen;
                }
                Utils.WriteColour($"{weapon.Type.Name} ", proficient);
            }
            else
            {
                Utils.WriteColour("--", ConsoleColor.DarkGray);
            }
        }

        /// <summary>
        /// If the player character is fully proficient with the armour, print it green.
        /// </summary>
        /// <param name="armour">Armour in question.</param>
        public void PrintArmourWithAffinity(ArmourItem armour)
        {
            if (armour != null)
            {
                if (armour.IsProficient)
                {
                    Utils.WriteColour($"{armour.Name} ", ConsoleColor.DarkGreen);
                }
                else
                {
                    Utils.WriteColour($"{armour.Name} ", ConsoleColor.Red);
                }
            }
            else
            {
                Utils.WriteColour("--", ConsoleColor.DarkGray);
            }
        }
    }
}
