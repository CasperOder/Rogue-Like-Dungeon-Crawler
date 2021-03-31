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

        Color playerColor=Color.White;

        public Player(Texture2D tex, Vector2 startPos) : base(tex)
        {
            hitbox.Width = tex.Width;
            hitbox.Height = tex.Height;
            hitbox.X = (int)startPos.X;
            hitbox.Y = (int)startPos.Y;
            speed = 10;
            
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
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) //right
            {
                //Game1.GetPlayerCollision(new Rectangle(hitbox.X + speed, hitbox.Y, tileSize, tileSize));

                //if (!isColliding)
                {
                    hitbox.X += speed;
                    hasMoved = true;



                    //isColliding = false;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) //down
            {
                hitbox.Y += speed;
                hasMoved = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))//up
            {
                hitbox.Y -= speed;
                hasMoved = true;
            }            
            if(Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                hasMoved = false;
            }

            if(hasMoved)
            {
                playerColor = Color.White;
            }
            else
            {
                playerColor = Color.Black;
            }


            //else
            //{
            //    isColliding = false;

            //}
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, hitbox, playerColor);
        }

    }
}
