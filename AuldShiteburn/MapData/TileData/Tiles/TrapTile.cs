using AuldShiteburn.CombatData;
using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    internal class TrapTile : Tile
    {
        public override ConsoleColor Foreground => Used ? ConsoleColor.Black : ConsoleColor.DarkGray;
        public bool Used { get; }
        public override string DisplayChar => "~";

        public TrapTile() : base("", false)
        {
        }

        public override void OnCollision(Entity entity)
        {
            Damage damage = new Damage(5, 0, PhysicalDamageType.Slash);
            PlayerEntity.Instance.ReceiveDamage(damage);
        }
    }
}
