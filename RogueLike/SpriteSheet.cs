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
        public Point frameSize;

        public List<Point[]> animationSequence { get; } = new List<Point[]>();

        //Animated sprite
        public SpriteSheet(Texture2D texture, Point sheetSize, List<Point[]> animationSequence)
        {
            this.texture = texture;
            this.sheetSize = sheetSize;

            this.animationSequence = animationSequence;

            frameSize = new Point(texture.Width / (sheetSize.X + 1), texture.Height / (sheetSize.Y + 1));
        }
        //Single frame sprite
        public SpriteSheet(Texture2D texture, Point sheetSize)
        {
            this.texture = texture;
            this.sheetSize = sheetSize;

            frameSize = new Point(texture.Width / (sheetSize.X + 1), texture.Height / (sheetSize.Y + 1));
        }
    }
}
