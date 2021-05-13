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
    class Moveable_Object : AnimatedObject
    {
        public int speed;
        protected float speedMultiplier;
        protected Vector2 direction;
        protected bool moving;

        public float maxHealth, health;

        public enum CardinalDirection
        {
            up,
            down,
            right,
            left
        }

        public Moveable_Object(SpriteSheet spriteSheet, double timeBetweenFrames) : base(spriteSheet, timeBetweenFrames)
        {
            speedMultiplier = 1;
        }
        public Moveable_Object(SpriteSheet spriteSheet, double timeBetweenFrames, float health, float maxHealth) : base(spriteSheet, timeBetweenFrames)
        {
            speedMultiplier = 1;
            this.health = health;
            this.maxHealth = maxHealth;
        }

        

    }
}
