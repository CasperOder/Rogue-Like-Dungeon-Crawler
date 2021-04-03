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
        public Tile(Texture2D tex, Rectangle pos) : base(tex)
        {
            this.pos = pos;

        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos,Color.White);
        }
    }
    
}
