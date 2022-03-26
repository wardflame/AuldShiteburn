using System;

namespace AuldShiteburn.ItemData.ArmourData
{
    [Serializable]
    internal class ArmourItem : Item
    {
        public override string Name { get; }

        public ArmourItem(string name)
        {
            this.Name = name;
        }
    }
}
