using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    internal abstract class NPCTile : InteractionTile
    {
        public NPCTile(string displayChar) : base(displayChar)
        {
        }

        protected override void InitLines()
        {
        }

        protected override void Interaction()
        {
        }
    }
}
