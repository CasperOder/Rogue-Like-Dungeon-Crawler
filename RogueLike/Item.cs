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


        public Item(int coinGain, bool autoPickUp, SpriteSheet spriteSheet, Vector2 spawnPos):base(spriteSheet)
        {
            this.coinGain = coinGain;
            this.autoPickUp = autoPickUp;
            middlepos = spawnPos;

            hitbox = new Rectangle((int)middlepos.X - Constants.itemSize / 2, (int)middlepos.Y - Constants.itemSize / 2, Constants.itemSize, Constants.itemSize);

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet.texture, hitbox, Color.White);
        }

    }
}
