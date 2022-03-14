using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    abstract class NPCTile : Tile
    {
        public abstract string NPCName { get; }
        protected NPCTile(string displayChar) : base(displayChar, true)
        {
        }

        protected abstract void Interaction(Area area);
        public override void OnCollision(Entity entity, Area area)
        {
            if (entity is PlayerEntity player)
            {
                player.inMenu = true;
                Interaction(area);
                player.inMenu = false;
            }
        }

        public void Narration(string narration, Area area, int offsetY = 2)
        {
            Console.CursorLeft = (area.Width * 2) + 2;
            Console.CursorTop = offsetY;
            Console.Write(narration);
        }

        public void Dialogue(string dialogue, Area area, int offsetY = 2)
        {
            Narration($"{NPCName}: {dialogue}", area, offsetY);
        }
    }
}
