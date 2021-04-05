using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    class GameObject
    {
        protected SpriteSheet spriteSheet;
        //public Rectangle pos;
        public Rectangle hitbox; //är pos och hitbox densamma?
    
        public GameObject(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;
//            this.hitbox = hitbox;
        }
        //pog

    }
}
