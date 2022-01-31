using AuldShiteburn.EntityData;
using System;

namespace AuldShiteburn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerEntity player1 = new PlayerEntity()
            {
                name = "Tyron",
                maxHP = 50,
                maxStamina = 25,
                maxMana = 10
            };

            Console.WriteLine($"{player1.name}, {player1.HP}, {player1.Stamina}, {player1.Mana}");
            player1.HP += 75;
            player1.Stamina -= 20;
            player1.Mana += 20;
            Console.WriteLine($"{player1.name}, {player1.HP}, {player1.Stamina}, {player1.Mana}");
        }
    }
}
