using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueLike
{
    public class Camera
    {
        private Matrix transform;
        private Vector2 pos;
        private Viewport view;

        public Matrix Transform
        {
            get { return transform; }
        }


        public Camera(Viewport view)
        {
            this.view = view;
        }


        public void SetPosition(Vector2 pos)
        {
            this.pos = pos;
            transform = Matrix.CreateTranslation(-pos.X + view.Width / 2, -pos.Y + view.Height / 2, 0);
        }
    }
}