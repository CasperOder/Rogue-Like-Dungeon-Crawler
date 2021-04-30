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
    class Enemy:Moveable_Object
    {
        public int enemySpottingRange;

        public Color enemyColor;
        public bool beenHit;

        public Enemy(SpriteSheet spriteSheet, double timeBetweenFrames, Vector2 startPos):base (spriteSheet, timeBetweenFrames)
        {
            hitbox.Size = spriteSheet.frameSize;
            hitbox.X = (int)startPos.X - hitbox.Width / 2;
            hitbox.Y = (int)startPos.Y - hitbox.Height / 2;

            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);
        }

        public void Movement(Player p, GameTime gameTime)
        {
            enemyColor = Color.Red;
        }


        public float GetPlayerDistance (Player p)
        {
            float playerDistance = Vector2.Distance(middlepos,p.middlepos);

            return playerDistance;
        }



        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox, enemyColor);
        }

    }
}
