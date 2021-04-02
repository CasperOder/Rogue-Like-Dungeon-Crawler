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
    public static class SpriteSheetManager
    {
        public static SpriteSheet ball { get; private set; }

        public static void LoadContent(ContentManager c)
        {
            Point sheetSize;
            Texture2D texture;
            //Ball
            sheetSize = new Point(0, 0);
            texture = c.Load<Texture2D>("ball");

            ball = new SpriteSheet(texture, sheetSize);
        }

    }
}
