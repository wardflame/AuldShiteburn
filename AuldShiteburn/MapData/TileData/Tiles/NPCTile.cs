using AuldShiteburn.EntityData;
using System;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    abstract class NPCTile : Tile
    {
        public abstract string NPCName { get; }
        protected NPCTile(string displayChar) : base(displayChar, true)
        {
        }

        protected abstract void Interaction();
        public override void OnCollision(Entity entity)
        {
            if (entity is PlayerEntity player)
            {
                player.inMenu = true;
                Interaction();
                player.inMenu = false;
            }
        }

        public void Narration(string narration, int offsetY = 2)
        {
            Console.CursorLeft = (Map.Instance.CurrentArea.Width * 2) + 2;
            Console.CursorTop = offsetY;
            Console.Write(narration);
        }

        public void Dialogue(string dialogue, int offsetY = 2)
        {
            Narration($"{NPCName}: {dialogue}", offsetY);
        }
    }
}
