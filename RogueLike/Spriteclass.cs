using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RogueLike
{
    public static class Spriteclass
    {
        public static Texture2D ballTex;

        public static void LoadContent(ContentManager c)
        {
            ballTex = c.Load<Texture2D>("ball");
        }

    }
}
