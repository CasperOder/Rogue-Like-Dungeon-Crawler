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
        int rangedZone = 50;

        //Hit cooldown in seconds
        double rangedSpeed = 0.4d;
        //Arm speed in seconds
        double armSpeed = 0.03d;
        //Arm stun in seconds
        double armStun = 0.4d;

        float armLength = 0;

        bool returnArm = false;

        double timeSinceRanged;
        double timeSinceArm;

        int rangeDamage = 10;

        //Melee stuff

        //Time between melee hits in seconds
        double meleeSpeed = 0.6d;
        double timeSinceMelee;
        //Animation lock time
        double meleeLock = 0.3d;
        double timeSinceMeleeLock;

        float meleeRange = 230f;
        float meleeZone = 4;

        int meeleDamage = 5;

        Rectangle meeleHitbox;

        bool hasHit = false;

        List<Vector2> armList = new List<Vector2>();

        public Minos(SpriteSheet spriteSheet, double timeBetweenFrames, float health, float maxHealth) :
            base(spriteSheet, timeBetweenFrames, health, maxHealth)
        {
            state = AttackState.Aiming;

            speed = 300;

            hitbox.Size = spriteSheet.frameSize;

            meeleHitbox = new Rectangle((int)position.X, (int)position.Y, spriteSheet.frameSize.X + 50, spriteSheet.frameSize.Y + 50);
        }

        public override void Update(GameTime gameTime)
        {
            Animate(gameTime, 0);
            hitbox.Location = position.ToPoint();

            Console.WriteLine(health);

            int playerPositionX = Level.player.hitbox.Location.X - SpriteSheetManager.player.frameSize.X;

            switch (state)
            {
                case AttackState.Aiming:


                    timeSinceMelee += gameTime.ElapsedGameTime.TotalSeconds;

                    if (Distance(new Vector2((position.X + (spriteSheet.texture.Width / spriteSheet.sheetSize.X) / 2), position.Y + spriteSheet.texture.Height / 2), Level.player.hitbox.Center.ToVector2()) < meleeRange)
                    {
                        if (playerPositionX > hitbox.Center.X - rangedZone && playerPositionX < hitbox.Center.X + meleeZone)
                            Follow(playerPositionX, gameTime);

                        if (timeSinceMelee >= meleeSpeed)
                        {
                            timeSinceMelee -= meleeSpeed;

                            state = AttackState.Melee;
                        }

                    }
                    else if (playerPositionX > (position.X + (spriteSheet.texture.Width / spriteSheet.sheetSize.X) / 8) - rangedZone && playerPositionX < hitbox.Center.X + rangedZone)
                    {
                        timeSinceRanged += gameTime.ElapsedGameTime.TotalSeconds;

                        if (timeSinceRanged >= rangedSpeed)
                        {
                            timeSinceRanged -= rangedSpeed;

                            state = AttackState.Ranged;
                        }

                        timeSinceRanged += gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else
                    {
                        Follow(playerPositionX, gameTime);
                    }
                    break;
                case AttackState.Melee:
                    if (playerPositionX > hitbox.Center.X - meleeZone && playerPositionX < hitbox.Center.X + meleeZone)
                        Follow(playerPositionX, gameTime);
                    Melee(gameTime);
                    break;
                case AttackState.Ranged:
                    Ranged(gameTime);
                    break;
                default:
                    break;
            }
        }

        public void Follow(float xPos, GameTime gameTime)
        {

            if (hitbox.Center.X > xPos)
            {
                Movement(new Vector2(-1, 0), gameTime);
            }
            if (hitbox.Center.X < xPos)
            {
                Movement(new Vector2(1, 0), gameTime);
            }
        }

        public void Melee(GameTime gameTime)
        {
            timeSinceMeleeLock += gameTime.ElapsedGameTime.TotalSeconds;

            currentFrame.X = 5;
            sequenceIndex = 4;

            meeleHitbox.Location = hitbox.Location;

            if (Level.player.hitbox.Intersects(meeleHitbox) && !hasHit)
            {
                hasHit = true;
                HUD.UpdateCurrentHealthHUD((int)Level.player.health - meeleDamage);
                Level.player.health -= meeleDamage;
            }

            if (timeSinceMeleeLock >= meleeLock)
            {
                timeSinceMeleeLock -= meleeLock;
                ResetFrame();

                timeSinceMelee -= meleeSpeed;

                state = AttackState.Aiming;
                hasHit = false;
            }
        }

        public void Ranged(GameTime gameTime)
        {
            if (timeSinceArm >= armSpeed && !returnArm)
            {
                timeSinceArm -= armSpeed;

                armList.Add(new Vector2((position.X + (spriteSheet.texture.Width / spriteSheet.sheetSize.X) / 2) - SpriteSheetManager.MinosArm.texture.Width / 2, position.Y + SpriteSheetManager.bossMinos.texture.Height + armLength));

                armLength += (SpriteSheetManager.MinosArm.texture.Height / 3) * 2;
            }

            timeSinceArm += gameTime.ElapsedGameTime.TotalSeconds;

            if (armList.Count >= 29)
                returnArm = true;

            foreach (Vector2 armPos in armList)
            {
                if (Level.player.hitbox.Intersects(new Rectangle(armPos.ToPoint(), SpriteSheetManager.MinosArm.frameSize)) && !hasHit)
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

            foreach (Vector2 armPos in armList)
            {
                sb.Draw(SpriteSheetManager.MinosArm.texture, armPos, Color.White);
            }
        }
    }
}
