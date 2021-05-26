using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace RogueLike
{
    class WeaponItem:Item
    {
        /// <summary>
        /// Creates an instance of an item.
        /// </summary>
        /// <param name="weaponItem">Which weapon the item contains.</param>
        /// <param name="coinGain">How much currency the player gain through pickup.</param>
        /// <param name="autoPickUp">If the Item shoould be automatically pickedup or not.</param>
        /// <param name="spriteSheet">Which spritesheet the Item utilize.</param>
        /// <param name="spawnPos">Position of the Item.</param>
        /// <param name="itemType">Which type of Item it is.</param>
        /// <param name="itemName">Name of the Item.</param>
        public WeaponItem(Weapon weaponItem, int coinGain, bool autoPickUp, SpriteSheet spriteSheet, Vector2 spawnPos, ItemType itemType, string itemName) :base(coinGain, autoPickUp, spriteSheet, spawnPos, itemType, itemName)
        {
            this.weaponItem = weaponItem;
            hitbox = new Rectangle((int)middlepos.X - Constants.weaponItemSize / 2, (int)middlepos.Y - Constants.weaponItemSize / 2, Constants.weaponItemSize, Constants.weaponItemSize);
        }
    }
}