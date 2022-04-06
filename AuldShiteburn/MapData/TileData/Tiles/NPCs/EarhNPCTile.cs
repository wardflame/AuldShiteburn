using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    [Serializable]
    internal class EarhNPCTile : NPCTile
    {
        public override string NPCName => "Earh";
        public override ConsoleColor Foreground => ConsoleColor.Cyan;
        private List<InteractionData> stage1BeatEnemies = new List<InteractionData>();
        private List<InteractionData> stage1Interim = new List<InteractionData>();
        private List<InteractionData> stage2Freed = new List<InteractionData>();
        private bool stage1 = false;
        private bool stage2 = false;

        public EarhNPCTile() : base("%%")
        {
        }

        public override void Interaction()
        {
            Utils.ClearInteractInterface();
            if (!stage1)
            {
                stage1 = CycleInteraction(stage1BeatEnemies, "Hmph. All right.");
            }
            else if (!stage2)
            {
                stage2 = CycleInteraction(stage2Freed);
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
        }
    }
}
