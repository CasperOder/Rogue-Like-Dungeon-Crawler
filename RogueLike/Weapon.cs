using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueLike
{
    class Weapon:AnimatedObject
    {
        public int baseDamage;
        public int baseRange;
        public int weight;
        public float baseAttackSpeed;
        public float damageMultiplyier;
        public float rangeMultiplyier;
        public float attackSpeedMultiplyier;
        public float speedMultiplier; //ändrar spelarens hastighet under attacken
        public SpriteSheet itemSpriteSheet;
        public string itemName;

        //public Rectangle hitbox;

        //public SpriteSheet spriteSheet;

        public int hitboxLength, hitboxWidth; //Length is how far FROM the player the weapon points.

        public Weapon(int hitboxLength,int hitboxWidth, int baseDamage, float baseAttackSpeed, SpriteSheet spriteSheet, SpriteSheet itemSpriteSheet, float speedMultiplier, int weight, string itemName, double timeBetweenFrames) :base(spriteSheet, timeBetweenFrames)
        {
            this.hitboxLength = hitboxLength;
            this.hitboxWidth = hitboxWidth;
            this.baseAttackSpeed = baseAttackSpeed;
            this.baseDamage = baseDamage;
            this.itemSpriteSheet = itemSpriteSheet;
            this.speedMultiplier = speedMultiplier;
            this.weight = weight;
            this.itemName = itemName;
            damageMultiplyier = 1;
            attackSpeedMultiplyier = 1;
        }


    }
}
