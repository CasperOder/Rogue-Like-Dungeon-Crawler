using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueLike
{
    class Item: GameObject
    {
        public int coinGain;
        public bool autoPickUp;

        public Weapon weaponItem; //används i WeaponItems

        public float multiplier;

        public enum ItemType
        {
            attackSpeedBoost,
            damageBoost,
            speedBoost,
            healthBoost,
            weaponType,
            coin,
        }
        public ItemType itemType;

        public Item(int coinGain, bool autoPickUp, SpriteSheet spriteSheet, Vector2 spawnPos, ItemType itemType):base(spriteSheet)
        {
            this.itemType = itemType;
            this.coinGain = coinGain;
            this.autoPickUp = autoPickUp;
            middlepos = spawnPos;

            hitbox = new Rectangle((int)middlepos.X - Constants.itemSize / 2, (int)middlepos.Y - Constants.itemSize / 2, Constants.itemSize, Constants.itemSize);
        }

        

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox, Color.White);
            if(coinGain<0)
            {
                sb.DrawString(Level.itemFont, Math.Abs(coinGain).ToString(), new Vector2(hitbox.X, hitbox.Bottom),Color.White);
            }
        }

    }
}
