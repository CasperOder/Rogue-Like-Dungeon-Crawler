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
    class Room : GameObject
    {
        public bool upConnection, downConnection, rightConnection, leftConnection, exitRoom, isSpawn;
        public Point pos; //upperleft corner
        public Tile[,] tileArray;
        public string fileName;
        public static List<Tile> wallTiles = new List<Tile>();
        public Vector2 playerSpawnPoint, bossSpawnPoint;

        /// <summary>
        /// Creates an instance of a room.
        /// </summary>
        /// <param name="pos">Position of the Room's upperleft corner.</param>
        /// <param name="fileName">The file which the room reads from.</param>
        public Room(Vector2 pos, string fileName) : base()
        {
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, Constants.roomWidth, Constants.roomHeight);
            middlepos = new Vector2(hitbox.Center.X, hitbox.Center.Y);
            playerSpawnPoint = middlepos;
            this.pos.X = (int)pos.X;
            this.pos.Y = (int)pos.Y;
            this.fileName = fileName;
        }

        /// <summary>
        /// Creates the whole room and all of its tiles.
        /// </summary>
        /// <param name="rnd">Random class.</param>
        /// <param name="currCircle">The current circle the game takes place on.</param>
        public void CreateLevel(Random rnd, int currCircle)
        {
            List<string> stringList = ReadFromFile(fileName);
            tileArray = new Tile[stringList[0].Length, stringList.Count];
            int frameY = currCircle - 1;

            for (int x = 0; x < tileArray.GetLength(0); x++)
            {
                for (int y = 0; y < tileArray.GetLength(1); y++)
                {
                    switch (stringList[y][x])
                    {
                        case 'C':

                            tileArray[x, y] = NewWallTile(x, y, frameY, rnd);
                            wallTiles.Add(tileArray[x, y]);

                            break;
                        case 'U':

                            if (!upConnection)
                            {
                                tileArray[x, y] = NewWallTile(x, y, frameY, rnd);
                                wallTiles.Add(tileArray[x, y]);
                            }
                            else
                                tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);

                            break;
                        case 'L':

                            if (!leftConnection)
                            {
                                tileArray[x, y] = NewWallTile(x, y, frameY, rnd);

                                wallTiles.Add(tileArray[x, y]);
                            }
                            else
                                tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);

                            break;
                        case 'R':

                            if (!rightConnection)
                            {
                                tileArray[x, y] = NewWallTile(x, y, frameY, rnd);
                                wallTiles.Add(tileArray[x, y]);
                            }
                            else
                                tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);


                            break;
                        case 'D':

                            if (!downConnection)
                            {
                                tileArray[x, y] = NewWallTile(x, y, frameY, rnd);
                                wallTiles.Add(tileArray[x, y]);
                            }
                            else
                                tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);

                            break;
                        case 'B':

                            tileArray[x, y] = NewWallTile(x, y, frameY, rnd);
                            wallTiles.Add(tileArray[x, y]);

                            break;
                        case 'E':

                            if (exitRoom)
                            {
                                tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);
                                Tile endTile = new Tile(SpriteSheetManager.stairTile, new Rectangle(pos.X + Constants.tileSize * x, pos.Y + Constants.tileSize * y, Constants.tileSize, Constants.tileSize), 0, 0, false);
                                Level.endTileList.Add(endTile);
                            }
                            else if (!upConnection)
                            {
                                tileArray[x, y] = NewWallTile(x, y, frameY, rnd);
                                wallTiles.Add(tileArray[x, y]);
                            }
                            else
                                tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);

                            break;
                        case 'F':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);

                            break;
                        case 'S':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);
                            playerSpawnPoint = tileArray[x, y].middlepos;

                            break;
                        case 'O':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);
                            bossSpawnPoint = tileArray[x, y].middlepos;

                            break;
                        case 'G':

                            Tile rockTile = new Tile(SpriteSheetManager.rock, new Rectangle(pos.X + Constants.tileSize * x, pos.Y + Constants.tileSize * y, Constants.tileSize, Constants.tileSize), 0, 0, true)
                            {
                                isRock = true
                            };
                            wallTiles.Add(rockTile);
                            Level.rockTiles.Add(rockTile);
                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);

                            break;
                        case 'e':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);
                            Level.enemySpawnTiles.Add(tileArray[x, y]);

                            break;
                        case 'i':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);

                            Level.itemsList.Add(LoadWeaponsAndItems.NewStatUpgrade(tileArray[x, y].middlepos, false, rnd));

                            break;
                        case 'I':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);
                            Level.itemsList.Add(LoadWeaponsAndItems.NewStatUpgrade(tileArray[x, y].middlepos, true, rnd));

                            break;
                        case 'w':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);
                            Level.itemsList.Add(LoadWeaponsAndItems.NewWeaponItem(tileArray[x, y].middlepos, currCircle, false, rnd));

                            break;
                        case 'W':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);
                            Level.itemsList.Add(LoadWeaponsAndItems.NewWeaponItem(tileArray[x, y].middlepos, currCircle, true, rnd));

                            break;
                        case 'K':
                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);

                            Level.shopKeeper = new NPC(SpriteSheetManager.shopKeeper, 0.5d, new Point(128, 128), new Vector2(pos.X + Constants.tileSize * x, pos.Y + Constants.tileSize * y), SpriteSheetManager.shopKeeperTextbox, new Point(200, 100));
                            break;
                        case '1':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);
                            if (!upConnection && !exitRoom)
                            {
                                if (rnd.Next(2) == 0)
                                {
                                    Level.NewVase(tileArray[x, y].middlepos);
                                }
                            }

                            break;
                        case '2':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);
                            if (!rightConnection)
                            {
                                if (rnd.Next(2) == 0)
                                {
                                    Level.NewVase(tileArray[x, y].middlepos);
                                }
                            }

                            break;
                        case '3':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);
                            if (!downConnection)
                            {
                                if (rnd.Next(2) == 0)
                                {
                                    Level.NewVase(tileArray[x, y].middlepos);
                                }
                            }

                            break;
                        case '4':

                            tileArray[x, y] = NewFloorTile(x, y, frameY, rnd);
                            if (!leftConnection)
                            {
                                if (rnd.Next(2) == 0)
                                {
                                    Level.NewVase(tileArray[x, y].middlepos);
                                }
                            }

                            break;
                        case 'o':

                            tileArray[x, y] = NewInvisibleTile(x, y, frameY, rnd);
                            wallTiles.Add(tileArray[x, y]);

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Sets a tile to a Wall Tile.
        /// </summary>
        /// <param name="x">Which placement the tile has in the X axis of the tile array.</param>
        /// <param name="y">Which placement the tile has in the y axis of the tile array.</param>
        /// <param name="frameY">Which row of sprites the tile uses from the spritesheet.</param>
        /// <param name="rnd">Random class.</param>
        /// <returns></returns>
        private Tile NewWallTile(int x, int y, int frameY, Random rnd)
        {
            int frameX = rnd.Next(0, 4);
            return new Tile(SpriteSheetManager.wallTiles, new Rectangle(pos.X + Constants.tileSize * x, pos.Y + Constants.tileSize * y, Constants.tileSize, Constants.tileSize), frameX, frameY, true);
        }

        /// <summary>
        /// Sets a tile to an Invisible Wall Tile.
        /// </summary>
        /// <param name="x">Which placement the tile has in the X axis of the tile array.</param>
        /// <param name="y">Which placement the tile has in the y axis of the tile array.</param>
        /// <param name="frameY">Which row of sprites the tile uses from the spritesheet.</param>
        /// <param name="rnd">Random class.</param>
        /// <returns></returns>
        private Tile NewInvisibleTile(int x, int y, int frameY, Random rnd)
        {
            int frameX = rnd.Next(0, 6);

            return new Tile(SpriteSheetManager.floorTile, new Rectangle(pos.X + Constants.tileSize * x, pos.Y + Constants.tileSize * y, Constants.tileSize, Constants.tileSize), frameX, frameY, true);
        }

        /// <summary>
        /// Sets a tile to a Floor Tile.
        /// </summary>
        /// <param name="x">Which placement the tile has in the X axis of the tile array.</param>
        /// <param name="y">Which placement the tile has in the y axis of the tile array.</param>
        /// <param name="frameY">Which row of sprites the tile uses from the spritesheet.</param>
        /// <param name="rnd">Random class.</param>
        /// <returns></returns>
        private Tile NewFloorTile(int x, int y, int frameY, Random rnd)
        {
            int frameX = rnd.Next(0, 6);

            return new Tile(SpriteSheetManager.floorTile, new Rectangle(pos.X + Constants.tileSize * x, pos.Y + Constants.tileSize * y, Constants.tileSize, Constants.tileSize), frameX, frameY, false);
        }

        /// <summary>
        /// Returns list of string from a specified file.
        /// </summary>
        /// <param name="fileName">File to read room from.</param>
        /// <returns></returns>
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
            foreach (Tile t in tileArray)
            {
                if (t != null)
                {
                    t.Draw(sb);
                }
            }
        }
    }
}