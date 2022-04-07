using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.PayloadData;
using AuldShiteburn.EntityData;
using System;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    internal class TrapTile : Tile
    {
        public override ConsoleColor Foreground => Used ? ConsoleColor.Black : ConsoleColor.DarkGray;
        public bool Used { get; private set; }
        public override string DisplayChar => "~";

        public TrapTile() : base("", false)
        {
        }

        public override void OnCollision(Entity entity)
        {
            CombatPayload attackPayload = new CombatPayload(true, hasPhysical: true, physicalAttackType: PhysicalDamageType.Pierce, physicalDamage: 10);
            PlayerEntity.Instance.ReceiveAttack(attackPayload);
            Used = false;
        }
    }
}
