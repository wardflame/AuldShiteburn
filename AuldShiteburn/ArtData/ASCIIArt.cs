using System;
using System.Collections.Generic;

namespace AuldShiteburn.ArtData
{
    internal static class ASCIIArt
    {
        /// <summary>
        /// List of ASCII numbers to use in methods to
        /// print multiple numbers where needed.
        /// </summary>
        public static List<string> numbers = new List<string>()
        {
            @"
█▀▀█ 
█▄▀█ 
█▄▄█",
            @"
▄█░ 
░█░ 
▄█▄",
            @"
█▀█ 
░▄▀ 
█▄▄",
            @"
█▀▀█ 
░░▀▄ 
█▄▄█",
            @"
░█▀█░ 
█▄▄█▄ 
░░░█░",
            @"
█▀▀ 
▀▀▄ 
▄▄▀",
            @"
▄▀▀▄ 
█▄▄░ 
▀▄▄▀",
            @"
▀▀▀█ 
░░█░ 
░▐▌░",
            @"
▄▀▀▄ 
▄▀▀▄ 
▀▄▄▀",
            @"
▄▀▀▄ 
▀▄▄█ 
░▄▄▀"
        };

        public const string MENU_NEWGAME =
            @"
█▀▀▄ █▀▀ █░░░█ 　 █▀▀▀ █▀▀█ █▀▄▀█ █▀▀ 
█░░█ █▀▀ █▄█▄█ 　 █░▀█ █▄▄█ █░▀░█ █▀▀ 
▀░░▀ ▀▀▀ ░▀░▀░ 　 ▀▀▀▀ ▀░░▀ ▀░░░▀ ▀▀▀";

        public const string MENU_RESUME =
            @"
█▀▀█ █▀▀ █▀▀ █░░█ █▀▄▀█ █▀▀ 
█▄▄▀ █▀▀ ▀▀█ █░░█ █░▀░█ █▀▀ 
▀░▀▀ ▀▀▀ ▀▀▀ ░▀▀▀ ▀░░░▀ ▀▀▀";

        public const string MENU_SAVE =
            @"
█▀▀ █▀▀█ ▀█░█▀ █▀▀ 
▀▀█ █▄▄█ ░█▄█░ █▀▀ 
▀▀▀ ▀░░▀ ░░▀░░ ▀▀▀";

        public const string MENU_LOAD =
    @"
█░░ █▀▀█ █▀▀█ █▀▀▄ 
█░░ █░░█ █▄▄█ █░░█ 
▀▀▀ ▀▀▀▀ ▀░░▀ ▀▀▀░";

        public const string MENU_SETTINGS =
            @"
█▀▀ █▀▀ ▀▀█▀▀ ▀▀█▀▀ ░▀░ █▀▀▄ █▀▀▀ █▀▀ 
▀▀█ █▀▀ ░░█░░ ░░█░░ ▀█▀ █░░█ █░▀█ ▀▀█ 
▀▀▀ ▀▀▀ ░░▀░░ ░░▀░░ ▀▀▀ ▀░░▀ ▀▀▀▀ ▀▀▀";

        public const string MENU_SEXRATIO =
            @"
█▀▀ █▀▀ █░█ 　 █▀▀█ █▀▀█ ▀▀█▀▀ ░▀░ █▀▀█ 
▀▀█ █▀▀ ▄▀▄ 　 █▄▄▀ █▄▄█ ░░█░░ ▀█▀ █░░█ 
▀▀▀ ▀▀▀ ▀░▀ 　 ▀░▀▀ ▀░░▀ ░░▀░░ ▀▀▀ ▀▀▀▀";

        public const string MENU_EXIT =
            @"
█▀▀ █░█ ░▀░ ▀▀█▀▀ 
█▀▀ ▄▀▄ ▀█▀ ░░█░░ 
▀▀▀ ▀░▀ ▀▀▀ ░░▀░░";

        public const string BANNER_SETTINGS =
            @"
░██████╗███████╗████████╗████████╗██╗███╗░░██╗░██████╗░░██████╗
██╔════╝██╔════╝╚══██╔══╝╚══██╔══╝██║████╗░██║██╔════╝░██╔════╝
╚█████╗░█████╗░░░░░██║░░░░░░██║░░░██║██╔██╗██║██║░░██╗░╚█████╗░
░╚═══██╗██╔══╝░░░░░██║░░░░░░██║░░░██║██║╚████║██║░░╚██╗░╚═══██╗
██████╔╝███████╗░░░██║░░░░░░██║░░░██║██║░╚███║╚██████╔╝██████╔╝
╚═════╝░╚══════╝░░░╚═╝░░░░░░╚═╝░░░╚═╝╚═╝░░╚══╝░╚═════╝░╚═════╝░
";

        public const string MENU_SAVESLOT =
            @"
█▀▀ █░░ █▀▀█ ▀▀█▀▀ 
▀▀█ █░░ █░░█ ░░█░░ 
▀▀▀ ▀▀▀ ▀▀▀▀ ░░▀░░ ";

        public const string BANNER_AULDSHITEBURN =
            @"
░█████╗░██╗░░░██╗██╗░░░░░██████╗░  ░██████╗██╗░░██╗██╗████████╗███████╗██████╗░██╗░░░██╗██████╗░███╗░░██╗
██╔══██╗██║░░░██║██║░░░░░██╔══██╗  ██╔════╝██║░░██║██║╚══██╔══╝██╔════╝██╔══██╗██║░░░██║██╔══██╗████╗░██║
███████║██║░░░██║██║░░░░░██║░░██║  ╚█████╗░███████║██║░░░██║░░░█████╗░░██████╦╝██║░░░██║██████╔╝██╔██╗██║
██╔══██║██║░░░██║██║░░░░░██║░░██║  ░╚═══██╗██╔══██║██║░░░██║░░░██╔══╝░░██╔══██╗██║░░░██║██╔══██╗██║╚████║
██║░░██║╚██████╔╝███████╗██████╔╝  ██████╔╝██║░░██║██║░░░██║░░░███████╗██████╦╝╚██████╔╝██║░░██║██║░╚███║
╚═╝░░╚═╝░╚═════╝░╚══════╝╚═════╝░  ╚═════╝░╚═╝░░╚═╝╚═╝░░░╚═╝░░░╚══════╝╚═════╝░░╚═════╝░╚═╝░░╚═╝╚═╝░░╚══╝
";
        public const string DEATH_MESSAGE =
            @"
██╗░░░██╗░█████╗░██╗░░░██╗  ██████╗░██╗███████╗██████╗░
╚██╗░██╔╝██╔══██╗██║░░░██║  ██╔══██╗██║██╔════╝██╔══██╗
░╚████╔╝░██║░░██║██║░░░██║  ██║░░██║██║█████╗░░██║░░██║
░░╚██╔╝░░██║░░██║██║░░░██║  ██║░░██║██║██╔══╝░░██║░░██║
░░░██║░░░╚█████╔╝╚██████╔╝  ██████╔╝██║███████╗██████╔╝
░░░╚═╝░░░░╚════╝░░╚═════╝░  ╚═════╝░╚═╝╚══════╝╚═════╝░";

        public const string VICTORY_MESSAGE =
            @"
██╗░░░██╗██╗░█████╗░████████╗░█████╗░██████╗░██╗░░░██╗██╗
██║░░░██║██║██╔══██╗╚══██╔══╝██╔══██╗██╔══██╗╚██╗░██╔╝██║
╚██╗░██╔╝██║██║░░╚═╝░░░██║░░░██║░░██║██████╔╝░╚████╔╝░██║
░╚████╔╝░██║██║░░██╗░░░██║░░░██║░░██║██╔══██╗░░╚██╔╝░░╚═╝
░░╚██╔╝░░██║╚█████╔╝░░░██║░░░╚█████╔╝██║░░██║░░░██║░░░██╗
░░░╚═╝░░░╚═╝░╚════╝░░░░╚═╝░░░░╚════╝░╚═╝░░╚═╝░░░╚═╝░░░╚═╝";

        /// <summary>
        /// Take a number, turn it into string, get each number as char
        /// and turn it into its ASCII art equivalent from the list above.
        /// </summary>
        /// <param name="number">Number we want broken down to ASCII.</param>
        /// <returns>Returns ASCII art string form of numbers.</returns>
        public static string NumberToASCII(int number)
        {
            List<string> lines = new List<string>();
            string numString = number.ToString();
            foreach (char c in numString)
            {
                if (int.TryParse(c.ToString(), out int cNum))
                {
                    string[] asciiLines = numbers[cNum].Split(Environment.NewLine);
                    for (int i = 0; i < asciiLines.Length; i++)
                    {
                        if (i >= lines.Count)
                        {
                            lines.Add(asciiLines[i]);
                        }
                        else
                        {
                            lines[i] += asciiLines[i];
                        }
                    }
                }
            }
            return string.Join(Environment.NewLine, lines.ToArray());
        }

        /// <summary>
        /// Take ASCII art and append it to the end of another
        /// series of ASCII characters.
        /// </summary>
        /// <param name="word">Word to append.</param>
        /// <returns>ASCII art appended phrase.</returns>
        public static string ASCIIAppendToEnd(params string[] word)
        {
            List<string> lines = new List<string>();
            foreach (string s in word)
            {
                string[] asciiLines = s.Split(Environment.NewLine);
                for (int i = 0; i < asciiLines.Length; i++)
                {
                    if (i >= lines.Count)
                    {
                        lines.Add(asciiLines[i]);
                    }
                    else
                    {
                        lines[i] += asciiLines[i];
                    }
                }
            }
            return string.Join(Environment.NewLine, lines.ToArray());
        }

        /// <summary>
        /// Split ASCII art into rows and print bit by bit,
        /// vertically using Console.CursorTop.
        /// </summary>
        /// <param name="ascii">ASCII to print.</param>
        /// <param name="colour">Colour to print in.</param>
        public static void PrintASCII(string ascii, ConsoleColor colour = ConsoleColor.Gray)
        {
            Console.ForegroundColor = colour;
            string[] messageArray = ascii.Split(Environment.NewLine);
            for (int i = 0; i < messageArray.Length; i++)
            {
                Console.CursorTop++;
                Utils.WriteColour(messageArray[i], ConsoleColor.DarkGreen);
                Console.CursorLeft -= messageArray[i].Length;
            }
            Console.ResetColor();
        }
    }
}
