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
    class Projectile:Moveable_Object
    {
        int gameticksTillWallCheck;
        public Vector2 startPos;
        public int damage;
        public float damageMultiplyier;
        
        float rangeMultiplyier;
        int range;

        public int hitboxLength, hitboxWidth;

        /// <summary>
        /// Template of a Projectile
        /// </summary>
        /// <param name="hitboxLength">Length of the hitbox</param>
        /// <param name="hitboxWidth">Width of the hitbox</param>
        /// <param name="spriteSheet">Spritesheet the Projectile uses</param>
        /// <param name="timeBetweenFrames">Time between frames, measured in seconds</param>
        /// <param name="speed">Traveling speed of the Projectile</param>
        public Projectile(int hitboxLength, int hitboxWidth ,SpriteSheet spriteSheet, double timeBetweenFrames, int speed) :base(spriteSheet, timeBetweenFrames)
        {
            this.speed = speed;
            this.hitboxLength = hitboxLength;
            this.hitboxWidth = hitboxWidth;
        }

        /// <summary>
        /// Creates an instance of a Projectile
        /// </summary>
        /// <param name="hitboxLength">Length of the hitbox</param>
        /// <param name="hitboxWidth">Width of the hitbox</param>
        /// <param name="spriteSheet">Spritesheet the Projectile uses</param>
        /// <param name="timeBetweenFrames">Time between frames, measured in seconds</param>
        /// <param name="speed">Traveling speed of the Projectile</param>
        /// <param name="startPos">Location the Projectile origins from</param>
        /// <param name="cardinalDirection">Direction of the Projectile</param>
        /// <param name="damage">Amount of damage the Projectile will deal upon impact</param>
        /// <param name="range">The maximal range the Projectile will reach before being destroyed.</param>
        /// <param name="rangeMultiplyier">Multiplies the maximal range.</param>
        /// <param name="damageMultiplyier">Multiplies the damage dealt.</param>
        public Projectile(int hitboxLength, int hitboxWidth, SpriteSheet spriteSheet, double timeBetweenFrames, int speed, Vector2 startPos, CardinalDirection cardinalDirection, int damage, int range, float rangeMultiplyier, float damageMultiplyier) : base(spriteSheet, timeBetweenFrames)
        {
            switch(cardinalDirection)
            {
                case CardinalDirection.up:
                    direction = new Vector2(0, -1);
                    hitbox.Size = new Point(hitboxWidth, hitboxLength);
                    break;
                case CardinalDirection.down:
                    direction = new Vector2(0, 1);
                    hitbox.Size = new Point(hitboxWidth, hitboxLength);
                    break;
                case CardinalDirection.left:
                    direction = new Vector2(-1, 0);
                    hitbox.Size = new Point(hitboxLength, hitboxWidth);
                    break;
                case CardinalDirection.right:
                    direction = new Vector2(1, 0);
                    hitbox.Size = new Point( hitboxLength, hitboxWidth);
                    break;
            }

            this.startPos = startPos;
            middlepos = startPos;
            hitbox.X = (int)middlepos.X - hitbox.Width / 2;
            hitbox.Y = (int)middlepos.Y - hitbox.Height / 2;
            this.speed = speed;
            this.damage = damage;
            this.range = range;
            this.rangeMultiplyier = rangeMultiplyier;
            this.damageMultiplyier = damageMultiplyier;
        }
        
        /// <summary>
        /// Updates the position of the Projectile
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            middlepos += speed * direction;
            hitbox.Location = new Point((int)middlepos.X, (int)middlepos.Y) - new Point(hitbox.Width / 2, hitbox.Height / 2);

            if (Vector2.Distance(startPos, middlepos) > range * rangeMultiplyier)
            {
                isColliding = true;
            }

            gameticksTillWallCheck++;
            if (gameticksTillWallCheck>3)
            {
                gameticksTillWallCheck = 0;

                TileCollisionHandler(hitbox);
                if(isColliding)
                {
                    isColliding = true;
                }
            }
            Animate(gameTime, 0);
        }

        /// <summary>
        /// Checks if the Projectile can inflict damage to the assigned target.
        /// </summary>
        /// <param name="target">Specific target to damage check.</param>
        public void CheckTargetCollision(Moveable_Object target)
        {
            if (target.hitbox.Intersects(hitbox))
            {
                isColliding = true;
                target.health -= (damage*damageMultiplyier);
                target.damaged = true;
            }
        }        
    }
}
