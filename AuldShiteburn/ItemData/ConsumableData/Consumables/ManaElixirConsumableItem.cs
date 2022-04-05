using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    internal class ManaElixirConsumableItem : ConsumableItem
    {
        public override string Name => "Azure Elixir";
        public override string Description => "An azure elixir that restores 6-12 Mana.";
        public int MinMana { get; } = 6;
        public int MaxMana { get; } = 12;
        public override void OnConsumption()
        {
            Random rand = new Random();
            int manaRestored = rand.Next(MinMana, MaxMana + 1);
            Utils.SetCursorInteract(Console.CursorTop);
            Utils.WriteColour($"{Name} restores ");
            Utils.WriteColour($"{manaRestored} ", ConsoleColor.Blue);
            Utils.WriteColour($"Mana!");
            PlayerEntity.Instance.Mana += manaRestored;
        }
    }
}
