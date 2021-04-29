using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    static class LoadWeapons
    {
        public static MeleeWeapon testMelee, sweepMelee;



        public static void LoadAllWeapons()
        {
            testMelee = new MeleeWeapon(120, 40, 20, 2, SpriteSheetManager.arrow, SpriteSheetManager.arrowItem, 0.9f);
            sweepMelee = new MeleeWeapon(60, 180, 40, 1, SpriteSheetManager.sweep, SpriteSheetManager.sweepItem, 0.69f);


        }


    }
}
