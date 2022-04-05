using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    internal class HealthElixirConsumableItem : ConsumableItem
    {
        public override string Name => "Crimson Elixir";
        public override string Description => "A crimson elixir that restores 4-8 HP.";
        public int MinHeal { get; } = 4;
        public int MaxHeal { get; } = 8;
        public override void OnConsumption()
        {
            Random rand = new Random();
            int hpRestored = rand.Next(MinHeal, MaxHeal + 1);
            Utils.SetCursorInteract(Console.CursorTop);
            Utils.WriteColour($"{Name} restores ");
            Utils.WriteColour($"{hpRestored} ", ConsoleColor.DarkRed);
            Utils.WriteColour($"HP!");
            PlayerEntity.Instance.HP += hpRestored;
        }
    }
}
