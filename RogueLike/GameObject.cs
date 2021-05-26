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
        public SpriteSheet spriteSheet;
        public Rectangle hitbox;
        public Vector2 middlepos;

        /// <summary>
        /// Basics which almost all games objects inherits from.
        /// </summary>
        /// <param name="spriteSheet">Spritesheet the GameObject utilize.</param>
        public GameObject(SpriteSheet spriteSheet)
        {
            this.spriteSheet = spriteSheet;
        }

        public GameObject()
        {

        }
        //pog
    }
}
