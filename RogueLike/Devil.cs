using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    class Devil : Boss
    {
        public List<Vector2> projectileList = new List<Vector2>();
        public List<Vector2> projectileDir = new List<Vector2>();

        double attackTime = 2d;
        double timeSinceAttack;

        double projectileTime;
        double timeSinceProjectile;

        int projectileCount = 20;

        float projectileSpeed = 400;

        int damage = 10;

        public Devil(SpriteSheet spriteSheet, double timeBetweenFrames, float health, float maxHealth) : base(spriteSheet, timeBetweenFrames, health, maxHealth)
        {
            hitbox.Size = spriteSheet.frameSize;
        }

        public override void Update(GameTime gameTime)
        {
            hitbox.Location = position.ToPoint();

            ProjectileTimer(gameTime);

            Projectile(gameTime);

            Animate(gameTime, 0);
        }

        double RandomDouble(double max, double min)
        {
            return Level.rnd.NextDouble() * (max - min) + min;
        }

        void ProjectileTimer(GameTime gameTime)
        {
            timeSinceAttack += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceAttack >= attackTime)
            {
                timeSinceAttack = 0;

                projectileList.Clear();
                projectileDir.Clear();

                for (int i = 0; i < projectileCount; i++)
                {
                    projectileList.Add(hitbox.Center.ToVector2());
                    projectileDir.Add(new Vector2((float)RandomDouble(1d, -1d), (float)RandomDouble(1d, -1d)));
                }
            }
        }

        void Projectile(GameTime gameTime)
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                projectileList[i] += projectileDir[i] * projectileSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (new Rectangle(projectileList[i].ToPoint(), SpriteSheetManager.fireBall.frameSize).Intersects(Level.player.hitbox))
                {
                    HUD.UpdateCurrentHealthHUD((int)Level.player.health - damage);
                    Level.player.health -= damage;

                    projectileList.RemoveAt(i);
                    projectileDir.RemoveAt(i);

                    break;
                }
            }

            foreach (Vector2 projectile in projectileList)
            {

            }
        }

        public override void Draw(SpriteBatch sb)
        {
            foreach (Vector2 projectile in projectileList)
            {
                sb.Draw(SpriteSheetManager.fireBall.texture, projectile, null, new Rectangle(0, 0, SpriteSheetManager.fireBall.frameSize.X, SpriteSheetManager.fireBall.frameSize.Y), Vector2.Zero, 0, Vector2.One, color * colorOpacity, SpriteEffects.None, 1f);
            }

            base.Draw(sb);
        }
    }
}
