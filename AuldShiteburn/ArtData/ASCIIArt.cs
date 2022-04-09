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

        public const string INTRO_PAGE1 =
            @"
▒█░░▒█ █▀▀ █░░ █▀▀ █▀▀█ █▀▄▀█ █▀▀ 　 ▀▀█▀▀ █▀▀█ 　 ▒█▀▀▀█ █░░█ ░▀░ ▀▀█▀▀ █▀▀ █▀▀▄ █░░█ █▀▀█ █▀▀▄ 
▒█▒█▒█ █▀▀ █░░ █░░ █░░█ █░▀░█ █▀▀ 　 ░░█░░ █░░█ 　 ░▀▀▀▄▄ █▀▀█ ▀█▀ ░░█░░ █▀▀ █▀▀▄ █░░█ █▄▄▀ █░░█ 
▒█▄▀▄█ ▀▀▀ ▀▀▀ ▀▀▀ ▀▀▀▀ ▀░░░▀ ▀▀▀ 　 ░░▀░░ ▀▀▀▀ 　 ▒█▄▄▄█ ▀░░▀ ▀▀▀ ░░▀░░ ▀▀▀ ▀▀▀░ ░▀▀▀ ▀░▀▀ ▀░░▀

[CONTROLS]
W - Move up.
S - Move down.
A - Move left.
D - Move right.

I - Inventory.

Arrow Keys - Menu navigation.
Enter - Menu select.
Backspace - Menu back/cancel.

[LEGEND]
>> Interactables (Walking into any of these will begin an interaction.)
$$ - Shitefire
(( - Obelisk
%% - NPC
!! - Storage Container
?? - Loot Pile
XX - Locked Door
-- - Passage Tile (leads to new room.)

>> Non-Interactables
## - Wall
.. - Open Door

[THE PREMISE]
You are one of many notable figure in the realm. The name 'Auld Shiteburn' has long been in your memory.
Once a normal village where shite was burned, something terrible took hold of the place, and now it has
become the heart of a great omen; if not stopped, the rotting shite may spread. On behalf of the people,
you've pledged to investigate the place and rid it of its foul curse. Save the unfortunate and purify
the land.

[INVENTORY]
Using the I key to navigate your inventory, select items to see more details about them.

[PAGE 1] Press any key to print Page 2.
";

        public const string INTRO_PAGE2 =
            @"
[GAMEPLAY]
>> Classes
Each notable figure has a class, providing 1-3 unique abilities, with offensive, defensive or utility
potential. Some classes will be stronger against certain foes. Your character is given a random class on
new game or death.

>> Abilities
Abilities provide offensive, defensive, utility, or a combination, advantages to a class in combat. For
example, the Heathen class has the ability Shite Ward, which provides nullification to Occult damage for
three combat rounds.

>> Proficiencies
Each class has proficiencies, which provide damage or resistance bonuses to weapons and armour. You will
know when you are proficient with a property (element/magic), material, or type of weapon/armour because
it will be highlighted green in your inventory.

>> Weapons
Weapons are divided into three parts: property, material, and type. Your class may be proficient in one,
two, or all parts of the item, with each proficient part highlighted in green. Certain enemies have
weaknesses to certain parts, so keep a close eye on the combat turn report as you go.

>> Status Effects & Consumables
Status Effects are boons provided to the player in combat. They come in two forms: defense and replenishment.
Defensive Status effects will either mititgate or nullify damage. Read the ability/consumable descriptions
carefully. A character has two status effect slots: Ability Status Effect and Potion Status Effect.
    Abilities used in turn-based combat will fill the Ability Status Effect slot, replacing anything currently
there. Consumables found throughout Shiteburn fill the Potion Status Effect slot when consumed from the character
inventory.
    Status Effects show their timer, reducing by one for each turn in combat. If combat ends before they deplete,
they will remain with the character into the next fight, unless replaced. Consumables cannot be used in combat.

>> Combat
Upon entering a new area, there's a chance that enemies are present. You will be warned if there are enemies ahead
and will be given the option to continue or remain in the current location. Should you enter the dangerous area,
a combat encounter begins. You will fight to the death against your enemies in turn-based combat, using your
melee weapon and abilities. Should you win the encounter by killing all the enemies, you will have your health and
resource restored.

[PAGE 2] Press any key to go to Main Menu.
";

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
            string[] messageArray = ascii.Split(Environment.NewLine);
            for (int i = 0; i < messageArray.Length; i++)
            {
                Console.CursorTop++;
                Utils.WriteColour(messageArray[i], colour);
                Console.CursorLeft -= messageArray[i].Length;
            }
        }
    }
}
