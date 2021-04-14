using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueLike
{
    class DummyEnemy:Enemy
    {


        public DummyEnemy(SpriteSheet spriteSheet, double timeBetweenFrames, Vector2 startPos) :base(spriteSheet, timeBetweenFrames, startPos)
        {
            enemyColor = Color.White;
            enemySpottingRange = 500;
        }






    }
}
