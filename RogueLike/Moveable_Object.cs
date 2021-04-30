﻿using System;
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
        protected int speed;
        protected float speedMultiplier;
        protected Vector2 direction;
        protected bool moving;

        public float health;



        public Moveable_Object(SpriteSheet spriteSheet, double timeBetweenFrames) : base(spriteSheet, timeBetweenFrames)
        {
            speedMultiplier = 1;
        }

        

    }
}
