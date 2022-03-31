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

        public StorageTile(string displayChar, bool collidable, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black) : base(displayChar, collidable, foreground, background)
        {
        }

        public override void OnCollision(Entity entity)
        {
            Utils.ClearInteractInterface();
            Storage.PrintInventory(Inventory.INTERACT_WEAPON_OFFSET, Inventory.INTERACT_ARMOUR_OFFSET, Inventory.INTERACT_CONSUMABLE_OFFSET, Inventory.INTERACT_KEY_OFFSET);
        }
    }
}
