using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs.NarrationNPCs
{
    internal class GraveyardNarrationNPCTile : NPCTile
    {
        public override string NPCName => "Graveyard";
        public override bool Collidable => false;
        public override ConsoleColor Foreground => ConsoleColor.Black;
        private bool CutsceneFinished { get; set; } = false;
        private List<InteractionData> graveyardBossIntro = new List<InteractionData>();

        public GraveyardNarrationNPCTile() : base(" ")
        {
        }

        public override void Interaction()
        {
            if (!CutsceneFinished)
            {
                CutsceneFinished = CycleInteraction(graveyardBossIntro);
            }
        }

        protected override void InitLines()
        {
            graveyardBossIntro.Add(new InteractionData(Description($"Wet sounds slither through the air. Low crumples and crunches follow.")));
            graveyardBossIntro.Add(new InteractionData(Description($"A robed figure lurches over a dark altar, not unlike the one in Shitebreach. Shite drips from the stone.")));
            graveyardBossIntro.Add(new InteractionData(Description($"'Another has come for sacrifice?' the robed figure says, turning slowly to you.")));
            graveyardBossIntro.Add(new InteractionData(Description($"Their mangled artistry is laid bare; a mutilated corpse lies strewn across the alter, bowels torn open.")));
            graveyardBossIntro.Add(new InteractionData(Description($"'You cannot stop this. Surrender yourself, your soul...'")));
        }
    }
}
