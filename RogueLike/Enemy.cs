using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueLike
{
    class Enemy : Moveable_Object
    {
        //Distans där fiender börjar följa spelaren
        public int spottingRange { get; private set; }

        //Distans från spelaren fienden försöker öppnå
        public int attackRange { get; private set; }

        //Extra distans där fiender kan attackera ifall andra fiender står ivägen
        public int attackRangeOverlap { get; private set; }

        //Tiden mellan attacker i sekunder
        public double attackSpeed { get; private set; }
        public Color enemyColor;
        public bool beenHit;

        double timeSinceAttack;

        bool remembersPlayer = false;
        Vector2 lastSeen;

        Vector2 position;

        Vector2 directionOfPlayer
        {
            get
            {
                return DirectionOfObject(hitbox, Level.player.hitbox);
            }
        }

        public Enemy(SpriteSheet spriteSheet, double timeBetweenFrames, Vector2 startPos, int speed, int spottingRange, int attackRange, int attackRangeOverlap, double attackSpeed, float health, float maxHealth) : base(spriteSheet, timeBetweenFrames, health, maxHealth)
        {
            hitbox.Size = spriteSheet.frameSize;
            hitbox.X = (int)startPos.X - hitbox.Width / 2;
            hitbox.Y = (int)startPos.Y - hitbox.Height / 2;

            base.speed = speed;
            this.spottingRange = spottingRange;
            this.attackRange = attackRange;
            this.attackRangeOverlap = attackRangeOverlap;
            this.attackSpeed = attackSpeed;

            position = hitbox.Location.ToVector2();

            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);

        }

        public void Update(GameTime gameTime)
        {
            Animate(gameTime, 0);

            hitbox.Location = position.ToPoint();

            Follow(gameTime);
        }

        void Follow(GameTime gameTime)
        {
            if (Distance(position, Level.player.hitbox.Center.ToVector2()) <= spottingRange)
            {
                if (RayCast(directionOfPlayer, hitbox.Center.ToVector2()))
                {
                    lastSeen = Level.player.hitbox.Center.ToVector2();
                    remembersPlayer = true;


                    if (Distance(position, Level.player.hitbox.Center.ToVector2()) >= attackRange)
                    {
                        Movement(directionOfPlayer, gameTime);
                    }

                    if (Distance(position, Level.player.hitbox.Center.ToVector2()) <= attackRange + attackRangeOverlap)
                        Attacking(gameTime);                   
                }
                else if (remembersPlayer)
                {

                    if (hitbox.Contains(lastSeen))
                        remembersPlayer = false;

                    Movement(DirectionOfObject(position, lastSeen), gameTime);
                }
            }         
        }

        void Attacking(GameTime gameTime)
        {
            if (timeSinceAttack >= attackSpeed)
            {
                //Attack sker här
                timeSinceAttack = 0;
            }
            else
                color = Color.White;

            timeSinceAttack += gameTime.ElapsedGameTime.TotalSeconds;
        }



        void Movement(Vector2 direction, GameTime gameTime)
        {

            //Enemy collision
            foreach (Enemy enemy in Level.enemyList)
            {
                if (enemy == this)
                    continue;

                if (enemy.hitbox.Intersects(hitbox))
                    direction -= DirectionOfObject(hitbox, enemy.hitbox);
            }

            //Player collision
            if (hitbox.Intersects(Level.player.hitbox))
                direction -= DirectionOfObject(hitbox, Level.player.hitbox);

            //Tile collision
            foreach (Room r in Level.generatedRoomList)
            {
                foreach (Tile t in r.tileArray)
                {
                    if (t.solid)
                    {
                        if (t.hitbox.Intersects(hitbox))
                        {
                            int collisionSide = CollisionSide(t.hitbox, position, hitbox.Size);

                            if (collisionSide <= 1)
                            {
                                direction.Y -= DirectionOfObject(hitbox, t.hitbox).Y;

                                if (direction.X > 0)
                                    direction.X = 1;
                                else
                                    direction.X = -1;
                            }
                            else if (collisionSide >= 2)
                            {
                                direction.X -= DirectionOfObject(hitbox, t.hitbox).X;

                                if (direction.Y > 0)
                                    direction.Y = 1;
                                else
                                    direction.Y = -1;
                            }
                            Console.WriteLine(collisionSide);

                            break;
                        }
                    }
                }
            }

            //Movement
            position += speed * direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static int CollisionSide(Rectangle rect, Vector2 objectPos, Point objectSize)
        {
            float[] distances = new float[4];

            distances[0] = rect.Top - (objectPos.Y + objectSize.Y);
            distances[1] = objectPos.Y - rect.Bottom;
            distances[2] = objectPos.X - rect.Right;
            distances[3] = rect.Left - (objectPos.X + objectSize.X);

            int closestIndex = 0;
            int diff = int.MaxValue;

            for (int i = 0; i < distances.Length; ++i)
            {
                int abs = (int)Math.Abs(distances[i]);
                if (abs < diff)
                {
                    closestIndex = i;
                    diff = abs;
                }
                else if (abs == diff && distances[i] > 0 && distances[closestIndex] < 0)
                {
                    closestIndex = i;
                }
            }

            return closestIndex;
        }
        public static int CollisionSide(Rectangle r1, Rectangle r2)
        {
            if (r1.Contains(new Point(r2.Center.X, r2.Center.Y - r2.Height / 2)))
                return 0;
            else if (r1.Contains(new Point(r2.Center.X, r2.Center.Y + r2.Height / 2)))
                return 1;
            else if (r1.Contains(new Point(r2.Center.X + r2.Width / 2, r2.Center.Y)))
                return 2;
            else if (r1.Contains(new Point(r2.Center.X - r2.Width / 2, r2.Center.Y)))
                return 3;
            else
                return 4;
        }

        Vector2 DirectionOfObject(Vector2 origin, Vector2 target)
        {
            Vector2 direction = target - origin;
            direction.Normalize();
            return direction;
        }
        public float GetPlayerDistance (Player p)
        {
            float playerDistance = Vector2.Distance(middlepos,p.middlepos);
            return playerDistance;

        }
        Vector2 DirectionOfObject(Rectangle origin, Rectangle target)
        {
            return DirectionOfObject(origin.Center.ToVector2(), target.Center.ToVector2());
        }

        bool RayCast(Vector2 direction, Vector2 origin)
        {
            Vector2 ray = origin;

            //Behöver vara mindre än spelare och tile hitbox men för liten skapar mycket lagg
            int jumps = 40;

            //Console.WriteLine("player" + Level.player.hitbox.Location);
            //Console.WriteLine("enemy" + Level.enemyList[1].hitbox.Location);

            while (true)
            {
                foreach (Room r in Level.generatedRoomList)
                {
                    foreach (Tile t in r.tileArray)
                    {
                        if (t.solid)
                        {
                            if (t.hitbox.Contains(ray))
                            {
                                return false;
                            }
                        }
                    }
                }
                if (Level.player.hitbox.Contains(ray))
                    return true;

                ray += direction * jumps;
            }

        }

        double Distance(Vector2 a, Vector2 b)
        {
            return Math.Sqrt((Math.Pow(a.X - b.X, 2d) + Math.Pow(a.Y - b.Y, 2d)));
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, position, null, new Rectangle(spriteSheet.frameSize.X * currentFrame.X, spriteSheet.frameSize.Y * currentFrame.Y, spriteSheet.frameSize.X, spriteSheet.frameSize.Y), Vector2.Zero, 0, Vector2.One, color, SpriteEffects.None, 1f);
        }
    }
}
