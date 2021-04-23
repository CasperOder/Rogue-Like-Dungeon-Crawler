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

        public Projectile(Vector2 startPos ,int damage, SpriteSheet spriteSheet, double timeBetweenFrames) :base(spriteSheet, timeBetweenFrames)
        {
            this.startPos = startPos;
            hitbox.Size = spriteSheet.frameSize;
            middlepos = startPos;
            hitbox.X = (int)middlepos.X - hitbox.Width / 2;
            hitbox.Y = (int)middlepos.Y - hitbox.Height / 2;
        }

        public void Update()
        {
            middlepos += speed * direction;
            hitbox.Location = new Point((int)middlepos.X, (int)middlepos.Y) - new Point(hitbox.Width / 2, hitbox.Height / 2);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox, Color.White);
        }

    }
}
