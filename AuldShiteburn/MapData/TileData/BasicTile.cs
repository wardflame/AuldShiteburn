using AuldShiteburn.EntityData;
using System;

namespace AuldShiteburn.MapData.TileData
{
    [Serializable]
    class BasicTile : Tile
    {
        public BasicTile(string displayChar, bool collidable, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black) : base(displayChar, collidable, foreground, background)
        {
        }

        public override void OnCollision(Entity entity)
        {
        }
    }
}
