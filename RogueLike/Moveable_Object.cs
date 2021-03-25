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
    class Moveable_Object : GameObject
    {
        protected int speed;
        protected bool hasMoved;



        public Moveable_Object(Texture2D tex, Rectangle hitbox) : base(tex, hitbox)
        {

        }

        

    }
}
