using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.ItemData.ConsumableData.Consumables
{
    internal class StaminaElixirConsumableItem : ConsumableItem
    {
        public override string Name => "Jade Elixir";
        public override string Description => "A jade elixir that restores 6-12 Stamina.";
        public int MinStamina { get; } = 6;
        public int MaxStamina { get; } = 12;
        public override void OnConsumption()
        {
            Random rand = new Random();
            int staminaRestored = rand.Next(MinStamina, MaxStamina + 1);
            Utils.SetCursorInteract(Console.CursorTop);
            Utils.WriteColour($"{Name} restores ");
            Utils.WriteColour($"{staminaRestored} ", ConsoleColor.Green);
            Utils.WriteColour($"Stamina!");
            PlayerEntity.Instance.HP += staminaRestored;
        }
    }
}
