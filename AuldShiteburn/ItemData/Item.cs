using System;

namespace AuldShiteburn.ItemData
{
    [Serializable]
    internal abstract class Item
    {
        public virtual string Name { get; }
    }
}
