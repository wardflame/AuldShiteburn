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
╚═════╝░╚══════╝░░░╚═╝░░░░░░╚═╝░░░╚═╝╚═╝░░╚══╝░╚═════╝░╚═════╝░";

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
╚═╝░░╚═╝░╚═════╝░╚══════╝╚═════╝░  ╚═════╝░╚═╝░░╚═╝╚═╝░░░╚═╝░░░╚══════╝╚═════╝░░╚═════╝░╚═╝░░╚═╝╚═╝░░╚══╝";

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
    }
}
