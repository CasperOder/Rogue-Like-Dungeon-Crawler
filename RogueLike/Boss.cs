using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    abstract class Boss : Moveable_Object
    {
        protected Vector2 position;

        //public bool beenHit;
        public bool alive = false;

        public Boss(SpriteSheet spriteSheet, double timeBetweenFrames, float health, float maxHealth) :
            base(spriteSheet, timeBetweenFrames, health, maxHealth)
        {

        }

        public abstract void Update(GameTime gametime);

        public void Movement(Vector2 direction, GameTime gameTime)
        {
            position += speed * direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void SetPosition(Vector2 newPos)
        {
            position = new Vector2(newPos.X - spriteSheet.frameSize.X / 2, newPos.Y - spriteSheet.frameSize.Y / 2);
        }

        public override void Draw(SpriteBatch sb)
        {
            ChangeDamagedColor();

            sb.Draw(spriteSheet.texture, position, null, new Rectangle(spriteSheet.frameSize.X * currentFrame.X, spriteSheet.frameSize.Y * currentFrame.Y, spriteSheet.frameSize.X, spriteSheet.frameSize.Y), Vector2.Zero, 0, Vector2.One, color, SpriteEffects.None, 1f);
        }
    }
}
