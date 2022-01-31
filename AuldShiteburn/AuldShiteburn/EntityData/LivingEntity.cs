namespace AuldShiteburn.EntityData
{
    internal class LivingEntity : Entity
    {
        string name;
        private float maxHP, hp, maxStamina, stamina, maxMana, mana;
        public float HP
        {
            get { return hp; }
            set
            {
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
