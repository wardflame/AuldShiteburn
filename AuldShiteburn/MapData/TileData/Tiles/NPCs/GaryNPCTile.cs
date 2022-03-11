using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    class GaryNPCTile : NPCTile
    {
        public override string NPCName => "Ethulwulf";
        public GaryNPCTile() : base("&&")
        {
        }

        protected override void Interaction(Area area)
        {
            Narration("Before you sits a dirty, lowly man in sackcloth.", area);
            Dialogue("Welcomen, fellow. Thou comst to Shiteburn, cursed plot of filth.", area, 3);
        }
    }
}
