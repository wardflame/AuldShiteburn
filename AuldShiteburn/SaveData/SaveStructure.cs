using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.SaveData
{
    internal struct SaveStructure
    {
        public Type mapType;
        public string name;
        public int width;
        public int height;
        public int posX;
        public int posY;
        public PlayerEntity player;
        public List<Area> availableAreas;
        public Area[] areas;
        public Area currentArea;
    }
}
