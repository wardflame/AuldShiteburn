using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using System;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    internal class StorageTile : Tile
    {
        public Inventory Storage { get; set; }

        public StorageTile(string name) : base("!", true, ConsoleColor.Cyan, ConsoleColor.Black)
        {
            Storage = new Inventory(name, 16, 4);
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
