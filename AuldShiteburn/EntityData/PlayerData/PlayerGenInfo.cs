using System;
using System.Collections.Generic;
using System.Text;

namespace AuldShiteburn.EntityData.PlayerData
{
    internal struct PlayerGenInfo
    {
        public enum Sex
        {
            Male,
            Female
        }

        public enum TitleMale
        {            
            King,
            Duke,
            Baron,
            Sir
        }

        public enum TitleFemale
        {
            Queen,
            Duchess,
            Baroness,
            Dame
        }

        public enum NameMale
        {
            Arthur,
            Jonathan,
            Jon,
            Edward,
            Ed,
            Charles,
            Godwin,
            Godfrey,
            Godrick,
            Darian,
            George
        }

        public enum NameFemale
        {
            Annalise,
            Anna,
            Renfrey,
            Elizabeth,
            Freya,
            Aida,
            Brook,
            Fiona,
            Isolda,
            Odilia,
            Morwen
        }
    }
}
