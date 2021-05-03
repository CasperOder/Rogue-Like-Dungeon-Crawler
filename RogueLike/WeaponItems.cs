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
        //public Weapon weaponItem;

        public WeaponItem(Weapon weaponItem, int coinGain, bool autoPickUp, SpriteSheet spriteSheet, Vector2 spawnPos) :base(coinGain, autoPickUp, spriteSheet, spawnPos)
        {
            this.weaponItem = weaponItem;
            hitbox = new Rectangle((int)middlepos.X - Constants.weaponItemSize / 2, (int)middlepos.Y - Constants.weaponItemSize / 2, Constants.weaponItemSize, Constants.weaponItemSize);

        }


    }
}
