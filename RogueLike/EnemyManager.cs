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
        static List<Enemy> circleOneEnemyRoster = new List<Enemy>();


        public static void LoadEnemies()
        {
            dummy= new Enemy(SpriteSheetManager.fire, 0.1, 300, 1000, 150, 60, 1d, 100, 100, 1);
            circleOneEnemyRoster.Add(dummy);

        }

        static public List<Enemy> spawnEnemies (List<Tile> tileList, int currentFloor, Random rnd)
        {
            List<Enemy> enemyList = new List<Enemy>();
            List<Enemy> enemyRoster = new List<Enemy>();
            int totalWeight =0 , weightLimit = 0;

            switch(currentFloor)
            {
                case 1:
                    weightLimit = 2;
                    enemyRoster = circleOneEnemyRoster;
                    break;
                case 2:

                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
            }

            while (totalWeight < weightLimit && tileList.Count != 0) 
            {
                int randomTile = rnd.Next(0, tileList.Count());
                int randomEnemy = rnd.Next(0, enemyRoster.Count());
                Enemy newEnemy = enemyRoster[randomEnemy].copyEnemy();

                totalWeight += newEnemy.spawnWeight;
                newEnemy.SetSpawn(tileList[randomTile].middlepos);
                enemyList.Add(newEnemy);
                
                tileList.Remove(tileList[randomTile]);
                
            }
            
            //Copy enemyRoster, itsället för "="


            return enemyList;
        }


    }
}