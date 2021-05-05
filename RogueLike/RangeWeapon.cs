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
        List<Projectile> projectilesOnScreenList;
        SpriteSheet projectileSpriteSheet;
        double projectileTimeBetweenFrames;

        public RangeWeapon(int baseDamage, int baseRange, float baseAttackSpeed, SpriteSheet projectileSpriteSheet, double projectileTimeBetweenFrames, SpriteSheet itemSpriteSheet, float speedMultiplier, int weight) :base(baseDamage, baseAttackSpeed, itemSpriteSheet, speedMultiplier, weight)
        {
            this.baseRange = baseRange;
            rangeMultiplyier = 1;
            this.projectileSpriteSheet = projectileSpriteSheet;
            this.projectileTimeBetweenFrames = projectileTimeBetweenFrames;
        }


        public void CreateNewProjectile(Vector2 startPos)
        {
            Projectile p = new Projectile(startPos, baseDamage, projectileSpriteSheet, projectileTimeBetweenFrames);
            projectilesOnScreenList.Add(p);
        }

        public void Update(Enemy e)
        {
            for(int p=0; p<projectilesOnScreenList.Count;p++)
            {
                projectilesOnScreenList[p].Update();

                if (Vector2.Distance(projectilesOnScreenList[p].startPos, projectilesOnScreenList[p].middlepos)>baseRange*rangeMultiplyier)
                {
                    projectilesOnScreenList.Remove(projectilesOnScreenList[p]);
                    p--;
                }

                if(e.hitbox.Intersects(projectilesOnScreenList[p].hitbox))
                {
                    //enemyn tar skada

                    projectilesOnScreenList.Remove(projectilesOnScreenList[p]);
                    p--;
                }
            }

            

        }

    }
}
