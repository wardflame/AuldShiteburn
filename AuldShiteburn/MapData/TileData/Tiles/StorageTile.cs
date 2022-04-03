﻿using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.ItemData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    internal class StorageTile : Tile
    {
        public Inventory Storage { get; set; }

        public StorageTile() : base("!", true, ConsoleColor.Cyan, ConsoleColor.Black)
        {
            Storage = new Inventory(16, 4);
        }

        public override void OnCollision(Entity entity)
        {
            PlayerEntity.Instance.InMenu = true;
            Utils.ClearInteractInterface();
            Utils.SetCursorInteract();
            Storage.EngageStorage();
            PlayerEntity.Instance.Inventory.PrintInventory(true);
            PlayerEntity.Instance.InMenu = false;
        }
    }
}