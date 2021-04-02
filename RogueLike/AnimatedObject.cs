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

        //ändra inte currentFrame utan att ställa in sequenceIndex och vise versa 
        public Point currentFrame { get; private set; }
        private byte sequenceIndex = 0;

        public AnimatedObject(SpriteSheet spriteSheet, double timeBetweenFrames) :
            base(spriteSheet)
        {
            this.timeBetweenFrames = timeBetweenFrames;

            currentFrame = new Point(0, 0);
        }

        public void Animate(GameTime gameTime, int animationIndex)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

            if (spriteSheet.animationSequence.Count != 0)
            {
                if (timeSinceLastFrame >= timeBetweenFrames)
                {
                    timeSinceLastFrame -= timeBetweenFrames;

                    if (currentFrame != spriteSheet.animationSequence[animationIndex].Last())
                        currentFrame = spriteSheet.animationSequence[animationIndex][sequenceIndex];
                    else
                    {
                        currentFrame = spriteSheet.animationSequence[animationIndex].First();
                    }

                    if (sequenceIndex == spriteSheet.animationSequence[animationIndex].Count())
                        sequenceIndex = 0;
                    else
                        sequenceIndex++;
                }
            }
            else
                Console.WriteLine("Spriten har ingen animation");
        }

        public void ResetFrame()
        {
            currentFrame = new Point(0, 0);
            sequenceIndex = 0;
        }
    }
}
