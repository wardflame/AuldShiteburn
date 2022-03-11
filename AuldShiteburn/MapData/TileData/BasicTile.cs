using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData
{
    class BasicTile : Tile
    {
        public BasicTile(string displayChar, bool collidable) : base(displayChar, collidable)
        {
        }

        public override void OnCollision(Entity entity, Area area)
        {
        }
    }
}
