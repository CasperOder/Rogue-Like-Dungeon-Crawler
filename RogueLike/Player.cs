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



        public static MouseState mouseState;

        
        public static CardinalDirection mouseDirection;

        MeleeWeapon equippedMelee;
        RangeWeapon equippedRange;
        public static float mouseDistanceX;
        public static float mouseDistanceY;

        float weaponSpeedMultiplier;
        float damageMultiplier=1;
        float attackSpeedMultiplier=1;
        
        Vector2 middleOfMap = new Vector2(Constants.windowWidth / 2, Constants.windowHeight / 2);

        public bool isAttacking;

        float attackTime, timeTillMovable;

        public Player(SpriteSheet spriteSheet, /*Vector2 startPos,*/ double timeBetweenFrames) : base(spriteSheet, timeBetweenFrames)
        {
            hitbox.Size = spriteSheet.frameSize;
            //hitbox.X = (int)startPos.X - hitbox.Width / 2;
            //hitbox.Y = (int)startPos.Y - hitbox.Height / 2;
            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);
            maxHealth = 100;
            health = maxHealth;
            
            speed = 10;

        }

        public void Movement(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mouseDistanceX = mouseState.Position.X - middleOfMap.X;
            mouseDistanceY = mouseState.Position.Y - middleOfMap.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) || mouseState.LeftButton == ButtonState.Pressed)  //vänsterklick funkar ej när man går?
            {

                if (!isAttacking)
                {
                    speedMultiplier *= weaponSpeedMultiplier;
                    isAttacking = true;
                    moving = false;
                    GetAttackingDirection();
                    if(equippedRange!=null)
                    {
                        equippedRange.CreateNewProjectile(equippedRange.middlepos, damageMultiplier, mouseDirection);
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Animate(gameTime, 3);

                TileCollisionHandler(new Rectangle(hitbox.X - (int)(speed * speedMultiplier), hitbox.Y - (int)(speed * speedMultiplier), hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.X -= (int)(Math.Sqrt(0.5) * (int)(speed * speedMultiplier));
                    hitbox.Y -= (int)(Math.Sqrt(0.5) * (int)(speed * speedMultiplier)); ;
                    moving = true;
                    isColliding = false;
                }
                else
                {
                    TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y - (int)(speed * speedMultiplier), hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.Y -= (int)(speed * speedMultiplier);
                        moving = true;
                        isColliding = false;
                    }
                    TileCollisionHandler(new Rectangle(hitbox.X - (int)(speed * speedMultiplier), hitbox.Y, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.X -= (int)(speed * speedMultiplier);
                        moving = true;
                        isColliding = false;
                    }
                }
            }
            //direction = new Vector2(-(float)Math.Sqrt(0.5),-(float)Math.Sqrt(0.5));
            //moving = true;







            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Animate(gameTime, 3);

                TileCollisionHandler(new Rectangle(hitbox.X + (int)(speed * speedMultiplier), hitbox.Y - (int)(speed * speedMultiplier), hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.X += (int)(Math.Sqrt(0.5) * (int)(speed * speedMultiplier)); ;
                    hitbox.Y -= (int)(Math.Sqrt(0.5) * (int)(speed * speedMultiplier)); ;
                    moving = true;
                    isColliding = false;
                }
                else
                {
                    TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y - (int)(speed * speedMultiplier), hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.Y -= (int)(speed * speedMultiplier);
                        moving = true;
                        isColliding = false;
                    }
                    TileCollisionHandler(new Rectangle(hitbox.X + (int)(speed * speedMultiplier), hitbox.Y, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.X += (int)(speed * speedMultiplier);
                        moving = true;
                        isColliding = false;
                    }
                }
                //direction = new Vector2((float)Math.Sqrt(0.5), -(float)Math.Sqrt(0.5));
                //moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Animate(gameTime, 0);

                TileCollisionHandler(new Rectangle(hitbox.X + (int)(speed * speedMultiplier), hitbox.Y + (int)(speed * speedMultiplier), hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.X += (int)(Math.Sqrt(0.5) * (int)(speed * speedMultiplier));
                    hitbox.Y += (int)(Math.Sqrt(0.5) * (int)(speed * speedMultiplier));
                    moving = true;
                    isColliding = false;
                }
                else
                {
                    TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y + (int)(speed * speedMultiplier), hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.Y += (int)(speed * speedMultiplier);
                        moving = true;
                        isColliding = false;
                    }
                    TileCollisionHandler(new Rectangle(hitbox.X + (int)(speed * speedMultiplier), hitbox.Y, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.X += (int)(speed * speedMultiplier);
                        moving = true;
                        isColliding = false;
                    }
                }
                //direction = new Vector2((float)Math.Sqrt(0.5), (float)Math.Sqrt(0.5));
                //moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Animate(gameTime, 0);

                TileCollisionHandler(new Rectangle(hitbox.X - speed, hitbox.Y + speed, hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.X -= (int)(Math.Sqrt(0.5) * (int)(speed * speedMultiplier));
                    hitbox.Y += (int)(Math.Sqrt(0.5) * (int)(speed * speedMultiplier));
                    moving = true;
                    isColliding = false;
                }
                else
                {
                    TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y + (int)(speed * speedMultiplier), hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.Y += (int)(speed * speedMultiplier);
                        moving = true;
                        isColliding = false;
                    }
                    TileCollisionHandler(new Rectangle(hitbox.X - (int)(speed * speedMultiplier), hitbox.Y, hitbox.Width, hitbox.Height));
                    if (!isColliding)
                    {
                        hitbox.X -= (int)(speed * speedMultiplier);
                        moving = true;
                        isColliding = false;
                    }
                }
                //direction = new Vector2(-(float)Math.Sqrt(0.5), (float)Math.Sqrt(0.5));
                //moving = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A)) //left
            {
                Animate(gameTime, 1);

                TileCollisionHandler(new Rectangle(hitbox.X - (int)(speed * speedMultiplier), hitbox.Y, hitbox.Width, hitbox.Height));

                if (!isColliding)
                {
                    hitbox.X -= (int)(speed * speedMultiplier);
                    moving = true;
                    isColliding = false;
                }
                //direction = new Vector2(-1,0);
                //moving = true;                
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D)) //right
            {
                Animate(gameTime, 2);

                TileCollisionHandler(new Rectangle(hitbox.X + (int)(speed * speedMultiplier), hitbox.Y, hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.X += (int)(speed * speedMultiplier);
                    moving = true;
                    isColliding = false;
                }
                //direction = new Vector2(1,0);
                //moving = true;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S)) //down
            {
                Animate(gameTime, 0);

                TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y + (int)(speed * speedMultiplier), hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.Y += (int)(speed * speedMultiplier);
                    moving = true;
                    isColliding = false;
                }
                //direction = new Vector2(0,1);
                //moving = true;


            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))//up
            {
                Animate(gameTime, 3);

                TileCollisionHandler(new Rectangle(hitbox.X, hitbox.Y - (int)(speed * speedMultiplier), hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    hitbox.Y -= (int)(speed * speedMultiplier);
                    moving = true;
                    isColliding = false;
                }
                //direction = new Vector2(0,-1);
                //moving = true;
            }
            else
            {
                //Idle here
                ResetFrame();
            }
                        
            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);


            if (moving)
            {
                //if (!isColliding)
                //{
                //    middlepos += speed * direction;
                //    hitbox.Location = new Point((int)middlepos.X, (int)middlepos.Y) - new Point(hitbox.Width / 2, hitbox.Height / 2);
                //}
            }
            else
            {                
            }
            if (!isAttacking)
            {
            }
            else
            {

                if (equippedMelee != null)
                {
                    equippedMelee.Animate(gameTime, 0);

                    switch (mouseDirection)
                    {
                        case CardinalDirection.up:
                            equippedMelee.hitbox = new Rectangle(hitbox.Center.X - equippedMelee.hitboxWidth / 2, hitbox.Top - equippedMelee.hitboxLength / 2, equippedMelee.hitboxWidth, equippedMelee.hitboxLength);

                            break;
                        case CardinalDirection.down:
                            equippedMelee.hitbox = new Rectangle(hitbox.Center.X - equippedMelee.hitboxWidth / 2, hitbox.Bottom + equippedMelee.hitboxLength / 2, equippedMelee.hitboxWidth, equippedMelee.hitboxLength);


                            break;
                        case CardinalDirection.right:
                            equippedMelee.hitbox = new Rectangle(hitbox.Right + equippedMelee.hitboxWidth, hitbox.Center.Y - equippedMelee.hitboxWidth / 2, equippedMelee.hitboxLength, equippedMelee.hitboxWidth);


                            break;
                        case CardinalDirection.left:
                            equippedMelee.hitbox = new Rectangle(hitbox.Left - equippedMelee.hitboxWidth, hitbox.Center.Y - equippedMelee.hitboxWidth / 2, equippedMelee.hitboxLength, equippedMelee.hitboxWidth);


                            break;
                    }
                }
                else
                {
                    if (equippedRange != null)
                    {
                        equippedRange.Animate(gameTime, 0);

                        switch (mouseDirection)
                        {
                            case CardinalDirection.up:
                                equippedRange.hitbox = new Rectangle(hitbox.Center.X - equippedRange.hitboxWidth / 2, hitbox.Top - equippedRange.hitboxLength, equippedRange.hitboxWidth, equippedRange.hitboxLength);
                                
                                break;
                            case CardinalDirection.down:
                                equippedRange.hitbox = new Rectangle(hitbox.Center.X - equippedRange.hitboxWidth / 2, hitbox.Bottom, equippedRange.hitboxWidth, equippedRange.hitboxLength);


                                break;
                            case CardinalDirection.right:
                                equippedRange.hitbox = new Rectangle(hitbox.Right, hitbox.Center.Y - equippedRange.hitboxWidth / 2, equippedRange.hitboxLength, equippedRange.hitboxWidth);


                                break;
                            case CardinalDirection.left:
                                equippedRange.hitbox = new Rectangle(hitbox.Left - equippedRange.hitboxLength, hitbox.Center.Y - equippedRange.hitboxWidth / 2, equippedRange.hitboxLength, equippedRange.hitboxWidth);


                                break;
                        }
                    }
                }

                timeTillMovable += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeTillMovable >= attackTime)
                {
                    isAttacking = false;
                    timeTillMovable = 0;

                    speedMultiplier /= weaponSpeedMultiplier;

                    Level.UnhitAllEnemies();

                    if (equippedMelee != null)
                    {
                        Console.WriteLine("dum");
                        equippedMelee.ResetFrame();
                        equippedMelee.hitbox = new Rectangle(0, 0, 0, 0);
                    }
                    else if(equippedRange!=null)
                    {
                        equippedRange.hitbox = new Rectangle(0, 0, 0, 0);
                        equippedRange.ResetFrame();
                    }
                }
            }
        }

        public void GetAttackingDirection()
        {
            if (Math.Abs(mouseDistanceX) > Math.Abs(mouseDistanceY))
            {
                if (mouseDistanceX > 0)
                {
                    mouseDirection = CardinalDirection.right;
                    direction = new Vector2(1, 0);
                    if(equippedRange!=null)
                    {
                        equippedRange.hitbox = new Rectangle(hitbox.Right, hitbox.Center.Y - equippedRange.hitboxWidth / 2, equippedRange.hitboxLength, equippedRange.hitboxWidth);
                        equippedRange.middlepos = equippedRange.hitbox.Center.ToVector2();
                    }
                }
                else
                {
                    mouseDirection = CardinalDirection.left;
                    direction = new Vector2(-1, 0);
                    if (equippedRange != null)
                    {
                        equippedRange.hitbox = new Rectangle(hitbox.Left - equippedRange.hitboxLength, hitbox.Center.Y - equippedRange.hitboxWidth / 2, equippedRange.hitboxLength, equippedRange.hitboxWidth);
                        equippedRange.middlepos = equippedRange.hitbox.Center.ToVector2();
                    }
                }
            }
            else
            {
                if (mouseDistanceY > 0)
                {
                    mouseDirection = CardinalDirection.down;
                    direction = new Vector2(0, 1);
                    if (equippedRange != null)
                    {
                        equippedRange.hitbox = new Rectangle(hitbox.Center.X - equippedRange.hitboxWidth / 2, hitbox.Bottom, equippedRange.hitboxWidth, equippedRange.hitboxLength);
                        equippedRange.middlepos = equippedRange.hitbox.Center.ToVector2();
                    }
                }
                else
                {
                    mouseDirection = CardinalDirection.up;
                    direction = new Vector2(0, -1);
                    if (equippedRange != null)
                    {
                        equippedRange.hitbox = new Rectangle(hitbox.Center.X - equippedRange.hitboxWidth / 2, hitbox.Top - equippedRange.hitboxLength, equippedRange.hitboxWidth, equippedRange.hitboxLength);
                        equippedRange.middlepos = equippedRange.hitbox.Center.ToVector2();
                    }
                }
            }
        }


        public void InflictMeleeDamage(Enemy e)
        {
            if (equippedMelee != null && !e.beenHit)
            {
                if (e.hitbox.Intersects(equippedMelee.hitbox))
                {
                    e.health -= (equippedMelee.baseDamage * equippedMelee.damageMultiplyier*damageMultiplier);
                    e.beenHit = true;
                }
            }            
        }



        public void InflictDamage(Boss b)
        {
            if (equippedMelee != null && !b.beenHit)
            {
                if (b.hitbox.Intersects(equippedMelee.hitbox))
                {
                    b.health -= (equippedMelee.baseDamage * equippedMelee.damageMultiplyier);
                    b.beenHit = true;
                }
            }
        }




        public void ChangeWeapon(Weapon newWeapon)
        {
            if (equippedMelee != null)
            {
                WeaponItem droppedWeapon = new WeaponItem(equippedMelee, 0, false, equippedMelee.itemSpriteSheet, middlepos, Item.ItemType.weaponType, equippedMelee.itemName);
                Level.itemsList.Add(droppedWeapon);
            }
            else if(equippedRange != null)
            {
                WeaponItem droppedWeapon = new WeaponItem(equippedRange, 0, false, equippedRange.itemSpriteSheet, middlepos, Item.ItemType.weaponType, equippedRange.itemName);
                Level.itemsList.Add(droppedWeapon);
            }

            if (newWeapon is MeleeWeapon)
            {             
                equippedMelee = (MeleeWeapon)newWeapon;
                attackTime = 1 / (equippedMelee.baseAttackSpeed * equippedMelee.attackSpeedMultiplyier * attackSpeedMultiplier);
                equippedRange = null;
                weaponSpeedMultiplier= equippedMelee.speedMultiplier ;
                equippedMelee.timeBetweenFrames = attackTime / ((equippedMelee.spriteSheet.sheetSize.X + 1) + equippedMelee.spriteSheet.sheetSize.Y);
            }
            else if(newWeapon is RangeWeapon)
            {
                equippedRange = (RangeWeapon)newWeapon;
                attackTime = 1 / (equippedRange.baseAttackSpeed * equippedRange.attackSpeedMultiplyier * attackSpeedMultiplier);
                equippedMelee = null;
                weaponSpeedMultiplier = equippedRange.speedMultiplier;
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

        public void SetPlayerPosition(Vector2 newPos)
        {
            middlepos = newPos;
            hitbox.Location = new Point((int)middlepos.X, (int)middlepos.Y) - new Point(hitbox.Width / 2, hitbox.Height / 2);

        }


        public void UpdatePlayerStats(Item.ItemType itemType, float multiplier)
        {
            switch(itemType)
            {
                case Item.ItemType.attackSpeedBoost:

                    attackSpeedMultiplier *= multiplier;

                    if(equippedMelee!=null)
                    {
                        attackTime = 1 / (equippedMelee.baseAttackSpeed * equippedMelee.attackSpeedMultiplyier * attackSpeedMultiplier);
                        equippedMelee.timeBetweenFrames = attackTime / ((equippedMelee.spriteSheet.sheetSize.X + 1) + equippedMelee.spriteSheet.sheetSize.Y);
                    }
                    else if(equippedRange!=null)
                    {
                        attackTime = 1 / (equippedRange.baseAttackSpeed * equippedRange.attackSpeedMultiplyier * attackSpeedMultiplier);
                    }

                    break;

                case Item.ItemType.damageBoost:
                    damageMultiplier *= multiplier;
                    break;

                case Item.ItemType.speedBoost:
                    speedMultiplier *= multiplier;

                    break;
                case Item.ItemType.healthBoost:
                    health += multiplier;
                    maxHealth += multiplier;
                    HUD.UpdateCurrentHealthHUD((int)health);
                    HUD.UpdateMaxHealthHUD((int)maxHealth);
                    break;
            }
        }


        public void Draw(SpriteBatch sb)
        {

            sb.Draw(spriteSheet.texture, hitbox.Location.ToVector2(), null, new Rectangle(spriteSheet.frameSize.X * currentFrame.X, spriteSheet.frameSize.Y * currentFrame.Y, spriteSheet.frameSize.X, spriteSheet.frameSize.Y), Vector2.Zero, 0, Vector2.One, playerColor, SpriteEffects.None, 1f);


            if(equippedRange!=null)
            {
                equippedRange.Draw(sb);
            }
            else if (equippedMelee != null && isAttacking)
            {
                equippedMelee.Draw(sb, mouseDirection);

              
                //switch (mouseDirection)
                //{
                //    case Direction.up:

                //        break;
                //    case Direction.down:
                //        sb.Draw(equippedMelee.spriteSheet.texture, equippedMelee.hitbox, new Rectangle(0, 0, equippedMelee.spriteSheet.texture.Width, equippedMelee.spriteSheet.texture.Height), Color.White, weaponRotation, new Vector2(equippedMelee.spriteSheet.texture.Height/2 - equippedMelee.hitboxWidth, equippedMelee.spriteSheet.texture.Width / 2), SpriteEffects.None, 1);
                    
                //        sb.Draw(equippedMelee.spriteSheet.texture, equippedMelee.hitbox, Color.White);

                //        break;
                //    case Direction.right:
                //        sb.Draw(equippedMelee.spriteSheet.texture, equippedMelee.hitbox, new Rectangle(0, 0, equippedMelee.spriteSheet.texture.Width, equippedMelee.spriteSheet.texture.Height), Color.White, weaponRotation, new Vector2(equippedMelee.spriteSheet.texture.Width / 2 - equippedMelee.hitbox.Width / 2, equippedMelee.spriteSheet.texture.Height / 2 - equippedMelee.hitbox.Height / 2), SpriteEffects.None, 1);

                //        break;
                //    case Direction.left:
                //        sb.Draw(equippedMelee.spriteSheet.texture, equippedMelee.hitbox, new Rectangle(0, 0, equippedMelee.spriteSheet.texture.Width, equippedMelee.spriteSheet.texture.Height), Color.White, weaponRotation, new Vector2(equippedMelee.spriteSheet.texture.Width / 2 - equippedMelee.hitbox.Width / 2, equippedMelee.spriteSheet.texture.Height / 2 - equippedMelee.hitbox.Height / 2), SpriteEffects.None, 1);


                //        break;
                //}



            }

        }

    }
}