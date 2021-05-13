using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueLike
{
    class RangeWeapon:Weapon
    {
        Projectile projectileType;

        public RangeWeapon(int hitboxLength,int hitboxWidth,int baseDamage, int baseRange, float baseAttackSpeed, SpriteSheet spriteSheet, SpriteSheet itemSpriteSheet, float speedMultiplier, int weight, string itemName, Projectile projectileType, double timeBetweenFrames) :base(hitboxLength,hitboxWidth,baseDamage, baseAttackSpeed, spriteSheet, itemSpriteSheet, speedMultiplier, weight, itemName, timeBetweenFrames)
        {
            this.baseRange = baseRange;
            rangeMultiplyier = 1;
            this.projectileType = projectileType;
        }


        public void CreateNewProjectile(Vector2 startPos, float playerDamageMultiplyier, Moveable_Object.CardinalDirection direction)
        {
            Projectile newProjectile = new Projectile(projectileType.hitboxLength,projectileType.hitboxWidth,projectileType.spriteSheet, projectileType.timeBetweenFrames, projectileType.speed, startPos, direction, baseDamage, baseRange, rangeMultiplyier, playerDamageMultiplyier);
            Level.projectilesOnScreenList.Add(newProjectile);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox, Color.White);            
        }

    }
}
