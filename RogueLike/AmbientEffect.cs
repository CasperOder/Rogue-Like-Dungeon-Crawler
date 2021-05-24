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
    class AmbientEffect: Moveable_Object
    {
        public bool destroy;
        double timeTillGone;

        public AmbientEffect(SpriteSheet spriteSheet, double timeBetweenFrames, Vector2 direction, Vector2 pos, int speed, float opacity):base (spriteSheet, timeBetweenFrames)
        {
            colorOpacity = opacity;
            this.direction = direction;
            this.speed = speed;

            destroy = false;
            middlepos = pos;
            hitbox.Size = spriteSheet.frameSize;
            hitbox.Location = new Point((int)middlepos.X - hitbox.Size.X / 2, (int)middlepos.Y - hitbox.Size.Y / 2);

            timeTillGone = timeBetweenFrames * (spriteSheet.sheetSize.X+1);
        }

        public void Update(GameTime gameTime)
        {
            middlepos += speed * direction;
            hitbox.Location = new Point((int)middlepos.X - hitbox.Size.X / 2, (int)middlepos.Y - hitbox.Size.Y / 2);

            timeTillGone -= gameTime.ElapsedGameTime.TotalSeconds;
            if(timeTillGone<=0)
            {
                destroy = true;
            }

            Animate(gameTime, 0);

        }        
    }
}
