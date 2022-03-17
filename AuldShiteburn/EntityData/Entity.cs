namespace AuldShiteburn.EntityData
{
    internal abstract class Entity
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public abstract string EntityChar { get; }
        public abstract void Move();
    }
}