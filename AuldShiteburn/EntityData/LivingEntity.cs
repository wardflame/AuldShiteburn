namespace AuldShiteburn.EntityData
{
    internal abstract class LivingEntity : Entity
    {
        public string name;
        private float hp, stamina, mana;
        public float maxHP, maxStamina, maxMana;
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
