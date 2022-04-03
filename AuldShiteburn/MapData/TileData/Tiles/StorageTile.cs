using AuldShiteburn.EntityData;
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

        public StorageTile() : base("*", true, ConsoleColor.DarkYellow, ConsoleColor.Black)
        {
            Storage = new Inventory(12, 4);
        }

        public override void OnCollision(Entity entity)
        {
            Utils.ClearInteractInterface();
            Utils.SetCursorInteract();
            Storage.PrintInventory(false);
        }
    }
}
