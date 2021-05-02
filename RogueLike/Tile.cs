﻿using System;
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
        public bool isRock;

        public Tile(SpriteSheet spriteSheet, Rectangle pos) : base(spriteSheet)
        {
            this.hitbox = pos;
            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox,Color.White);
        }
    }
    
}
