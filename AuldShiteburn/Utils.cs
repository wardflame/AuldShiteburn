using AuldShiteburn.MapData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn
{
    class Utils
    {
        #region UI Cursor Offsets
        public static int UIInteractOffset => (Map.Instance.CurrentArea.Width * 2) + 2;
        public static int UIInteractHeight { get; } = 2;

        public static int UIInventoryOffset { get; } = 1;
        public static int UIInventoryHeight { get; } = Map.Instance.CurrentArea.Height + 14;

        public static int UIPlayerStatOffset { get; } = 1;
        public static int UIPlayerStatHeight { get; } = Map.Instance.CurrentArea.Height + 1;
        #endregion UI Cursor Offsets


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

        /// <summary>
        /// Query the player and get a yes/no response.
        /// </summary>
        /// <param name="query">Query string.</param>
        /// <returns></returns>
        public static bool VerificationQuery(string query)
        {
            if (query != null)
            {
                Console.WriteLine(query);
            }
            do
            {
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.Y:
                        {
                            return true;
                        }
                    case ConsoleKey.N:
                        {
                            return false;
                        }
                }
            } while (InputSystem.InputKey != ConsoleKey.Y && InputSystem.InputKey != ConsoleKey.N);
            return false;
        }

        /// <summary>
        /// Write a string with colour.
        /// </summary>
        /// <param name="colour">Colour for string.</param>
        /// <param name="message">String to print.</param>
        public static void WriteColour(string message, ConsoleColor colour = ConsoleColor.Gray)
        {
            Console.ForegroundColor = colour;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Clear a length of characters with empty characters.
        /// </summary>
        /// <param name="clearLength">How far to send the empty characters.</param>
        public static void ClearLine(int clearLength = 0)
        {
            Console.Write(new string(' ', clearLength));
        }

        /// <summary>
        /// Set the cursor to the default interact window position,
        /// with offsets to go wider or deeper than usual.
        /// </summary>
        /// <param name="offsetX">Addition cursor width offset.</param>
        /// <param name="offsetY">Additional cursor height offset.</param>
        public static void SetCursorInteract(int offsetY = 0, int offsetX = 0)
        {
            Console.SetCursorPosition(UIInteractOffset + offsetX, UIInteractHeight + offsetY);
        }

        /// <summary>
        /// Position the cursor at a static value (interact interface) and iterate
        /// down for a length of Y at an offset of X (Cursor.Left), clearing the line with
        /// empty characters.
        /// </summary>
        /// <param name="lengthY">The number of lines to clear downwards.</param>
        /// <param name="offsetX">The offset across to start clearing from.</param>
        public static void ClearInteractInterface(int lengthY = 18, int offsetX = 0)
        {
            for (int y = UIInteractHeight; y <= UIInteractHeight + lengthY; y++)
            {
                Console.CursorLeft = UIInteractOffset + offsetX;
                Console.CursorTop = y;
                Console.Write(new string(' ', Console.WindowWidth - (UIInteractOffset + offsetX)));
            }
        }

        /// <summary>
        /// Set the cursor to the default player stat window position,
        /// with offsets to go wider or deeper than usual.
        /// </summary>
        /// <param name="offsetX">Addition cursor width offset.</param>
        /// <param name="offsetY">Additional cursor height offset.</param>
        public static void SetCursorPlayerStat(int offsetY = 0, int offsetX = 0)
        {
            Console.SetCursorPosition(UIPlayerStatOffset + offsetX, UIPlayerStatHeight + offsetY);
        }

        /// <summary>
        /// Position the cursor at a static value (player stat interface) and iterate
        /// down for a length of Y at an offset of X (Cursor.Left), clearing the line with
        /// empty characters.
        /// </summary>
        /// <param name="lengthY">The number of lines to clear downwards.</param>
        /// <param name="offsetX">The offset across to start clearing from.</param>
        public static void ClearPlayerStatInterface(int lengthY = 11, int offsetX = 0)
        {
            for (int y = UIPlayerStatHeight; y <= UIPlayerStatHeight + lengthY; y++)
            {
                Console.CursorLeft = UIPlayerStatOffset + offsetX;
                Console.CursorTop = y;
                Console.Write(new string(' ', Console.WindowWidth - (UIPlayerStatOffset + offsetX)));
            }
        }

        /// <summary>
        /// Set the cursor to the default inventory window position,
        /// with offsets to go wider or deeper than usual.
        /// </summary>
        /// <param name="offsetX">Addition cursor width offset.</param>
        /// <param name="offsetY">Additional cursor height offset.</param>
        public static void SetCursorInventory(int offsetY = 0, int offsetX = 0)
        {
            Console.SetCursorPosition(UIInventoryOffset + offsetX, UIInventoryHeight + offsetY);
        }

        /// <summary>
        /// Position the cursor at a static value (player inventory interface) and iterate
        /// down for a length of Y at an offset of X (Cursor.Left), clearing the line with
        /// empty characters.
        /// </summary>
        /// <param name="lengthY">The number of lines to clear downwards.</param>
        /// <param name="offsetX">The offset across to start clearing from.</param>
        public static void ClearPlayerInventoryInterface(int lengthY = 6, int offsetX = 0)
        {
            for (int y = UIInventoryHeight; y <= UIInventoryHeight + lengthY; y++)
            {
                Console.CursorLeft = UIInventoryOffset + offsetX;
                Console.CursorTop = y;
                Console.Write(new string(' ', Console.WindowWidth - (UIInventoryOffset + offsetX)));
            }
        }

        /// <summary>
        /// Target a cursor location with the interact coordinates as an origin point, then clear.
        /// This is used when we don't want to clear the whole interface, only a specific region.
        /// </summary>
        /// <param name="offsetY"></param>
        /// <param name="length"></param>
        /// <param name="offsetX"></param>
        public static void ClearInteractArea(int offsetY = 0, int length = 0, int offsetX = 0)
        {
            for (int y = UIInteractHeight + offsetY; y <= UIInteractHeight + offsetY + length; y++)
            {
                Console.CursorLeft = UIInteractOffset + offsetX;
                Console.CursorTop = y;
                Console.Write(new string(' ', Console.WindowWidth - (UIInteractOffset + offsetX)));
            }
        }
    }
}
