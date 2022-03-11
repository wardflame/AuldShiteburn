using System;

namespace AuldShiteburn.CombatData
{
    internal class Combat
    {
        public float hitBase = 30;
        public float armourRollLower = 0.5f;

        public float meleeAtk = 28;
        public float meleeDef = 35;
        public float damageBase = 18;
        public float damagePiercing = 4;

        public float health2 = 50;
        public float meleeAtk2 = 42;
        public float meleeDef2 = 22;
        public float armour2 = 50;

        public void TryHit()
        {
            Random rand = new Random();
            int hit = rand.Next();

            if (hit >= (hitBase + meleeAtk - meleeDef2))
            {
                //do damage
            }
        }

        public void DoDamage()
        {
            Random rand = new Random();
            float lowerArmour = armour2 * armourRollLower;
            int lowInt = (int)lowerArmour;
            int mitigation = rand.Next(lowInt, (int)armour2 + 1);
            float miti = (float)mitigation;

            damageBase /= miti;
            health2 -= damageBase;
            health2 -= damagePiercing;
        }
    }
}
