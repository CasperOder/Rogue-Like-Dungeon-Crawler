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
        public Rectangle hitbox;

        public SpriteSheet spriteSheet;

        public int hitboxLength, hitboxWidth; //Length is how far FROM the player the weapon points.

        public MeleeWeapon(int hitboxLength, int hitboxWidth, int baseDamage, float baseAttackSpeed, SpriteSheet spriteSheet, SpriteSheet itemSpriteSheet, float speedMultiplier, int weight, string itemName) : base(baseDamage, baseAttackSpeed, itemSpriteSheet, speedMultiplier, weight, itemName)
        {
            this.spriteSheet = spriteSheet;
            this.hitboxLength = hitboxLength;
            this.hitboxWidth = hitboxWidth;
            //hitbox = new Rectangle(-100, -100, hitboxWidth, hitboxLength);

        }
        
    



    }
}
