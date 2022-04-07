using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData.KeyData;
using System;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    internal class DoorTile : Tile
    {
        public override string DisplayChar => Locked ? "X" : ".";
        public override bool Collidable => Locked;
        public override ConsoleColor Foreground => ConsoleColor.DarkYellow;
        public bool Locked { get; set; }
        public KeyItem Key { get; }

        public DoorTile(bool locked, KeyItem key = null) : base("", true)
        {
            Locked = locked;
            this.Key = key;
        }

        public override void OnCollision(Entity entity)
        {
            if (entity is PlayerEntity)
            {
                if (Key != null && Locked)
                {
                    Utils.ClearInteractInterface();
                    Utils.SetCursorInteract();
                    if (PlayerEntity.Instance.Inventory.CheckForKey(Key))
                    {
                        Utils.WriteColour($"Unlocked door with {Key.Name}.", ConsoleColor.DarkYellow);
                        Utils.SetCursorInteract(2);
                        Utils.WriteColour(" Press any key to continue.");
                        Console.ReadKey(true);
                        Locked = false;
                    }
                    if (Locked)
                    {
                        Utils.WriteColour($"It's locked.", ConsoleColor.Red);
                    }
                }
            }
        }
    }
}
