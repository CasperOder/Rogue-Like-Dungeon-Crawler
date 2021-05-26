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

        /// <summary>
        /// Creates a template of a ranged weapon.
        /// </summary>
        /// <param name="hitboxLength">Length of the hitbox.</param>
        /// <param name="hitboxWidth">Width of the hitbox.</param>
        /// <param name="baseDamage">Damage the weapon's projectile inflicts upon impact.</param>
        /// <param name="baseRange">The maximal range the projectile reaches before being destroyed.</param>
        /// <param name="baseAttackSpeed">Attack Speed of the weapon.</param>
        /// <param name="spriteSheet">Spritesheet the weapon utilizes.</param>
        /// <param name="itemSpriteSheet">Spritesheet of the weapon in item form.</param>
        /// <param name="speedMultiplier">Speed multiplier applied to the user when used.</param>
        /// <param name="weight">Which circle the weapon spawns in a shop.</param>
        /// <param name="itemName">Name of the weapon.</param>
        /// <param name="projectileType">The type of Projectile the weapon fires.</param>
        /// <param name="timeBetweenFrames">Time between frames, measured in seconds.</param>
        public RangeWeapon(int hitboxLength,int hitboxWidth,int baseDamage, int baseRange, float baseAttackSpeed, SpriteSheet spriteSheet, SpriteSheet itemSpriteSheet, float speedMultiplier, int weight, string itemName, Projectile projectileType, double timeBetweenFrames) :base(hitboxLength,hitboxWidth,baseDamage, baseAttackSpeed, spriteSheet, itemSpriteSheet, speedMultiplier, weight, itemName, timeBetweenFrames)
        {
            this.baseRange = baseRange;
            rangeMultiplyier = 1;
            this.projectileType = projectileType;
        }

        /// <summary>
        /// Fires a new projectile.
        /// </summary>
        /// <param name="playerDamageMultiplyier">Damage multiplier of the player upon firing.</param>
        /// <param name="direction">The direction of the Projectile.</param>
        public void CreateNewProjectile(float playerDamageMultiplyier, Moveable_Object.CardinalDirection direction)
        {
            Projectile newProjectile = new Projectile(projectileType.hitboxLength,projectileType.hitboxWidth,projectileType.spriteSheet, projectileType.timeBetweenFrames, projectileType.speed, middlepos, direction, baseDamage, baseRange, rangeMultiplyier, playerDamageMultiplyier);
            Level.projectilesOnScreenList.Add(newProjectile);
        }
    }
}
