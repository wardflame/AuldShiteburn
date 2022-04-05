using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData;
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
        public KeyItem key;

        public DoorTile(bool locked, KeyItem key = null) : base("", true)
        {
            Locked = locked;
            this.key = key;
        }

        public override void OnCollision(Entity entity)
        {
            if (entity is PlayerEntity)
            {
                if (key != null && Locked)
                {
                    Utils.ClearInteractInterface();
                    Utils.SetCursorInteract();
                    for (int i = 0; i < PlayerEntity.Instance.Inventory.Row; i++)
                    {
                        if (PlayerEntity.Instance.Inventory.ItemList[i, 3] != null)
                        {
                            if (PlayerEntity.Instance.Inventory.ItemList[i, 3].Name == key.Name)
                            Locked = false;
                            Utils.WriteColour($"Unlocked door with {key.Name}.", ConsoleColor.DarkYellow);
                            Console.Write(" Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    }
                    if (Locked)
                    {
                        Utils.WriteColour($"It's locked.", ConsoleColor.DarkRed);
                    }
                }
            }
        }
    }
}
