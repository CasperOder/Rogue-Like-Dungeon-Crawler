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
        static Enemy fire;
        static Enemy book;
        static Enemy corpse;
        static List<Enemy> circleOneEnemyRoster = new List<Enemy>();
        static List<Enemy> circleTwoEnemyRoster = new List<Enemy>();
        static List<Enemy> circleThreeEnemyRoster = new List<Enemy>();
        static List<Enemy> circleFourEnemyRoster = new List<Enemy>();
        static List<Enemy> circleFiveEnemyRoster = new List<Enemy>();
        static List<Enemy> circleSixEnemyRoster = new List<Enemy>();
        static List<Enemy> circleSevenEnemyRoster = new List<Enemy>();
        static List<Enemy> circleEightEnemyRoster = new List<Enemy>();
        static List<Enemy> circleNineEnemyRoster = new List<Enemy>();

        /// <summary>
        /// Load all the enemies and assign them to specific circles.
        /// </summary>
        public static void LoadEnemies()
        {
            fire = new Enemy(SpriteSheetManager.fire, 0.1, 400, 1000, 250, 60, 2.2d, 100, 100, 1, 10, new Moveable_Object(SpriteSheetManager.iceBall, 0.1d), 400);
            book = new Enemy(SpriteSheetManager.book, 0.1, 300, 1000, 150, 60, 1.2d, 100, 100, 1, 5);
            corpse = new Enemy(SpriteSheetManager.corpse, 0.1, 400, 1000, 150, 60, 1d, 100, 100, 1, 10);
            circleOneEnemyRoster.Add(book);
            circleTwoEnemyRoster.Add(corpse);
            circleThreeEnemyRoster.Add(fire);
        }

        /// <summary>
        /// Returns a list of all the enemies in the circle.
        /// </summary>
        /// <param name="tileList">List of tiles the enemies can spawn on.</param>
        /// <param name="currentCircle">The current level circle.</param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        static public List<Enemy> spawnEnemies (List<Tile> tileList, int currentCircle, Random rnd)
        {
            List<Enemy> enemyList = new List<Enemy>();
            List<Enemy> enemyRoster = new List<Enemy>();
            int totalWeight =0 , weightLimit = 0;

            switch(currentCircle)
            {
                case 1:
                    weightLimit = 4;
                    enemyRoster = circleOneEnemyRoster;
                    break;
                case 2:
                    weightLimit = 7;
                    enemyRoster = circleTwoEnemyRoster;
                    break;
                case 3:
                    weightLimit = 10;
                    enemyRoster = circleThreeEnemyRoster;
                    break;
                case 4:
                    //weightLimit = 15;
                    //enemyRoster = circleFourEnemyRoster;
                    break;
                case 5:
                    //weightLimit = 20;
                    //enemyRoster = circleFiveEnemyRoster;
                    break;
                case 6:
                    //weightLimit = 25;
                    //enemyRoster = circleSixEnemyRoster;
                    break;
                case 7:
                    //weightLimit = 30;
                    //enemyRoster = circleSevenEnemyRoster;
                    break;
                case 8:
                    //weightLimit = 40;
                    //enemyRoster = circleEightEnemyRoster;
                    break;
                case 9:
                    //weightLimit = 50;
                    //enemyRoster = circleNineEnemyRoster;
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
            
            return enemyList;
        }        
    }
}