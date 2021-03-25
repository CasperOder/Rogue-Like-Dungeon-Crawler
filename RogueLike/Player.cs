using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    class Player : Moveable_Object
    {
        Rectangle playerStartPos;

        public Player(Texture2D tex, Rectangle hitbox) : base(tex, hitbox)
        {

        }

        public void Movement()
        {
            if (!hasMoved)
            {
                playerStartPos = hitbox;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) //left
            {
                //Game1.GetPlayerCollision(new Rectangle(hitbox.X - speed, hitbox.Y, tileSize, tileSize));

                //if (!isColliding)
                {
                    hitbox.X -= speed;
                    hasMoved = true;



                    //isColliding = false;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right)) //right
            {
                //Game1.GetPlayerCollision(new Rectangle(hitbox.X + speed, hitbox.Y, tileSize, tileSize));

                //if (!isColliding)
                {
                    hitbox.X += speed;
                    hasMoved = true;



                    //isColliding = false;
                }
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Down)) //down
            {
                hasMoved = true;
            }
            //else
            //{
            //    isColliding = false;

            //}
        }

    }
}
