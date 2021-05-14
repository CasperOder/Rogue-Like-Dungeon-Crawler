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
        public Vector2 startPos;
        public int damage;
        public float damageMultiplyier;
        public bool destroy = false;

        float rangeMultiplyier;
        int range;

        public int hitboxLength, hitboxWidth;

        //När en projectile template skapas i LoadWeaponsAndItems
        public Projectile(int hitboxLength, int hitboxWidth ,SpriteSheet spriteSheet, double timeBetweenFrames, int speed) :base(spriteSheet, timeBetweenFrames)
        {
            this.speed = speed;
            this.hitboxLength = hitboxLength;
            this.hitboxWidth = hitboxWidth;
        }

        //När en ny projectile skapas för bruk
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
        
        public void Update()
        {
            middlepos += speed * direction;
            hitbox.Location = new Point((int)middlepos.X, (int)middlepos.Y) - new Point(hitbox.Width / 2, hitbox.Height / 2);

            if (Vector2.Distance(startPos, middlepos) > range * rangeMultiplyier)
            {
                destroy = true;
            }

            foreach(Tile t in Room.wallTiles)
            {
                if(t.hitbox.Intersects(hitbox))
                {
                    destroy = true;
                }
            }
        }

        public void CheckEnemyCollision(Enemy e)
        {
            if (e.hitbox.Intersects(hitbox))
            {
                destroy = true;
                e.health -= (damage*damageMultiplyier);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox, Color.White);
        }

    }
}
