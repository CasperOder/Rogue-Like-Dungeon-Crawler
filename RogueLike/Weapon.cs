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

        public void Draw(SpriteBatch sb, Moveable_Object.CardinalDirection direction)
        {
            switch (direction)
            {
                case Moveable_Object.CardinalDirection.up:
                    sb.Draw(spriteSheet.texture, hitbox.Location.ToVector2(), null, new Rectangle(spriteSheet.frameSize.X * currentFrame.X, spriteSheet.frameSize.Y * currentFrame.Y, spriteSheet.frameSize.X, spriteSheet.frameSize.Y), new Vector2(spriteSheet.frameSize.X / 2, spriteSheet.frameSize.Y / 2), 0, Vector2.One, color, SpriteEffects.None, 1f);
                    break;
                case Moveable_Object.CardinalDirection.down:
                    sb.Draw(spriteSheet.texture, hitbox.Location.ToVector2(), null, new Rectangle(spriteSheet.frameSize.X * currentFrame.X, spriteSheet.frameSize.Y * currentFrame.Y, spriteSheet.frameSize.X, spriteSheet.frameSize.Y), new Vector2(spriteSheet.frameSize.X / 2, spriteSheet. frameSize.Y / 2), (float)Math.PI / 2f * 2, Vector2.One, color, SpriteEffects.None, 1f);

                    break;
                case Moveable_Object.CardinalDirection.right:
                    sb.Draw(spriteSheet.texture, hitbox.Location.ToVector2(), null, new Rectangle(spriteSheet.frameSize.X * currentFrame.X, spriteSheet.frameSize.Y * currentFrame.Y, spriteSheet.frameSize.X, spriteSheet.frameSize.Y), new Vector2(spriteSheet.frameSize.X / 2, spriteSheet.frameSize.Y / 2), (float)Math.PI / 2f, Vector2.One, color, SpriteEffects.None, 1f);

                    break;
                case Moveable_Object.CardinalDirection.left:
                    sb.Draw(spriteSheet.texture, hitbox.Location.ToVector2(), null, new Rectangle(spriteSheet.frameSize.X * currentFrame.X, spriteSheet.frameSize.Y * currentFrame.Y, spriteSheet.frameSize.X, spriteSheet.frameSize.Y), new Vector2(spriteSheet.frameSize.X / 2, spriteSheet.frameSize.Y / 2), (float)Math.PI / 2f * 3, Vector2.One, color, SpriteEffects.None, 1f);


                    break;
            }
        }
    }
}
