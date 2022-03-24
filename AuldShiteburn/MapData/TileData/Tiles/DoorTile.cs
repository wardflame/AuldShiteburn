using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData;
using AuldShiteburn.ItemData.KeyData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    internal class DoorTile : Tile
    {
        public override string DisplayChar => Locked ? "X": ".";
        public override bool Collidable => Locked;
        public override ConsoleColor Foreground => ConsoleColor.DarkYellow;
        public bool Locked { get; set; }
        public KeyItem key;

        public DoorTile(bool locked, KeyItem key = null) : base("", true)
        {
            this.Locked = locked;
            this.key = key;
        }

        public override void OnCollision(Entity entity)
        {
            if (entity is PlayerEntity)
            {
                if (key != null && Locked)
                {
                    foreach (Item item in PlayerEntity.Instance.inventory)
                    {
                        if (item == key)
                        {
                            Locked = false;                            
                            break;
                        }
                    }
                    if (Locked)
                    {
                        Utils.InteractPrompt("It's locked.");
                    }
                }
            }
        }
    }
}
