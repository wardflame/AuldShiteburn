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
        public static int UIInventoryHeight => Map.Instance.CurrentArea.Height + 8;

        public static int UIPlayerStatOffset { get; } = 1;
        public static int UIPlayerStatHeight => Map.Instance.CurrentArea.Height + 1;
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
                        break;
                    case ConsoleKey.N:
                        {
                            return false;
                        }
                        break;
                }
            } while (InputSystem.InputKey != ConsoleKey.Y && InputSystem.InputKey != ConsoleKey.N);
            return false;
        }

        /// <summary>
        /// Write a string with colour.
        /// </summary>
        /// <param name="colour">Colour for string.</param>
        /// <param name="message">String to print.</param>
        public static void WriteColour(ConsoleColor colour, string message)
        {
            Console.ForegroundColor = colour;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Clear an area of the screen.
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
        public static void SetCursorInteract(int offsetX = 0, int offsetY = 0)
        {
            Console.SetCursorPosition(UIInteractOffset + offsetX, UIInteractHeight + offsetY);
        }

        /// <summary>
        /// Place the cursor 1 row down in final column of area width. Then, until reaching
        /// the area height, go down each row and replace any text with space characters until
        /// the end of the line.
        /// </summary>
        public static void ClearInteractInterface(int offsetX = 0, int offsetY = 2)
        {
            for (int y = UIInteractHeight; y <= UIInteractHeight + offsetY; y++)
            {
                Console.CursorLeft = UIInteractOffset + offsetX;
                Console.CursorTop = y;
                Console.Write(new string(' ', Console.WindowWidth - UIInteractOffset));
            }
        }

        /// <summary>
        /// Set the cursor to the default player stat window position,
        /// with offsets to go wider or deeper than usual.
        /// </summary>
        /// <param name="offsetX">Addition cursor width offset.</param>
        /// <param name="offsetY">Additional cursor height offset.</param>
        public static void SetCursorPlayerStat(int offsetX = 0, int offsetY = 0)
        {
            Console.SetCursorPosition(UIPlayerStatOffset + offsetX, UIPlayerStatHeight + offsetY);
        }

        /// <summary>
        /// Place the cursor 1 row down in final column of area width. Then, until reaching
        /// the area height, go down each row and replace any text with space characters until
        /// the end of the line.
        /// </summary>
        public static void ClearPlayerStatInterface(int offsetX = 0, int offsetY = 2)
        {
            for (int y = UIPlayerStatHeight; y <= UIPlayerStatHeight + offsetY; y++)
            {
                Console.CursorLeft = UIPlayerStatOffset + offsetX;
                Console.CursorTop = y;
                Console.Write(new string(' ', Console.WindowWidth - UIPlayerStatOffset));
            }
        }

        /// <summary>
        /// Set the cursor to the default inventory window position,
        /// with offsets to go wider or deeper than usual.
        /// </summary>
        /// <param name="offsetX">Addition cursor width offset.</param>
        /// <param name="offsetY">Additional cursor height offset.</param>
        public static void SetCursorInventory(int offsetX = 0, int offsetY = 0)
        {
            Console.SetCursorPosition(UIInventoryOffset + offsetX, UIInventoryHeight + offsetY);
        }

        /// <summary>
        /// Place the cursor 1 row down in final column of area width. Then, until reaching
        /// the area height, go down each row and replace any text with space characters until
        /// the end of the line.
        /// </summary>
        public static void ClearInventoryInterface(int offsetX = 0, int offsetY = 5)
        {
            for (int y = UIInventoryHeight; y <= UIInventoryHeight + offsetY; y++)
            {
                Console.CursorLeft = UIInventoryOffset + offsetX;
                Console.CursorTop = y;
                Console.Write(new string(' ', Console.WindowWidth - UIInventoryOffset));
            }
        }

        /// <summary>
        /// Clear the interact area and print a prompt message.
        /// </summary>
        /// <param name="message">String to print as prompt.</param>
        public static void InteractPrompt(string message)
        {
            Utils.ClearInteractInterface();
            Console.CursorLeft = UIInteractOffset;
            Console.CursorTop = UIInteractHeight;
            Console.Write(message);
        }
    }
}
