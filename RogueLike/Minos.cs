using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    enum AttackState
    {
        Aiming,
        Melee,
        Ranged,
        Special
    }

    class Minos : Boss
    {
        AttackState state;


        //Ranged stuff

        //Half the zone infront of Minos where he can do range attack
        int rangedZone = 30;

        //Hit cooldown in seconds
        double rangedSpeed = 0.4d;
        //Arm speed in seconds
        double armSpeed = 0.02d;
        //Arm stun in seconds
        double armStun = 0.4d;

        float armLength = 0;
        int maxArmLength = 35;

        bool returnArm = false;

        double timeSinceRanged;
        double timeSinceArm;

        int rangeDamage = 10;

        //Melee stuff

        //Time between melee hits in seconds
        double meleeSpeed = 0.3d;
        double timeSinceMelee;
        //Animation lock time
        double meleeLock = 0.3d;
        double timeSinceMeleeLock;

        float meleeRange = 230f;
        float meleeZone = 2;

        int meleeDamage = 10;

        Rectangle meleeHitbox;
        Point meleeHitboxSize = new Point(200, 200);

        bool hasHit = false;

        List<Vector2> armList = new List<Vector2>();

        //Special attack stuff

        AnimatedObject specialAttack = new AnimatedObject(SpriteSheetManager.minosGround, 0.4);

        double specialWait = 2d;
        double specialWaitElapsed;

        int specialSuccesChance = 50;

        double specialSpeed = 0.4d;
        double specialElapsedTime;

        bool drawSpecial = false;

        int specialDamage = 30;


        public Minos(SpriteSheet spriteSheet, double timeBetweenFrames, float health, float maxHealth) :
            base(spriteSheet, timeBetweenFrames, health, maxHealth)
        {
            state = AttackState.Aiming;

            speed = 400;

            hitbox.Size = spriteSheet.frameSize;

            meleeHitbox = new Rectangle((int)position.X, (int)position.Y, spriteSheet.frameSize.X + meleeHitboxSize.X, spriteSheet.frameSize.Y + meleeHitboxSize.Y);

            specialAttack.hitbox.Size = SpriteSheetManager.minosGround.frameSize;
        }

        public override void Update(GameTime gameTime)
        {
            Animate(gameTime, 0);
            hitbox.Location = position.ToPoint();
            middlepos = hitbox.Center.ToVector2();

            Console.WriteLine(health);

            int playerPositionX = Level.player.hitbox.Location.X - SpriteSheetManager.playerPlaceHolder.frameSize.X;

            switch (state)
            {
                case AttackState.Aiming:


                    timeSinceMelee += gameTime.ElapsedGameTime.TotalSeconds;

                    if (!drawSpecial && Distance(new Vector2((position.X + (spriteSheet.texture.Width / spriteSheet.sheetSize.X) / 2), position.Y + spriteSheet.texture.Height / 2), Level.player.hitbox.Center.ToVector2()) < meleeRange)
                    {
                        if (playerPositionX > hitbox.Center.X - rangedZone && playerPositionX < hitbox.Center.X + meleeZone)
                            Follow(playerPositionX, gameTime);

                        if (timeSinceMelee >= meleeSpeed)
                        {
                            timeSinceMelee -= meleeSpeed;

                            state = AttackState.Melee;
                        }

                    }
                    //(!drawSpecial && playerPositionX > (position.X + (spriteSheet.texture.Width / spriteSheet.sheetSize.X) / 8) - rangedZone && playerPositionX < (position.X + (spriteSheet.texture.Width / spriteSheet.sheetSize.X) / 4) + rangedZone)
                    else if (!drawSpecial && playerPositionX > hitbox.Location.X - rangedZone && playerPositionX < hitbox.Location.X + rangedZone)
                    {
                        timeSinceRanged += gameTime.ElapsedGameTime.TotalSeconds;

                        if (timeSinceRanged >= rangedSpeed)
                        {
                            if (Level.rnd.Next(100) > specialSuccesChance)
                            {
                                specialAttack.hitbox.Location = Level.player.hitbox.Location;
                                state = AttackState.Special;
                            }
                            else
                            {
                                timeSinceRanged -= rangedSpeed;

                                state = AttackState.Ranged;
                            }
                        }

                        timeSinceRanged += gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else if (!drawSpecial)
                    {
                        Follow(playerPositionX, gameTime);
                    }

                    break;
                case AttackState.Melee:
                    if (playerPositionX > hitbox.Location.X - meleeZone && playerPositionX < hitbox.Location.X + meleeZone)
                        Follow(playerPositionX, gameTime);
                    Melee(gameTime);
                    break;
                case AttackState.Ranged:
                    Ranged(gameTime);
                    break;
                case AttackState.Special:
                    Special(gameTime);
                    break;
                default:
                    break;
            }
        }

        public void Follow(float xPos, GameTime gameTime)
        {
            if (hitbox.Location.X > xPos)
            {
                Movement(new Vector2(-1, 0), gameTime);
            }
            if (hitbox.Location.X < xPos)
            {
                Movement(new Vector2(1, 0), gameTime);
            }
        }

        public void Special(GameTime gameTime)
        {
            specialElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            drawSpecial = true;

            specialAttack.ResetFrame();

            if (specialElapsedTime >= specialSpeed)
            {
                specialWaitElapsed += gameTime.ElapsedGameTime.TotalSeconds;

                specialAttack.currentFrame.X = 2;
                specialAttack.sequenceIndex = 2;

                if (Level.player.hitbox.Intersects(specialAttack.hitbox) && !hasHit)
                {
                    hasHit = true;
                    HUD.UpdateCurrentHealthHUD((int)Level.player.health - specialDamage);
                    Level.player.health -= specialDamage;
                }

                if (specialWaitElapsed >= specialWait)
                {
                    specialWaitElapsed -= specialWait;
                    specialElapsedTime = 0;

                    hasHit = false;
                    drawSpecial = false;

                    state = AttackState.Aiming;
                }
            }
        }

        public void Melee(GameTime gameTime)
        {
            timeSinceMeleeLock += gameTime.ElapsedGameTime.TotalSeconds;

            currentFrame.X = 5;
            sequenceIndex = 4;

            meleeHitbox.Location = new Point(hitbox.X - meleeHitboxSize.X / 2, hitbox.Y - meleeHitboxSize.Y / 2);

            if (Level.player.hitbox.Intersects(meleeHitbox) && !hasHit)
            {
                hasHit = true;
                HUD.UpdateCurrentHealthHUD((int)Level.player.health - meleeDamage);
                Level.player.health -= meleeDamage;
            }

            if (timeSinceMeleeLock >= meleeLock)
            {
                timeSinceMeleeLock= 0;
                ResetFrame();

                timeSinceMelee = 0;

                state = AttackState.Aiming;
                hasHit = false;
            }
        }

        public void Ranged(GameTime gameTime)
        {
            if (timeSinceArm >= armSpeed && !returnArm)
            {
                timeSinceArm -= armSpeed;

                armList.Add(new Vector2((position.X + (spriteSheet.texture.Width / spriteSheet.sheetSize.X) / 2) - SpriteSheetManager.minosArm.texture.Width / 2, position.Y + SpriteSheetManager.bossMinos.texture.Height + armLength));

                armLength += (SpriteSheetManager.minosArm.texture.Height / 3) * 2;
            }

            timeSinceArm += gameTime.ElapsedGameTime.TotalSeconds;

            if (armList.Count >= maxArmLength)
                returnArm = true;

            foreach (Vector2 armPos in armList)
            {
                if (Level.player.hitbox.Intersects(new Rectangle(armPos.ToPoint(), SpriteSheetManager.minosArm.frameSize)) && !hasHit)
                {
                    hasHit = true;
                    HUD.UpdateCurrentHealthHUD((int)Level.player.health - rangeDamage);
                    Level.player.health -= rangeDamage;
                    break;
                }
            }

            if (returnArm)
            {
                if (timeSinceArm >= armSpeed)
                {
                    timeSinceArm -= armSpeed;

                    armList.Remove(armList.Last());
                }
                armLength = 0;

                if (armList.Count <= 0)
                {
                    returnArm = false;
                    hasHit = false;
                    state = AttackState.Aiming;
                }
            }
        }

        /// <summary>
        /// Returns true if X axis is closest
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool ClosestAxis(Vector2 origin, Vector2 target)
        {
            Vector2 direction = DirectionOfObject(origin, target);

            if (direction.X > direction.Y)
                return true;
            else
                return false;
        }

        Vector2 DirectionOfObject(Vector2 origin, Vector2 target)
        {
            Vector2 direction = target - origin;
            direction.Normalize();
            return direction;
        }

        double Distance(Vector2 a, Vector2 b)
        {
            return Math.Sqrt((Math.Pow(a.X - b.X, 2d) + Math.Pow(a.Y - b.Y, 2d)));
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            if (drawSpecial)
                specialAttack.Draw(sb);

            foreach (Vector2 armPos in armList)
            {
                sb.Draw(SpriteSheetManager.minosArm.texture, armPos, Color.White);
            }
        }
    }
}
