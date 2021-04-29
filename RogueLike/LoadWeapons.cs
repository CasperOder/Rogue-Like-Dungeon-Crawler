using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    static class LoadWeapons
    {
        public static MeleeWeapon testMelee;



        public static void LoadAllWeapons()
        {
            testMelee = new MeleeWeapon(120, 40, 20, 2, SpriteSheetManager.arrow);



        }


    }
}
