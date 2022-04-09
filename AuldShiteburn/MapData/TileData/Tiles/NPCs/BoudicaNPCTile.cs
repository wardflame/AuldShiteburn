using AuldShiteburn.EntityData;
using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.ItemData.WeaponData;
using AuldShiteburn.MapData.AreaData.Areas;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles.NPCs
{
    [Serializable]
    internal class BoudicaNPCTile : NPCTile
    {
        public override string NPCName => finishedDrain ? "Shaman Boudica" : "Boudica";
        private List<InteractionData> drainMeeting = new List<InteractionData>();
        private List<InteractionData> shitebreachMeeting = new List<InteractionData>();
        private List<InteractionData> shitebreachRegular = new List<InteractionData>();
        private bool finishedDrain = false;
        private bool giftGiven = false;

        public BoudicaNPCTile() : base("%")
        {
        }

        public override void Interaction()
        {
            Utils.ClearInteractInterface();
            if (!finishedDrain)
            {
                finishedDrain = CycleInteraction(drainMeeting);
                StartArea shitebreach = (StartArea)Map.Instance.ActiveAreas[Map.Instance.GetIndex(0, 0)];
                shitebreach.SetTile(2, 12, this);
                Map.Instance.CurrentArea.SetTile(18, 1, AirTile);
                Map.Instance.PrintTile(18, 1);
                shitebreach.NPCsRemaining--;
            }
            else if (!giftGiven)
            {
                giftGiven = CycleInteraction(shitebreachMeeting);
                WeaponType wType = WeaponType.Longsword;
                switch(PlayerEntity.Instance.Class.ClassType)
                {
                    case ClassType.Heathen:
                        {
                            wType = WeaponType.HandAxe;
                        }
                        break;
                    case ClassType.Fighter:
                        {
                            wType = WeaponType.Longsword;
                        }
                        break;
                    case ClassType.Marauder:
                        {
                            wType = WeaponType.Greatsword;
                        }
                        break;
                    case ClassType.Monk:
                        {
                            wType = WeaponType.Spear;
                        }
                        break;
                    case ClassType.Rogue:
                        {
                            wType = WeaponType.Shortsword;
                        }
                        break;
                }
                if (giftGiven)
                {
                    PlayerEntity.Instance.Inventory.AddItem(
                    new WeaponItem()
                    {
                        Type = wType,
                        Material = WeaponMaterial.WeaponMaterialMoonstone,
                        Property = WeaponProperty.WeaponPropertyHoly
                    }, true);
                    CycleInteraction(shitebreachRegular);
                }
            }
            else
            {
                CycleInteraction(shitebreachRegular);
            }
        }

        protected override void InitLines()
        {
            drainMeeting.Add(new InteractionData(Description("A broad, pungent, grim woman in rags sits at the back of the cell, staring at the ground.")));
            drainMeeting.Add(new InteractionData(Dialogue($"Come to finish the job then? Go ahead. I'll haunt you when I'm-...")));
            drainMeeting.Add(new InteractionData(Dialogue($"You're no heathen, nor hunter. Elatha brings me fortune.")));
            drainMeeting.Add(new InteractionData(Dialogue($"Tell me, stranger, does that damned Dung Eater lie dead?"), true));
            drainMeeting.Add(new InteractionData(Dialogue($"Then you've done this auld town a valiant service.")));
            drainMeeting.Add(new InteractionData(Dialogue($"I will depart for Shitebreach. When you return, come find me.")));
            drainMeeting.Add(new InteractionData(Dialogue($"I'll be in a better position to express my gratitude.")));
            shitebreachMeeting.Add(new InteractionData(Description("Boudica appears another woman entire. She is tall and strong and offers a grateful bow of her head.")));
            shitebreachMeeting.Add(new InteractionData(Dialogue($"There you are. In your absence, I've prayed to the gods. Elatha smiles on you too, it seems.")));
            shitebreachMeeting.Add(new InteractionData(Dialogue($"Whatever you've done so far, it's working. Our obelisk glows a little brighter.")));
            shitebreachMeeting.Add(new InteractionData(Dialogue($"Elatha has granted you a blessed weapon. Please, it is for you.")));
            shitebreachRegular.Add(new InteractionData(Dialogue($"Your presence is healing Auld Shiteburn. Do not waver.")));
            shitebreachRegular.Add(new InteractionData(Dialogue($"Darkness lies ahead, but you will overcome it.")));
            shitebreachRegular.Add(new InteractionData(Dialogue($"There was an amulet, long ago, that provided comfort to us in dark times.")));
            shitebreachRegular.Add(new InteractionData(Dialogue($"We once bathed it in the altar. I imagine it would prove useful...")));
        }
    }
}
