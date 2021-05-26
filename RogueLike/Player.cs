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

        /// <summary>
        /// Creates a new instance of a player character
        /// </summary>
        /// <param name="spriteSheet">Sets the spritesheet of the player</param>
        /// <param name="timeBetweenFrames">Time between frames, measured in seconds</param>
        public Player(SpriteSheet spriteSheet, /*Vector2 startPos,*/ double timeBetweenFrames) : base(spriteSheet, timeBetweenFrames)
        {
            hitbox.Size = spriteSheet.frameSize;

            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);
            maxHealth = 100;
            health = maxHealth;
            
            speed = 10;

        }

        /// <summary>
        /// Handles the movements and attacks of the player
        /// </summary>
        /// <param name="gameTime">Inherit the GameTime class</param>
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
                        equippedRange.CreateNewProjectile(damageMultiplier, mouseDirection);
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
            }
            else
            {
                //Idle here
                ResetFrame();
            }
                        
            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);

            
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
                            equippedMelee.position = new Vector2(hitbox.Center.X - equippedMelee.hitboxWidth / 4, hitbox.Top - equippedMelee.hitboxLength / 4);
                            equippedMelee.damageHitbox = new Rectangle(hitbox.Center.X - equippedMelee.hitboxWidth / 2, hitbox.Top - equippedMelee.hitboxLength, equippedMelee.hitboxWidth, equippedMelee.hitboxLength);
                            break;
                        case CardinalDirection.down:
                            equippedMelee.hitbox = new Rectangle(hitbox.Center.X - equippedMelee.hitboxWidth / 2, hitbox.Bottom + equippedMelee.hitboxLength / 2, equippedMelee.hitboxWidth, equippedMelee.hitboxLength);
                            equippedMelee.position = new Vector2(hitbox.Center.X - equippedMelee.hitboxWidth / 4, hitbox.Bottom + equippedMelee.hitboxLength / 4);
                            equippedMelee.damageHitbox = new Rectangle(hitbox.Center.X - equippedMelee.hitboxWidth / 2, hitbox.Bottom, equippedMelee.hitboxWidth, equippedMelee.hitboxLength);
                            break;
                        case CardinalDirection.right:
                            equippedMelee.hitbox = new Rectangle(hitbox.Right + equippedMelee.hitboxWidth, hitbox.Center.Y - equippedMelee.hitboxWidth / 2, equippedMelee.hitboxLength, equippedMelee.hitboxWidth);
                            equippedMelee.position = new Vector2(hitbox.Right + equippedMelee.hitboxWidth, hitbox.Center.Y);

                            equippedMelee.damageHitbox = new Rectangle(hitbox.Right, hitbox.Center.Y - equippedMelee.hitboxWidth / 2, equippedMelee.hitboxLength, equippedMelee.hitboxWidth);
                            break;
                        case CardinalDirection.left:
                            equippedMelee.hitbox = new Rectangle(hitbox.Left - equippedMelee.hitboxWidth, hitbox.Center.Y - equippedMelee.hitboxWidth / 2, equippedMelee.hitboxLength, equippedMelee.hitboxWidth);
                            equippedMelee.position = new Vector2(hitbox.Left - equippedMelee.hitboxWidth, hitbox.Center.Y);
                            equippedMelee.damageHitbox = new Rectangle(hitbox.Left - equippedMelee.hitboxLength, hitbox.Center.Y - equippedMelee.hitboxWidth / 2, equippedMelee.hitboxLength, equippedMelee.hitboxWidth);
                            break;
                    }
                }
                else
                {

                   equippedRange.Animate(gameTime, 0);

                    if (equippedRange != null)
                    {
                        equippedRange.Animate(gameTime, 0);

                        switch (mouseDirection)
                        {
                            case CardinalDirection.up:
                                equippedRange.hitbox = new Rectangle(hitbox.Center.X - equippedRange.hitboxWidth / 2, hitbox.Top - equippedRange.hitboxLength, equippedRange.hitboxWidth, equippedRange.hitboxLength);
                                equippedRange.position = new Vector2(hitbox.Center.X - equippedRange.hitboxWidth / 4, hitbox.Top - equippedRange.hitboxLength / 4);

                                break;
                            case CardinalDirection.down:
                                equippedRange.hitbox = new Rectangle(hitbox.Center.X - equippedRange.hitboxWidth / 2, hitbox.Bottom, equippedRange.hitboxWidth, equippedRange.hitboxLength);
                                equippedRange.position = new Vector2(hitbox.Center.X - equippedRange.hitboxWidth / 4, hitbox.Bottom + equippedRange.hitboxLength / 4);

                                break;
                            case CardinalDirection.right:
                                equippedRange.hitbox = new Rectangle(hitbox.Right, hitbox.Center.Y - equippedRange.hitboxWidth / 2, equippedRange.hitboxLength, equippedRange.hitboxWidth);
                                equippedRange.position = new Vector2(hitbox.Right + equippedRange.hitboxWidth, hitbox.Center.Y);

                                break;
                            case CardinalDirection.left:
                                equippedRange.hitbox = new Rectangle(hitbox.Left - equippedRange.hitboxLength, hitbox.Center.Y - equippedRange.hitboxWidth / 2, equippedRange.hitboxLength, equippedRange.hitboxWidth);
                                equippedRange.position = new Vector2(hitbox.Left - equippedRange.hitboxWidth, hitbox.Center.Y);
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
                        equippedMelee.ResetFrame();
                        equippedMelee.hitbox = new Rectangle(0, 0, 0, 0);
                        equippedMelee.damageHitbox = new Rectangle(0, 0, 0, 0);
                    }
                    else if(equippedRange!=null)
                    {
                        equippedRange.hitbox = new Rectangle(0, 0, 0, 0);
                        equippedRange.ResetFrame();
                    }
                }
            }

            if (health <= 0)
            {
                Game1.SetGameOverScreen();
            }
        }

        /// <summary>
        /// Sets the attacking direction of the player
        /// </summary>
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
                    else if(equippedMelee!=null)
                    {
                        equippedMelee.damageHitbox = new Rectangle(hitbox.Right, hitbox.Center.Y - equippedMelee.hitboxWidth / 2, equippedMelee.hitboxLength, equippedMelee.hitboxWidth);
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
                    else if (equippedMelee != null)
                    {
                        equippedMelee.damageHitbox = new Rectangle(hitbox.Left - equippedMelee.hitboxLength, hitbox.Center.Y - equippedMelee.hitboxWidth / 2, equippedMelee.hitboxLength, equippedMelee.hitboxWidth);
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
                    else if (equippedMelee != null)
                    {
                        equippedMelee.damageHitbox = new Rectangle(hitbox.Center.X - equippedMelee.hitboxWidth / 2, hitbox.Bottom, equippedMelee.hitboxWidth, equippedMelee.hitboxLength);
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
                    else if (equippedMelee != null)
                    {
                        equippedMelee.damageHitbox = new Rectangle(hitbox.Center.X - equippedMelee.hitboxWidth / 2, hitbox.Top - equippedMelee.hitboxLength, equippedMelee.hitboxWidth, equippedMelee.hitboxLength);
                    }
                    
                }
            }
        }

        /// <summary>
        /// Checks if the player can inflict melee damage on a target
        /// </summary>
        /// <param name="target">Specific target to damage check</param>
        public void InflictMeleeDamage(Moveable_Object target)
        {
            if (equippedMelee != null && !target.beenHit)
            {
                if (target.hitbox.Intersects(equippedMelee.damageHitbox))
                {
                    target.health -= (equippedMelee.baseDamage * equippedMelee.damageMultiplyier*damageMultiplier);
                    target.beenHit = true;
                }
            }            
        }
        

        /// <summary>
        /// Change the weapon of the player and drops the old weapon on the ground
        /// </summary>
        /// <param name="newWeapon">The new weapon assigned to the player</param>
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
                equippedRange.timeBetweenFrames = attackTime / ((equippedRange.spriteSheet.sheetSize.X + 1) + equippedRange.spriteSheet.sheetSize.Y);
            }
        }
        

        /// <summary>
        /// Positions the player to a new location
        /// </summary>
        /// <param name="newPos">The new location</param>
        public void SetPlayerPosition(Vector2 newPos)
        {
            middlepos = newPos;
            hitbox.Location = new Point((int)middlepos.X, (int)middlepos.Y) - new Point(hitbox.Width / 2, hitbox.Height / 2);
        }

        /// <summary>
        /// Changes a stat of the player
        /// </summary>
        /// <param name="itemType">Changes stat depending on what ItemType the picked up Item had.</param>
        /// <param name="multiplier">The change amount. Additively health, multiplicity otherwise.</param>
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
                        equippedRange.timeBetweenFrames = attackTime / ((equippedRange.spriteSheet.sheetSize.X + 1) + equippedRange.spriteSheet.sheetSize.Y);
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
                case Item.ItemType.healAndSave:

                    health += multiplier;
                    if(health>maxHealth)
                    {
                        health = maxHealth;
                    }
                    HUD.UpdateCurrentHealthHUD((int)health);
                    SetSaveFile();
                    break;
            }
        }

        /// <summary>
        /// Sets the player stats from an old savefile.
        /// </summary>
        /// <param name="savedWeapon">Old Weapon from savefile.</param>
        /// <param name="savedHealth">Old Health from savefile.</param>
        /// <param name="savedMaxHealth">Old Max Health from savefile.</param>
        /// <param name="savedAttackSpeedMultiplier">Old Attack Speed Multiplier from savefile.</param>
        /// <param name="savedDamageMultiplier">Old Damage Multiplier from savefile.</param>
        /// <param name="savedSpeedMultiplier">Old Speed Multiplier from savefile.</param>
        public void SetStatsFromSaveFile(Weapon savedWeapon, float savedHealth, float savedMaxHealth, float savedAttackSpeedMultiplier, float savedDamageMultiplier, float savedSpeedMultiplier)
        {
            health = savedHealth;
            maxHealth = savedMaxHealth;
            attackSpeedMultiplier = savedAttackSpeedMultiplier;
            damageMultiplier = savedDamageMultiplier;
            speedMultiplier = savedSpeedMultiplier;
            ChangeWeapon(savedWeapon);

            HUD.UpdateCurrentHealthHUD((int)health);
            HUD.UpdateMaxHealthHUD((int)maxHealth);
        }

        /// <summary>
        /// Saves the player stats to the savefile
        /// </summary>
        public void SetSaveFile()
        {
            Weapon weaponToSave;
            if(equippedMelee!=null)
            {
                weaponToSave = equippedMelee;
            }
            else
            {
                weaponToSave = equippedRange;
            }
            SavefileHandler.SaveToFile(weaponToSave, health, maxHealth, attackSpeedMultiplier, damageMultiplier, speedMultiplier);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox.Location.ToVector2(), null, new Rectangle(spriteSheet.frameSize.X * currentFrame.X, spriteSheet.frameSize.Y * currentFrame.Y, spriteSheet.frameSize.X, spriteSheet.frameSize.Y), Vector2.Zero, 0, Vector2.One, playerColor, SpriteEffects.None, 1f);


            //sb.Draw(SpriteSheetManager.dummy.texture, equippedMelee.damageHitbox, Color.White);

            if(equippedRange!=null && isAttacking)
            {
                equippedRange.Draw(sb, mouseDirection);
            }
            else if (equippedMelee != null && isAttacking)
            {
                equippedMelee.Draw(sb, mouseDirection);
            }
        }
    }
}