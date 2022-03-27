using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData.PlayerData
{
    [Serializable]
    internal struct PlayerGenerationData
    {
        public static List<string> HeathenTitles { get; } = new List<string>()
        {
            "Foundling",
            "Apostate",
            "Warlock",
            "Shitebirthed"
        };
        public static List<string> FighterTitlesMale { get; } = new List<string>()
        {
            "Sir",
            "Baron",
            "Duke",
            "King"
        };
        public static List<string> FighterTitlesFemale { get; } = new List<string>()
        {
            "Dame",
            "Baroness",
            "Duchess",
            "Queen"
        };
        public static List<string> MarauderTitles { get; } = new List<string>()
        {
            "Bloody",
            "Iron",
            "Brutal",
            "Chieftain"
        };
        public static List<string> MonkTitlesMale { get; } = new List<string>()
        {
            "Brother",
            "Reverend",
            "Father",
            "Bishop"
        };
        public static List<string> MonkTitlesFemale { get; } = new List<string>()
        {
            "Sister",
            "Nun",
            "Mother",
            "Abbess"
        };
        public static List<string> RogueTitles { get; } = new List<string>()
        {
            "Cunning",
            "Sly",
            "Ratbag",
            "Cutthroat"
        };
        public static List<string> NamesMale { get; } = new List<string>()
        {
            "Abrecan",
            "Aethelstan",
            "Aelfred",
            "Bearn",
            "Beowulf",
            "Bowdyn",
            "Faran",
            "Gimm",
            "Isen",
            "Offa",
            "Weir"
        };
        public static List<string> NameFemale { get; } = new List<string>()
        {
            "Aethelflaed",
            "Arianrod",
            "Bodicia",
            "Cwene",
            "Diera",
            "Eldrida",
            "Elswyth",
            "Odelyn",
            "Sunn",
            "Titha",
            "Wendelin"
        };
    }
}
