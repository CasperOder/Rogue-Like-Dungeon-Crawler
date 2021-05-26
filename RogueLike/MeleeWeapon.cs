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
    class MeleeWeapon:Weapon
    {
        public Rectangle damageHitbox;

        /// <summary>
        /// Creates a template of a melee weapon.
        /// </summary>
        /// <param name="hitboxLength">Length of the hitbox.</param>
        /// <param name="hitboxWidth">Width of the hitbox.</param>
        /// <param name="baseDamage">Damage the weapon inflicts upon impact</param>
        /// <param name="baseAttackSpeed">Attack Speed of the weapon</param>
        /// <param name="spriteSheet">Spritesheet the weapon utilizes.</param>
        /// <param name="itemSpriteSheet">Spritesheet of the weapon in item form.</param>
        /// <param name="speedMultiplier">Speed multiplier applied to the user when used.</param>
        /// <param name="weight">Which circle the weapon spawns in a shop.</param>
        /// <param name="itemName">Name of the weapon.</param>
        /// <param name="timeBetweenFrames">Time between frames, measured in seconds.</param>
        public MeleeWeapon(int hitboxLength, int hitboxWidth, int baseDamage, float baseAttackSpeed, SpriteSheet spriteSheet, SpriteSheet itemSpriteSheet, float speedMultiplier, int weight, string itemName, double timeBetweenFrames) : base(hitboxLength,hitboxWidth,baseDamage, baseAttackSpeed, spriteSheet, itemSpriteSheet, speedMultiplier, weight, itemName, timeBetweenFrames)
        {
            this.spriteSheet = spriteSheet;            
        }        
    }
}