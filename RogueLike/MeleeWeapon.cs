using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace RogueLike
{
    class MeleeWeapon:Weapon
    {
        public Rectangle damageHitbox;

        public MeleeWeapon(int hitboxLength, int hitboxWidth, int baseDamage, float baseAttackSpeed, SpriteSheet spriteSheet, SpriteSheet itemSpriteSheet, float speedMultiplier, int weight, string itemName, double timeBetweenFrames) : base(hitboxLength,hitboxWidth,baseDamage, baseAttackSpeed, spriteSheet, itemSpriteSheet, speedMultiplier, weight, itemName, timeBetweenFrames)
        {
            this.spriteSheet = spriteSheet;
            
        }


        //public override void Draw(SpriteBatch sb)
        //{
        //    sb.Draw(spriteSheet.texture, hitbox, Color.White);
        //}
    }
}
