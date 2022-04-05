using AuldShiteburn.CombatData;
using AuldShiteburn.CombatData.PayloadData;
using System;

namespace AuldShiteburn.EntityData
{
    [Serializable]
    internal abstract class LivingEntity : Entity
    {
        public string Name { get; protected set; }
        public bool UsesStamina { get; set; }
        public bool UsesMana { get; set; }
        protected float hp, stamina, mana;
        public float MaxHP { get; protected set; }
        public float MaxStamina { get; protected set; }
        public float MaxMana { get; protected set; }
        public float HP
        {
            get { return hp; }
            set
            {
                hp = value;
                if (value > MaxHP)
                {
                    hp = MaxHP;
                }
                else if (value < 0)
                {
                    hp = 0;
                }
            }
        }
        public float Stamina
        {
            get { return stamina; }
            set
            {
                stamina = value;
                if (value > MaxStamina)
                {
                    stamina = MaxStamina;
                }
                else if (value < 0)
                {
                    stamina = 0;
                }
            }
        }
        public float Mana
        {
            get { return mana; }
            set
            {
                mana = value;
                if (value > MaxMana)
                {
                    mana = MaxMana;
                }
                else if (value < 0)
                {
                    mana = 0;
                }
            }
        }
        public bool Stunned => StunTimer > 0 ? true : false;
        public int StunCap { get; set; }
        private int stunTimer;
        public int StunTimer
        {
            get
            {
                return stunTimer;
            }
            set
            {
                stunTimer = value;
                if (stunTimer < 0) stunTimer = 0;
                if (stunTimer > StunCap) stunTimer = StunCap;
            }
        }

        /// <summary>
        /// Take a damage payload and calculate it against the enitity's
        /// resistances (where applicable). Mitigate damage and then
        /// reduce the enitity's HP by the remaining damage.
        /// </summary>
        /// <param name="incomingDamage">Damage payload to process.</param>
        /// <param name="offsetY">Potentially required for UI placement.</param>
        /// <returns>Returns true if the entity died.</returns>
        public virtual bool ReceiveAttack(CombatPayload attackPayload, int offsetY = 0)
        {
            return false;
        }
    }
}
