using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RogueLike
{
    static class EnemyManager
    {
        static Enemy dummy;



        static void LoadEnemies()
        {
            dummy= new Enemy(SpriteSheetManager.fire, 0.1, Vector2.Zero, 300, 1000, 150, 60, 1d, 100, 100);
        }

        static public List<Enemy> spawnEnemies (List<Tile> tileList, int currentFloor)
        {
            List<Enemy> enemyList = new List<Enemy>();



            return enemyList;
        }

    }
}