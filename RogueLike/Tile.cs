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
        public bool solid { get; private set; }
        public Tile(SpriteSheet spriteSheet, Rectangle pos) : base(spriteSheet)
        {
            this.hitbox = pos;
            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);
            solid = true;
        }
        public Tile(SpriteSheet spriteSheet, Rectangle pos, bool solid) : base(spriteSheet)
        {
            this.hitbox = pos;
            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);
            this.solid = solid;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox,Color.White);
        }
    }
    
}
