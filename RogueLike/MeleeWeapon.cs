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
        Rectangle hitbox;

        public MeleeWeapon(Rectangle hitbox, int damage, int range, float attackSpeed) : base(damage, range, attackSpeed)
        {
            this.hitbox = hitbox;
        }
        
    

        public void InflictDamage(Enemy e)
        {
            if(e.hitbox.Intersects(hitbox))
            {

            }
        }



    }
}
