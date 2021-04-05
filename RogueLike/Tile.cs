using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueLike
{
    class Tile : GameObject
    {
        Rectangle pos;
        public Tile(SpriteSheet spriteSheet, Rectangle pos) : base(spriteSheet)
        {
            this.pos = pos;

        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, pos,Color.White);
        }
    }
    
}
