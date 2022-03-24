using AuldShiteburn.MapData.AreaData.Areas;
using System;

namespace AuldShiteburn.MapData.Maps
{
    [Serializable]
    class AuldShiteburnMap : Map
    {
        public override string Name => "Auld Shiteburn";
        public override int Width => 3;
        public override int Height => 3;
        private bool newGame = true;

        public AuldShiteburnMap()
        {
            AvailableAreas.Add(new GraveyardArea());
            AvailableAreas.Add(new GuildHallArea());
            AvailableAreas.Add(new MarketArea());
            AvailableAreas.Add(new ResidencesArea());
            AvailableAreas.Add(new ShitepileArea());
            AvailableAreas.Add(new ShiterootGardenArea());
            AvailableAreas.Add(new StablesArea());
            AvailableAreas.Add(new TheDrainArea());
            AvailableAreas.Add(new TheGranaryArea());
        }

        protected override void SetFixedAreas()
        {
            SetArea(0, 0, new StartArea());
            SetArea(2, 2, new EndArea());

            if (newGame)
            {
                SetArea(1, 0, new ResidencesArea());
                newGame = false;
            }
        }
    }
}
