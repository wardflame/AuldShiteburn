using System;
using System.Collections.Generic;

namespace AuldShiteburn
{
    class Utilities
    {
        /// <summary>
        /// Get a generic list and print out its contents.
        /// In the context of a menu, assign an index if applicable,
        /// to highlight the item at that index.
        /// </summary>
        /// <typeparam name="T">Any type required.</typeparam>
        /// <param name="list">List to print.</param>
        /// <param name="index">Index of list to change colour of when printed.</param>
        public static void PrintList<T>(List<T> list, int index)
        {
            foreach (T item in list)
            {
                if (item != null)
                {
                    if (list.IndexOf(item) == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(item);
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.Write(item + "\n");
                }
            }
        }

        public static bool VerificationQuery(string query)
        {
            Console.WriteLine(query);
            InputSystem.GetInput();
            if (InputSystem.InputKey == ConsoleKey.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
