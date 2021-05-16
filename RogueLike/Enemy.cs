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
        public bool beenHit;

        bool isColliding = false;

        double timeSinceAttack;
        public int spawnWeight; //ju högre desto svårare enemy i förhållande till resten av kretsen.

        bool remembersPlayer = false;
        Vector2 lastSeen;

        public Vector2 position;

        Vector2 directionOfPlayer
        {
            get
            {
                return DirectionOfObject(hitbox, Level.player.hitbox);
            }
        }

        public Enemy(SpriteSheet spriteSheet, double timeBetweenFrames, int speed, int spottingRange, int attackRange, int attackRangeOverlap, double attackSpeed, float health, float maxHealth, int spawnWeight) : base(spriteSheet, timeBetweenFrames, health, maxHealth)
        {
            hitbox.Size = spriteSheet.frameSize;
            
            base.speed = speed;
            this.spottingRange = spottingRange;
            this.attackRange = attackRange;
            this.attackRangeOverlap = attackRangeOverlap;
            this.attackSpeed = attackSpeed;
            this.spawnWeight = spawnWeight;
            
        }

        public Enemy copyEnemy()
        {
            return new Enemy(spriteSheet,timeBetweenFrames, speed, spottingRange, attackRange, attackRangeOverlap, attackSpeed, health, maxHealth, spawnWeight);
        }
        
        public void SetSpawn(Vector2 startPos)
        {
            hitbox.X = (int)startPos.X - hitbox.Width / 2;
            hitbox.Y = (int)startPos.Y - hitbox.Height / 2;
            position = hitbox.Location.ToVector2();
            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);

        }

        public void Update(GameTime gameTime)
        {
            Animate(gameTime, 0);

            hitbox.Location = position.ToPoint();

            Follow(gameTime);

            middlepos = hitbox.Center.ToVector2();
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


            //Movement
            TileCollision(new Rectangle((int)(position.X + speed * direction.X * (float)gameTime.ElapsedGameTime.TotalSeconds),(int)(position.Y + speed * direction.Y * (float)gameTime.ElapsedGameTime.TotalSeconds),hitbox.Width,hitbox.Height));
            if (!isColliding)
            {
                position += speed * direction * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                TileCollision(new Rectangle((int)(position.X), (int)(position.Y + speed * direction.Y * (float)gameTime.ElapsedGameTime.TotalSeconds), hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                   
                    position.Y += speed * direction.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                TileCollision(new Rectangle((int)(position.X + speed * direction.X * (float)gameTime.ElapsedGameTime.TotalSeconds), (int)(position.Y), hitbox.Width, hitbox.Height));
                if (!isColliding)
                {
                    position.X += speed * direction.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            
        }
        void TileCollision(Rectangle rect)
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
            int jumps = 10;

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
