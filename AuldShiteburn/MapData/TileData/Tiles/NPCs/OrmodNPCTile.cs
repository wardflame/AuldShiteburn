using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    [Serializable]
    class OrmodNPCTile : NPCTile
    {
        public override string NPCName => "Ormod";
        public override ConsoleColor Foreground => stage1 ? ConsoleColor.Cyan : ConsoleColor.DarkCyan;
        private List<InteractionData> stage1Interactions = new List<InteractionData>();
        private List<InteractionData> stage2Interactions = new List<InteractionData>();
        private bool stage1 = false;

        public OrmodNPCTile() : base("%%")
        {
        }

        protected override void Interaction()
        {
            Map.Instance.ClearInteractInterface();
            if (!stage1)
            {
                stage1 = CycleInteraction(stage1Interactions, "Hmph. All right.");
            }
            else
            {
                CycleInteraction(stage2Interactions);
            }
        }

        protected override void InitLines()
        {
            stage1Interactions.Add(new InteractionData(Description("Before you sits a dirty, lowly man in sackcloth. He is crestfallen and frail.")));
            stage1Interactions.Add(new InteractionData(Dialogue($"Thou comst to Shiteburn...cursed plot of filth. I am {NPCName}, a...umm...-")));
            stage1Interactions.Add(new InteractionData(Dialogue("All's become vague. I canst not find escape from this place. Please...would you help me?"), true));
            stage1Interactions.Add(new InteractionData(Dialogue("Bless you, stranger. I had a friend, Earh. He and I were separated in the Residents.")));
            stage1Interactions.Add(new InteractionData(Dialogue("If you head east, you should find it. Beware, unimaginable filth roams this village.")));
            stage1Interactions.Add(new InteractionData(Dialogue("Journey this place. Seek the heart, the Foulstench. Rid us of this burden...")));
            stage2Interactions.Add(new InteractionData(Dialogue("Unimaginable filth that roams this village.")));
            stage2Interactions.Add(new InteractionData(Dialogue("Journey this place. Seek the heart, the Foulstench. Rid us of this burden...")));
        }
    }
}
