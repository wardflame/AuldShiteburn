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
        protected float maxHP, maxStamina, maxMana;
        public float HP
        {
            get { return hp; }
            set
            {
                hp = value;
                if (value > maxHP)
                {
                    hp = maxHP;
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
                if (value > maxStamina)
                {
                    stamina = maxStamina;
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
                if (value > maxMana)
                {
                    mana = maxMana;
                }
                else if (value < 0)
                {
                    mana = 0;
                }
            }
        }
    }
}
