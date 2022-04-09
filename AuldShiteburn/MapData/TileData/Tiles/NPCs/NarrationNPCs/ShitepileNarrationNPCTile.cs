using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs.NarrationNPCs
{
    internal class ShitepileNarrationNPCTile : NPCTile
    {
        public override string NPCName => "Shitepile";
        public override bool Collidable => false;
        public override ConsoleColor Foreground => ConsoleColor.DarkRed;
        private bool CutsceneFinished { get; set; } = false;
        private List<InteractionData> shitepileBossIntro = new List<InteractionData>();

        public ShitepileNarrationNPCTile() : base(" ")
        {
        }

        public override void Interaction()
        {
            if (!CutsceneFinished)
            {
                CutsceneFinished = CycleInteraction(shitepileBossIntro);
            }
        }

        protected override void InitLines()
        {
            shitepileBossIntro.Add(new InteractionData(Description($"A large pile of burning shite illuminates the muddy waste ahead.")));
            shitepileBossIntro.Add(new InteractionData(Description($"Crouching before the flames is a twisted figure, now scarcely like a man.")));
            shitepileBossIntro.Add(new InteractionData(Description($"It shovels burning shite into its mouth. Its cuirass gleans against the flames.")));
            shitepileBossIntro.Add(new InteractionData(Description($"As if noticing you by some other sense, it turns slowly and stares.")));
            shitepileBossIntro.Add(new InteractionData(Description($"For a brief moment, moonlight breaches the thick, dark clouds overhead.")));
            shitepileBossIntro.Add(new InteractionData(Description($"The creature turns to the waning moonlight and howls like a grieving hound.")));
        }
    }
}
