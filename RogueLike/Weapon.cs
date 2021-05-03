using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    class Weapon
    {
        public int baseDamage;
        public int baseRange;
        public float baseAttackSpeed;
        public float damageMultiplyier;
        public float rangeMultiplyier;
        public float attackSpeedMultiplyier;
        public float speedMultiplier; //ändrar spelarens hastighet under attacken
        public SpriteSheet itemSpriteSheet;

        public Weapon(int baseDamage, float baseAttackSpeed, SpriteSheet itemSpriteSheet, float speedMultiplier)
        {
            this.baseAttackSpeed = baseAttackSpeed;
            this.baseDamage = baseDamage;
            this.itemSpriteSheet = itemSpriteSheet;
            this.speedMultiplier = speedMultiplier;
            damageMultiplyier = 1;
            attackSpeedMultiplyier = 1;
        }


    }
}
