using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData.KeyData;
using AuldShiteburn.MapData.AreaData.Areas;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    [Serializable]
    class OrmodNPCTile : NPCTile
    {
        public override string NPCName => "Ormod";
        private List<InteractionData> earhRequest = new List<InteractionData>();
        private List<InteractionData> questInterim = new List<InteractionData>();
        private List<InteractionData> thanksForEarh = new List<InteractionData>();
        private List<InteractionData> amuletTaken = new List<InteractionData>();
        private List<InteractionData> amuletRefused = new List<InteractionData>();
        private List<InteractionData> returnedAmulet = new List<InteractionData>();
        private List<InteractionData> afterAmulet = new List<InteractionData>();
        private bool earhQuest = false;
        private bool amuletOffered = false;
        private bool tookAmulet = false;

        public OrmodNPCTile() : base("%")
        {
        }

        public override void Interaction()
        {
            StartArea shitebreach = (StartArea)Map.Instance.ActiveAreas[Map.Instance.GetIndex(0, 0)];
            Utils.ClearInteractInterface();
            bool earhReturned = shitebreach.GetTile(5, 1).GetType() == typeof(EarhNPCTile);
            if (!earhReturned)
            {
                if (!earhQuest)
                {
                    earhQuest = CycleInteraction(earhRequest, "Hmph. All right.");
                }
                else
                {
                    CycleInteraction(questInterim);
                }
            }
            else
            {
                if (!amuletOffered)
                {
                    tookAmulet = CycleInteraction(thanksForEarh);
                    if (tookAmulet)
                    {
                        PlayerEntity.Instance.Inventory.AddItem(KeyItem.OrmodsAmulet, true);
                        PlayerEntity.Instance.TookFromOrmod = true;
                        CycleInteraction(amuletTaken);
                    }
                    else
                    {
                        CycleInteraction(amuletRefused);
                    }
                    amuletOffered = true;
                }
                else
                {
                    if (PlayerEntity.Instance.TookFromOrmod && PlayerEntity.Instance.CarryingOrmodsAmulet)
                    {
                        if (Utils.VerificationQuery("Return Ormod's amulet?", ConsoleColor.Cyan))
                        {
                            for (int i = 0; i < PlayerEntity.Instance.Inventory.Row; i++)
                            {
                                if (PlayerEntity.Instance.Inventory.ItemList[i, 3].Name == KeyItem.OrmodsAmulet.Name)
                                {
                                    PlayerEntity.Instance.Inventory.ItemList[i, 3] = null;
                                    PlayerEntity.Instance.PrintInventory();
                                }
                            }
                            CycleInteraction(returnedAmulet);
                            PlayerEntity.Instance.TookFromOrmod = false;
                            return;
                        }
                    }
                    CycleInteraction(afterAmulet);
                }
            }
        }

        protected override void InitLines()
        {
            earhRequest.Add(new InteractionData(Description("Before you sits a dirty, slouched man in sackcloth. He is crestfallen and frail.")));
            earhRequest.Add(new InteractionData(Dialogue($"Thou comst to Shiteburn...cursed plot of filth. I am {NPCName}, a...umm...-")));
            earhRequest.Add(new InteractionData(Dialogue("I cannot remember much now. All is vague and I am lost. Please...would you help me?"), true));
            earhRequest.Add(new InteractionData(Dialogue("Bless you, stranger. I had a friend, Earh. He and I were separated in the Living Quarter.")));
            earhRequest.Add(new InteractionData(Dialogue("You should find it in the East. Do be wary; unimaginable filth roams this village.")));
            earhRequest.Add(new InteractionData(Dialogue("To leave this cell, you'll need the key. It's on that dead heathen opposite us.")));
            questInterim.Add(new InteractionData(Dialogue("You should find the Living Quarter in the East, but be wary; unimaginable filth roams this village.")));
            questInterim.Add(new InteractionData(Dialogue("To leave this cell, you'll need the key. It's on that dead heathen across the room.")));
            questInterim.Add(new InteractionData(Description("Ormod looks at the ground and mumbles to himself, clutching an amulet in his fist.")));
            thanksForEarh.Add(new InteractionData(Dialogue("Bless you, saint. Earh returned to me not long ago. He told me of your valour.")));
            thanksForEarh.Add(new InteractionData(Dialogue("To think that ghastly creature is gone brings me much peace.")));
            thanksForEarh.Add(new InteractionData(Description("Ormod looks down at his chest and lifts his little amulet into his hand.")));
            thanksForEarh.Add(new InteractionData(Description("Painfully, he lifts it from over his head and offers it to you. A tear falls down his cheek.")));
            thanksForEarh.Add(new InteractionData(Dialogue("I have...nothing but...this. Forgive me...")));
            thanksForEarh.Add(new InteractionData(Description("Take Ormod's amulet?"), true));
            amuletTaken.Add(new InteractionData(Description("Ormod looks down at the ground and covers his face. After a moment of silent jerking, he looks up, weary.")));
            amuletTaken.Add(new InteractionData(Dialogue("Blessing upon you, stranger...")));
            amuletRefused.Add(new InteractionData(Description("Ormod withdraws the amulet to his chest, clutching it dearly. He looks up and the briefest smile dawns.")));
            amuletRefused.Add(new InteractionData(Dialogue("Blessing upon you, friend...")));
            afterAmulet.Add(new InteractionData(Dialogue("Something has changed here. The air seems less thick. I believe you're making a difference.")));
            afterAmulet.Add(new InteractionData(Dialogue("Do not give up. The fate of this place lies with you.")));
        }
    }
}
