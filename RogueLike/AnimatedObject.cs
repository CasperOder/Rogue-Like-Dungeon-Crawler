using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    class AnimatedObject : GameObject
    {
        private double timeBetweenFrames;
        private double timeSinceLastFrame;

        //ändra inte currentFrame utan att ställa in sequenceIndex och vise versa. Kan göras till en metod eller set property om det behövs
        public Point currentFrame { get; private set; }
        private byte sequenceIndex = 0;

        public AnimatedObject(SpriteSheet spriteSheet, double timeBetweenFrames) :
            base(spriteSheet)
        {
            this.timeBetweenFrames = timeBetweenFrames;

            currentFrame = new Point(0, 0);
        }

        /// <summary>
        /// animationIndex is used to to choose what animation sequence is used from the SpriteSheet object
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="animationIndex"></param>
        public void Animate(GameTime gameTime, int animationIndex)
        {
            if (spriteSheet.animationSequence.Count != 0 && spriteSheet.animationSequence.Count > animationIndex)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

                if (timeSinceLastFrame >= timeBetweenFrames)
                {
                    timeSinceLastFrame -= timeBetweenFrames;

                    currentFrame = spriteSheet.animationSequence[animationIndex][sequenceIndex];

                    if (sequenceIndex == spriteSheet.animationSequence[animationIndex].Count() - 1)
                        sequenceIndex = 0;
                    else
                        sequenceIndex++;
                }
            }
            else
                Console.WriteLine("Det finns ingen animation med index ´" + animationIndex + "´ för " + spriteSheet.texture.Name);
        }

        public void ResetFrame()
        {
            currentFrame = new Point(0, 0);
            sequenceIndex = 0;
        }
    }
}
