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
        public Texture2D tex;
        //public Rectangle pos;
        public Rectangle hitbox; //är pos och hitbox densamma?
    
        public GameObject(Texture2D tex, Rectangle hitbox)
        {
            this.tex = tex;
            this.hitbox = hitbox;
        }
        //pog

    }
}
