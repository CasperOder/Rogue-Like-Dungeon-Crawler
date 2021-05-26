using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueLike
{
    class Item : GameObject
    {
        public int coinGain;
        public bool autoPickUp;

        public Weapon weaponItem; //används i WeaponItems, men behövs här

        public float multiplier;
        public string itemName;

        public enum ItemType
        {
            attackSpeedBoost,
            damageBoost,
            speedBoost,
            healthBoost,
            weaponType,
            coin,
            healAndSave,
        }
        public ItemType itemType;

        /// <summary>
        /// Creates an instance of an item.
        /// </summary>
        /// <param name="coinGain">How much currency the player gain through pickup.</param>
        /// <param name="autoPickUp">If the Item shoould be automatically pickedup or not.</param>
        /// <param name="spriteSheet">Which spritesheet the Item utilize.</param>
        /// <param name="spawnPos">Position of the Item.</param>
        /// <param name="itemType">Which type of Item it is.</param>
        /// <param name="itemName">Name of the Item.</param>
        public Item(int coinGain, bool autoPickUp, SpriteSheet spriteSheet, Vector2 spawnPos, ItemType itemType, string itemName) : base(spriteSheet)
        {
            this.itemType = itemType;
            this.coinGain = coinGain;
            this.autoPickUp = autoPickUp;
            this.itemName = itemName;
            middlepos = spawnPos;

            hitbox = new Rectangle((int)middlepos.X - Constants.itemSize / 2, (int)middlepos.Y - Constants.itemSize / 2, Constants.itemSize, Constants.itemSize);
        }
        /// <summary>
        /// Creates an instance of an item eith specific hitbox size.
        /// </summary>
        /// <param name="coinGain">How much currency the player gain through pickup.</param>
        /// <param name="autoPickUp">If the Item shoould be automatically pickedup or not.</param>
        /// <param name="spriteSheet">Which spritesheet the Item utilize.</param>
        /// <param name="spawnPos">Position of the Item.</param>
        /// <param name="itemType">Which type of Item it is.</param>
        /// <param name="itemName">Name of the Item.</param>
        public Item(int coinGain, bool autoPickUp, SpriteSheet spriteSheet, Vector2 spawnPos, ItemType itemType, string itemName, int hitboxX, int hitboxY) : base(spriteSheet)
        {
            this.itemType = itemType;
            this.coinGain = coinGain;
            this.autoPickUp = autoPickUp;
            this.itemName = itemName;
            middlepos = spawnPos;

            hitbox = new Rectangle((int)middlepos.X - hitboxX / 2, (int)middlepos.Y - hitboxY / 2, hitboxX, hitboxY);
        }


        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox, Color.White);
            sb.DrawString(Level.itemFont, itemName, new Vector2(hitbox.Left, hitbox.Top - 20), Color.White);
            if (coinGain < 0)
            {
                sb.DrawString(Level.itemFont, Math.Abs(coinGain).ToString(), new Vector2(hitbox.X, hitbox.Bottom), Color.White);
            }
        }
    }
}
