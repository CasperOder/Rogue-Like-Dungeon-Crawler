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
        Color playerColor=Color.White;
        public static bool isColliding = false;

        Weapon equippedWeapon;

        public Player(SpriteSheet spriteSheet, Vector2 startPos, double timeBetweenFrames) : base(spriteSheet, timeBetweenFrames)
        {
            hitbox.Size = spriteSheet.frameSize;
            hitbox.X = (int)startPos.X - hitbox.Width/2;
            hitbox.Y = (int)startPos.Y - hitbox.Height/2;
            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);

            speed = 10;
        }

        public void Movement(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                TileCollisionHandler(new Rectangle(hitbox.X - speed, hitbox.Y - speed, hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.X -= (int)(Math.Sqrt(0.5) * speed);
                    hitbox.Y -= (int)(Math.Sqrt(0.5) * speed); ;
                    moving = true;
                    isColliding = false;
                }
                else
                {
                    TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y - speed, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.Y -= speed;
                        moving = true;
                        isColliding = false;
                    }
                    TileCollisionHandler(new Rectangle(hitbox.X - speed, hitbox.Y, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.X -= speed;
                        moving = true;
                        isColliding = false;
                    }
                }
                //direction = new Vector2(-(float)Math.Sqrt(0.5),-(float)Math.Sqrt(0.5));
                //moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                TileCollisionHandler(new Rectangle(hitbox.X + speed, hitbox.Y - speed, hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.X += (int)(Math.Sqrt(0.5) * speed); ;
                    hitbox.Y -= (int)(Math.Sqrt(0.5) * speed); ;
                    moving = true;
                    isColliding = false;
                }
                else
                {
                    TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y - speed, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.Y -= speed;
                        moving = true;
                        isColliding = false;
                    }
                    TileCollisionHandler(new Rectangle(hitbox.X + speed, hitbox.Y, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.X += speed;
                        moving = true;
                        isColliding = false;
                    }
                }
                //direction = new Vector2((float)Math.Sqrt(0.5), -(float)Math.Sqrt(0.5));
                //moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                TileCollisionHandler(new Rectangle(hitbox.X + speed, hitbox.Y + speed, hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.X += (int)(Math.Sqrt(0.5) * speed); ;
                    hitbox.Y += (int)(Math.Sqrt(0.5) * speed); ;
                    moving = true;
                    isColliding = false;
                }
                else
                {
                    TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y + speed, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.Y += speed;
                        moving = true;
                        isColliding = false;
                    }
                    TileCollisionHandler(new Rectangle(hitbox.X + speed, hitbox.Y, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.X += speed;
                        moving = true;
                        isColliding = false;
                    }
                }
                //direction = new Vector2((float)Math.Sqrt(0.5), (float)Math.Sqrt(0.5));
                //moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                TileCollisionHandler(new Rectangle(hitbox.X - speed, hitbox.Y + speed, hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.X -= (int)(Math.Sqrt(0.5) * speed); ;
                    hitbox.Y += (int)(Math.Sqrt(0.5) * speed); ;
                    moving = true;
                    isColliding = false;
                }
                else
                {
                    TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y + speed, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.Y += speed;
                        moving = true;
                        isColliding = false;
                    }
                    TileCollisionHandler(new Rectangle(hitbox.X - speed, hitbox.Y, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.X -= speed;
                        moving = true;
                        isColliding = false;
                    }
                }
                //direction = new Vector2(-(float)Math.Sqrt(0.5), (float)Math.Sqrt(0.5));
                //moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left)) //left
            {
                TileCollisionHandler(new Rectangle(hitbox.X - speed, hitbox.Y, hitbox.Width, hitbox.Height));

                if (!isColliding)
                {
                    hitbox.X -= speed;
                    moving = true;
                    isColliding = false;
                }
                //direction = new Vector2(-1,0);
                //moving = true;                
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right)) //right
            {
                TileCollisionHandler(new Rectangle(hitbox.X + speed, hitbox.Y, hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.X += speed;
                    moving = true;
                    isColliding = false;
                }
                //direction = new Vector2(1,0);
                //moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down)) //down
            {
                TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y + speed, hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.Y += speed;
                    moving = true;
                    isColliding = false;
                }
                //direction = new Vector2(0,1);
                //moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))//up
            {
                TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y - speed, hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.Y -= speed;
                    moving = true;
                    isColliding = false;
                }
                //direction = new Vector2(0,-1);
                //moving = true;
            }            
            else if(Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                direction = Vector2.Zero;
                moving = false;
            }
            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);

            if (moving)
            {
                //if (!isColliding)
                //{
                //    middlepos += speed * direction;
                //    hitbox.Location = new Point((int)middlepos.X, (int)middlepos.Y) - new Point(hitbox.Width / 2, hitbox.Height / 2);
                //}
                
                
                ResetFrame();
            }
            else
            {
                Animate(gameTime, 0);
            }
        }

        public void TileCollisionHandler (Rectangle rect)
        {
            
            
            for (int i = 0; i < Room.wallTiles.Count; i++)
            {
                if (Room.wallTiles[i].hitbox.Intersects(rect))
                {
                    isColliding = true;
                    break;
                }
                else if (i == Room.wallTiles.Count - 1)
                {
                    isColliding = false;
                        
                }
            }
            
        //    Vector2 diff = (middlepos - t.middlepos) / Vector2.Distance(middlepos, t.middlepos);
        //    middlepos += diff * speed;
        //    hitbox.Location = new Point((int)middlepos.X, (int)middlepos.Y) - new Point(hitbox.Width / 2, hitbox.Height / 2);
        //    moving = false;
        }



        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox.Location.ToVector2(), null, new Rectangle(spriteSheet.frameSize.X * currentFrame.X, spriteSheet.frameSize.Y * currentFrame.Y, spriteSheet.frameSize.X, spriteSheet.frameSize.Y), Vector2.Zero, 0, Vector2.One, playerColor, SpriteEffects.None, 1f);
        }

    }
}