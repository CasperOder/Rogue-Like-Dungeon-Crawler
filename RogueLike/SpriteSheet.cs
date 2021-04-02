using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public class SpriteSheet
    {
        public Texture2D texture { get; }

        public Point sheetSize { get; }
        public Point frameSize { get; }

        public SpriteSheet(Texture2D texture, Point sheetSize)
        {
            this.texture = texture;
            this.sheetSize = sheetSize;

            frameSize = new Point(texture.Width / (sheetSize.X + 1), texture.Height / (sheetSize.Y + 1));
        }
    }
}
