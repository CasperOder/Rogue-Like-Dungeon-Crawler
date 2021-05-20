using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace RogueLike
{
    class Room:GameObject
    {
        public bool upConnection, downConnection, rightConnection, leftConnection, exitRoom, isSpawn;
        public Point pos; //upperleft corner
        public Tile[,] tileArray;
        public string fileName;

        public static List<Tile> wallTiles = new List<Tile>();

        public Vector2 playerSpawnPoint;
        public Vector2 bossSpawnPoint;

        public Room(Vector2 pos, string fileName, SpriteSheet spriteSheet):base(spriteSheet)
        {
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, Constants.roomWidth, Constants.roomHeight);
            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);
            playerSpawnPoint = middlepos;
            this.pos.X = (int)pos.X;
            this.pos.Y = (int)pos.Y;
            this.spriteSheet = spriteSheet;
            this.fileName = fileName;
        }

        //I Level avgörs åt vilka håll det finns connections, sen kallar man på varje rooms CreateLevel() för att skapa rummet. Måste kallas för att programmet ska fungera
        public void CreateLevel(Random rnd, int currCircle)
        {
            List<string> stringList = ReadFromFile(fileName);
            tileArray = new Tile[stringList[0].Length, stringList.Count];

            int frameY = currCircle - 1;

            for(int i= 0; i< tileArray.GetLength(0);i++)
            {
                for(int j=0; j<tileArray.GetLength(1);j++)
                {
                    switch (stringList[j][i])
                    {
                        case 'C':

                            tileArray[i, j] = NewWallTile(i, j, frameY, rnd);

                            wallTiles.Add(tileArray[i, j]);

                            break;
                        case 'U':

                            if (!upConnection)
                            {
                                tileArray[i, j] = NewWallTile(i, j, frameY, rnd);
                                wallTiles.Add(tileArray[i, j]);
                            }
                            else
                                tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);


                            break;
                        case 'L':

                            if (!leftConnection)
                            {
                                tileArray[i, j] = NewWallTile(i, j, frameY, rnd);

                                wallTiles.Add(tileArray[i, j]);
                            }
                            else
                                tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);

                            break;
                        case 'R':

                            if (!rightConnection)
                            {
                                tileArray[i, j] = NewWallTile(i, j, frameY, rnd);
                                wallTiles.Add(tileArray[i, j]);
                            }
                            else
                                tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);


                            break;
                        case 'D':

                            if (!downConnection)
                            {
                                tileArray[i, j] = NewWallTile(i, j, frameY, rnd);
                                wallTiles.Add(tileArray[i, j]);
                            }
                            else
                                tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);

                            break;
                        case 'B':

                            tileArray[i, j] = NewWallTile(i, j, frameY, rnd);
                            wallTiles.Add(tileArray[i, j]);

                            break;
                        case 'E':

                            if (exitRoom)
                            {
                                tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);
                                Tile endTile = new Tile(SpriteSheetManager.stairTile, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize), 0, 0, false);
                                Level.endTileList.Add(endTile);
                            }
                            else if (!upConnection)
                            {
                                tileArray[i, j] = NewWallTile(i, j, frameY, rnd);
                                wallTiles.Add(tileArray[i, j]);
                            }
                            else
                                tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);

                            break;
                        case 'F':

                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);

                            break;
                        case 'S':

                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);
                            playerSpawnPoint = tileArray[i, j].middlepos;

                            break;
                        case 'O':

                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);
                            bossSpawnPoint = tileArray[i, j].middlepos;

                            break;
                        case 'G':

                            Tile rockTile = new Tile(SpriteSheetManager.rock, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize), 0, 0, true)
                            {
                                isRock = true
                            };
                            wallTiles.Add(rockTile);
                            Level.rockTiles.Add(rockTile);
                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);

                            break;
                        case 'e':

                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);
                            Level.enemySpawnTiles.Add(tileArray[i, j]);

                            break;
                        case 'i':

                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);

                            Level.itemsList.Add(LoadWeaponsAndItems.newStatUpgrade(tileArray[i, j].middlepos, false, rnd));

                            break;
                        case 'I':

                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);

                            Level.itemsList.Add(LoadWeaponsAndItems.newStatUpgrade(tileArray[i, j].middlepos, true, rnd));

                            break;
                        case 'w':

                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);

                            Level.itemsList.Add(LoadWeaponsAndItems.newWeaponItem(tileArray[i, j].middlepos, currCircle, false, rnd));

                            break;
                        case 'W':

                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);

                            Level.itemsList.Add(LoadWeaponsAndItems.newWeaponItem(tileArray[i, j].middlepos, currCircle, true, rnd));

                            break;
                        case 'K':
                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);

                            Level.shopKeeper = new NPC(SpriteSheetManager.shopKeeper, 0.5d, new Point(128, 128), new Vector2(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j), SpriteSheetManager.shopKeeperTextbox, new Point(200, 100));
                            break;
                        case '1':
                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);
                            if (!upConnection&& !exitRoom)
                            {
                                if (rnd.Next(2) == 0)
                                {
                                    Level.NewVase(tileArray[i, j].middlepos);
                                }
                            }
                            break;
                        case '2':
                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);
                            if (!rightConnection)
                            {
                                if (rnd.Next(2) == 0)
                                {
                                    Level.NewVase(tileArray[i, j].middlepos);
                                }
                            }
                            break;
                        case '3':
                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);
                            if (!downConnection)
                            {
                                if (rnd.Next(2) == 0)
                                {
                                    Level.NewVase(tileArray[i, j].middlepos);
                                }
                            }
                            break;
                        case '4':
                            tileArray[i, j] = NewFloorTile(i, j, frameY, rnd);
                            if (!leftConnection)
                            {
                                if (rnd.Next(2) == 0)
                                {
                                    Level.NewVase(tileArray[i, j].middlepos);
                                }
                            }
                            break;

                    }

                }
            }

        }

        private Tile NewWallTile(int i, int j, int frameY, Random rnd)
        {
            int frameX = rnd.Next(0, 4);

            return new Tile(SpriteSheetManager.wallTiles, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize), frameX, frameY, true);
        }

        private Tile NewFloorTile(int i, int j, int frameY, Random rnd)
        {
            int frameX = rnd.Next(0, 6);

            return new Tile(SpriteSheetManager.floorTile, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize), frameX, frameY, false);
        }


        public List<string> ReadFromFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            List<string> stringList = new List<string>();
            while (!sr.EndOfStream)
            {
                stringList.Add(sr.ReadLine());
            }
            sr.Close();
            return stringList;
        }

        public void Draw(SpriteBatch sb)
        {
            foreach(Tile t in tileArray)
            {
                if (t != null)
                {
                    t.Draw(sb);
                }
            }
        }

    }
}
