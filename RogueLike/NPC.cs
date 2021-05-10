using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace RogueLike
{
    class NPC: AnimatedObject
    {
        SpriteSheet textboxSheet;

        Rectangle textboxHitbox;

        public NPC(SpriteSheet spriteSheet, double timeBetweenFrames, Point hitboxSize, Vector2 pos, SpriteSheet textboxSheet, Point textboxSize) :base (spriteSheet, timeBetweenFrames)
        {
            hitbox.Size = hitboxSize;
            hitbox.Location = new Point((int)pos.X-hitboxSize.X/2, (int)pos.Y-hitboxSize.Y/2);
            middlepos = hitbox.Center.ToVector2();



            textboxHitbox = new Rectangle(hitbox.Center.X - textboxSize.X / 2, hitbox.Top - textboxSize.Y, textboxSize.X, textboxSize.Y);

            this.textboxSheet = textboxSheet;
        }


        public void Update(GameTime gameTime)
        {
            //OBS! Animeras inte?
            Animate(gameTime, 0);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(textboxSheet.texture, textboxHitbox,Color.White);

            sb.Draw(spriteSheet.texture, hitbox.Location.ToVector2(), null, new Rectangle(spriteSheet.frameSize.X * currentFrame.X, spriteSheet.frameSize.Y * currentFrame.Y, spriteSheet.frameSize.X, spriteSheet.frameSize.Y), Vector2.Zero, 0, Vector2.One, color, SpriteEffects.None, 1f);
        }
    }
}
