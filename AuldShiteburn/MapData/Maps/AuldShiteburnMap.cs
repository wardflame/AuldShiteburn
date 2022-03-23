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

        public AuldShiteburnMap()
        {
            AvailableAreas.Add(new GraveyardArea());
            AvailableAreas.Add(new GuildHallArea());
            AvailableAreas.Add(new MarketArea());
            AvailableAreas.Add(new ResidentsArea());
            AvailableAreas.Add(new ShitepileArea());
            AvailableAreas.Add(new ShiterootGardenArea());
            AvailableAreas.Add(new StablesArea());
            AvailableAreas.Add(new TheDrainArea());
            AvailableAreas.Add(new TheGranaryArea());
        }

        protected override void SetFixedAreas()
        {
            SetArea(0, 0, new ResidentsArea());
            SetArea(2, 2, new EndArea());
        }
    }
}
