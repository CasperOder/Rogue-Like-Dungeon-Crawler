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

        /// <summary>
        /// Creates a Ladder which can warp the player to a new location.
        /// </summary>
        /// <param name="spriteSheet">Which spritesheet the Ladder utilize.</param>
        /// <param name="pos">Position of the Ladder.</param>
        /// <param name="exitPos">New position upon entering the ladder.</param>
        /// <param name="enterText">Text below the ladder before entering.</param>
        public Ladder(SpriteSheet spriteSheet, Vector2 pos, Vector2 exitPos, string enterText) : base(spriteSheet)
        {
            hitbox.Size = new Point(Constants.tileSize * 3, Constants.tileSize * 3);
            middlepos = pos;
            hitbox.Location = middlepos.ToPoint() - new Point(hitbox.Width / 2, hitbox.Height / 2);
            this.enterText = enterText;

            newPlayerPos = exitPos;
        }

        /// <summary>
        /// Moves the specific target to the ladder's exit position.
        /// </summary>
        /// <param name="player"></param>
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
