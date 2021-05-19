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
        static List<Enemy> circleTwoEnemyRoster = new List<Enemy>();
        static List<Enemy> circleThreeEnemyRoster = new List<Enemy>();
        static List<Enemy> circleFourEnemyRoster = new List<Enemy>();
        static List<Enemy> circleFiveEnemyRoster = new List<Enemy>();
        static List<Enemy> circleSixEnemyRoster = new List<Enemy>();
        static List<Enemy> circleSevenEnemyRoster = new List<Enemy>();
        static List<Enemy> circleEightEnemyRoster = new List<Enemy>();
        static List<Enemy> circleNineEnemyRoster = new List<Enemy>();


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
                    weightLimit = 4;
                    enemyRoster = circleOneEnemyRoster;
                    break;
                case 2:
                    //weightLimit = 7;
                    //enemyRoster = circleTwoEnemyRoster;
                    break;
                case 3:
                    //weightLimit = 10;
                    //enemyRoster = circleThreeEnemyRoster;
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
            
            //Copy enemyRoster, itsället för "="


            return enemyList;
        }


    }
}