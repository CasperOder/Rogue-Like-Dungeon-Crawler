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
    class Ladder : GameObject
    {
        public bool showText;
        string enterText;

        Vector2 newPlayerPos;

        public Ladder(SpriteSheet spriteSheet, Vector2 pos, Vector2 exitPos, string enterText) : base(spriteSheet)
        {
            hitbox.Size = new Point(Constants.tileSize * 3, Constants.tileSize * 3);
            middlepos = pos;
            hitbox.Location = middlepos.ToPoint() - new Point(hitbox.Width / 2, hitbox.Height / 2);
            this.enterText = enterText;

            newPlayerPos = exitPos;

        }

        public void Moveplayer(Player player)
        {
            player.SetPlayerPosition(newPlayerPos);
        }


        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox, Color.White);
            if(showText)
            {
                sb.DrawString(Level.itemFont, enterText, new Vector2(hitbox.Left, hitbox.Bottom), Color.White);
            }

        }


    }
}
