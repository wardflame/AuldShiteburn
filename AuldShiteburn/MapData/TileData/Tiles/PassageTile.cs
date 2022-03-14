using System;
using System.Collections.Generic;
using System.Text;
using AuldShiteburn.EntityData;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    class PassageTile : Tile
    {
        public Direction Direction { get; set; }

        public PassageTile() : base("-", true)
        {
        }

        public override void OnCollision(Entity entity, Area area)
        {
            if (entity is PlayerEntity)
            {
                Map.Instance.MoveArea(Direction);
                Map.Instance.PrintAreaName();
            }
        }
    }
}
