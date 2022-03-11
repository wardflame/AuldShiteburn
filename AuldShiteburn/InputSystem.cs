using System;

namespace AuldShiteburn
{
    static class InputSystem
    {
        public static ConsoleKey InputKey { get; private set; }

        public static void GetInput()
        {
            InputKey = Console.ReadKey(true).Key;
        }
    }
}
