using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs.NarrationNPCs
{
    [Serializable]
    internal class FoulstenchNarrationNPCTile : NPCTile
    {
        public override string NPCName => "Foulstench";
        public override bool Collidable => false;
        public override ConsoleColor Foreground => ConsoleColor.Black;
        private bool CutsceneFinished { get; set; } = false;
        private List<InteractionData> foulstenchBossInterlude = new List<InteractionData>();
        private List<InteractionData> foulstenchBossIntroOrmodTrue = new List<InteractionData>();
        private List<InteractionData> foulstenchBossIntroOrmodFalse = new List<InteractionData>();

        public FoulstenchNarrationNPCTile() : base(FinalArenaTile.DisplayChar)
        {
        }

        public override void Interaction()
        {
            if (!CutsceneFinished)
            {
                CycleInteraction(foulstenchBossInterlude);
                if (PlayerEntity.Instance.TookFromOrmod)
                {
                    CutsceneFinished = CycleInteraction(foulstenchBossIntroOrmodTrue);
                }
                else
                {
                    CutsceneFinished = CycleInteraction(foulstenchBossIntroOrmodFalse);
                }
            }
        }

        protected override void InitLines()
        {
            foulstenchBossInterlude.Add(new InteractionData(Description($"As you near the heart of the stench, all grows dim.")));
            foulstenchBossInterlude.Add(new InteractionData(Description($"The moon is choked and tucked away behind dense, dark clouds.")));
            foulstenchBossInterlude.Add(new InteractionData(Description($"Hardly able to see, you wade into the shite and push on.")));
            foulstenchBossInterlude.Add(new InteractionData(Description($"From the darkness, a low resonance shakes you to your core.")));
            foulstenchBossInterlude.Add(new InteractionData(Description($"Straining, you make out a shape, not man, nor beast, something...else.")));
            foulstenchBossIntroOrmodTrue.Add(new InteractionData(Description($"You are alone. It is coming.")));
            foulstenchBossIntroOrmodFalse.Add(new InteractionData(Description($"You hear the otherworldly creature wading through the shite, heavy footfalls plunging.")));
            foulstenchBossIntroOrmodFalse.Add(new InteractionData(Description($"And then, like a flash of lightning, moonlight pierces the failing veil!")));
            foulstenchBossIntroOrmodFalse.Add(new InteractionData(Description($"The beast, whilst terrifying, is itself shocked and off-guard!")));
            foulstenchBossIntroOrmodFalse.Add(new InteractionData(Description($"Elatha is with you! Strike!")));
        }
    }
}
