﻿using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    [Serializable]
    class OrmodNPCTile : NPCTile
    {
        public override string NPCName => "Ormod";
        private List<InteractionData> stage1EarhRequest = new List<InteractionData>();
        private List<InteractionData> stage1Interim = new List<InteractionData>();
        private bool stage1 = false;

        public OrmodNPCTile() : base("%")
        {
        }

        public override void Interaction()
        {
            Utils.ClearInteractInterface();
            if (!stage1)
            {
                stage1 = CycleInteraction(stage1EarhRequest, "Hmph. All right.");
            }
            else
            {
                CycleInteraction(stage1Interim);
            }
        }

        protected override void InitLines()
        {
            stage1EarhRequest.Add(new InteractionData(Description("Before you sits a dirty, slouched man in sackcloth. He is crestfallen and frail.")));
            stage1EarhRequest.Add(new InteractionData(Dialogue($"Thou comst to Shiteburn...cursed plot of filth. I am {NPCName}, a...umm...-")));
            stage1EarhRequest.Add(new InteractionData(Dialogue("I cannot remember much now. All is vague and I am lost. Please...would you help me?"), true));
            stage1EarhRequest.Add(new InteractionData(Dialogue("Bless you, stranger. I had a friend, Earh. He and I were separated in the Living Quarter.")));
            stage1EarhRequest.Add(new InteractionData(Dialogue("You should find it in the East. Do be wary; unimaginable filth roams this village.")));
            stage1EarhRequest.Add(new InteractionData(Dialogue("To leave this cell, you'll need the key. It's on that dead heathen opposite us.")));
            stage1Interim.Add(new InteractionData(Dialogue("You should find the Living Quarter in the East, but be wary; unimaginable filth roams this village.")));
            stage1Interim.Add(new InteractionData(Dialogue("To leave this cell, you'll need the key. It's on that dead heathen across the room.")));
            stage1Interim.Add(new InteractionData(Description("Ormod looks at the ground and mumbles to himself, clutching an amulet in his fist.")));
        }
    }
}
