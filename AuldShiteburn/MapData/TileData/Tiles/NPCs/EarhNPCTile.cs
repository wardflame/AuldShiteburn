using AuldShiteburn.EntityData;
using AuldShiteburn.ItemData.ArmourData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MapData.AreaData.Areas;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    [Serializable]
    internal class EarhNPCTile : NPCTile
    {
        public override string NPCName => "Earh";
        private List<InteractionData> stage1BeatEnemies = new List<InteractionData>();
        private List<InteractionData> stage2Freed = new List<InteractionData>();
        private List<InteractionData> shitebreachMeet = new List<InteractionData>();
        private bool stage1 = false;
        private bool stage2 = false;

        public EarhNPCTile() : base("%")
        {
        }

        public override void Interaction()
        {
            Utils.ClearInteractInterface();
            if (!stage1)
            {
                stage1 = CycleInteraction(stage1BeatEnemies);
            }
            else if (!stage2)
            {
                stage2 = CycleInteraction(stage2Freed);
                if (stage2)
                {
                    PlayerEntity.Instance.Inventory.AddItem(ArmourItem.IndomitableCuirass, true);
                    StartArea shitebreach = (StartArea)Map.Instance.ActiveAreas[Map.Instance.GetIndex(0, 0)];
                    shitebreach.SetTile(5, 1, this);
                    Map.Instance.CurrentArea.SetTile(1, 6, AirTile);
                    Map.Instance.PrintTile(1, 6);
                    shitebreach.NPCsRemaining--;
                }
            }
            else
            {
                CycleInteraction(shitebreachMeet);
            }
        }

        protected override void InitLines()
        {
            stage1BeatEnemies.Add(new InteractionData(Description("A relieved voice echoes through the courtyard.")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"Heavens! You've had them. I thought I'd be in here forever...")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"I am {NPCName}, once-valiant. Alas, now I'm stuck in this house.")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"My companion, Ormod, and I were with another man, Orlege.")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"We weren't long here when, as night fell, a horrific creature ambushed us.")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"I never truly saw it, but the sounds it made still fills my mind. Gods...")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"It dragged him away to the Shitepile south of here.")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"He did not die for a long while, for I heard his screams long into the night.")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"They changed, twisted with time. I shiver at the thought...")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"He locked me in here just before the beast could assail the house.")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"And, sadly, he had the key to this house, and I imagine it was dragged away with him.")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"I beg of you, free me from this house, so that I may return to Ormod.")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"Should you choose to pursue the key, take fire with you.")));
            stage1BeatEnemies.Add(new InteractionData(Dialogue($"Before the beast could snatch Orlege away, I saw it recoil at his torch's touch.")));
            stage2Freed.Add(new InteractionData(Dialogue($"You've actually done it... That shit-eater is dead? Gods be praised.")));
            stage2Freed.Add(new InteractionData(Description($"Earh unbuckles his cuirass and offers it to you.")));
            stage2Freed.Add(new InteractionData(Dialogue($"By my honour, it's yours. I'll see you back in Shitebreach.")));
            shitebreachMeet.Add(new InteractionData(Dialogue($"Hail, friend. It is good to be back in Ormod's presence.")));
            shitebreachMeet.Add(new InteractionData(Dialogue($"I am forever in your debt.")));
        }
    }
}
