using AuldShiteburn.MapData.AreaData.Areas;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.Maps
{
    class AuldShiteburnMap : Map
    {
        public override string Name => "Auld Shiteburn";

        public override int Width => 3;

        public override int Height => 3;

        public AuldShiteburnMap()
        {
            AvailableAreas.Add(new StartArea());
        }
    }
}
