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
        public bool upConnection, downConnection, rightConnection, leftConnection, exitRoom;
        public Point pos; //upperleft corner
        public Tile[,] tileArray;
        string fileName;

        public static List<Tile> wallTiles = new List<Tile>();

        public Vector2 playerSpawnPoint;


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
        public void CreateLevel()
        {
            List<string> stringList = ReadFromFile(fileName);
            tileArray = new Tile[stringList[0].Length, stringList.Count];

            for(int i= 0; i< tileArray.GetLength(0);i++)
            {
                for(int j=0; j<tileArray.GetLength(1);j++)
                {
                    if(stringList[j][i] == 'C')
                    {
                        tileArray[i, j] = new Tile(spriteSheet, new Rectangle(pos.X+Constants.tileSize * i, pos.Y+Constants.tileSize * j, Constants.tileSize, Constants.tileSize));
                        wallTiles.Add(tileArray[i, j]);
                    }
                    else if (stringList[j][i] == 'U')
                    {
                        if(!upConnection)
                        {
                            tileArray[i, j] = new Tile(spriteSheet, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize));
                            wallTiles.Add(tileArray[i, j]);
                        }
                        else
                            tileArray[i, j] = new Tile(SpriteSheetManager.floorTile, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize), false);

                    }
                    else if (stringList[j][i] == 'L')
                    {
                        if(!leftConnection)
                        {
                            tileArray[i, j] = new Tile(spriteSheet, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize));

                            wallTiles.Add(tileArray[i, j]);
                        }
                        else
                            tileArray[i, j] = new Tile(SpriteSheetManager.floorTile, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize), false);

                                             

                    }
                    else if (stringList[j][i] == 'R')
                    {
                        if(!rightConnection)
                        {
                            tileArray[i, j] = new Tile(spriteSheet, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize));
                            wallTiles.Add(tileArray[i, j]);
                        }
                        else
                            tileArray[i, j] = new Tile(SpriteSheetManager.floorTile, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize), false);
                    }
                    else if (stringList[j][i] == 'D')
                    {
                        if (!downConnection)
                        {
                            tileArray[i, j] = new Tile(spriteSheet, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize));
                            wallTiles.Add(tileArray[i, j]);
                        }
                        else
                            tileArray[i, j] = new Tile(SpriteSheetManager.floorTile, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize), false);
                    }
                    else if (stringList[j][i] == 'B')
                    {                        
                        tileArray[i, j] = new Tile(spriteSheet, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize));
                        wallTiles.Add(tileArray[i, j]);
                    }
                    else if (stringList[j][i] == 'E')
                    {
                        if(exitRoom)
                        {
                            tileArray[i, j] = new Tile(SpriteSheetManager.floorTile, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize), false);
                            Tile endTile = new Tile(SpriteSheetManager.stairTile, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize));
                            Level.endTileList.Add(endTile);
                        }
                        else if (!upConnection)
                        {
                            tileArray[i, j] = new Tile(spriteSheet, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize));
                            wallTiles.Add(tileArray[i, j]);
                        }
                        else
                            tileArray[i, j] = new Tile(SpriteSheetManager.floorTile, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize), false);

                    }
                    else if (stringList[j][i] == 'F')
                    {
                        tileArray[i, j] = new Tile(SpriteSheetManager.floorTile, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize), false);
                    }
                    else if (stringList[j][i] == 'S')
                    {
                        tileArray[i, j] = new Tile(spriteSheet, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize));
                        playerSpawnPoint = tileArray[i, j].middlepos;
                        tileArray[i, j] = null;

                    }
                    else if (stringList[j][i] == 'G')
                    {
                        Tile rockTile = new Tile(SpriteSheetManager.rock, new Rectangle(pos.X + Constants.tileSize * i, pos.Y + Constants.tileSize * j, Constants.tileSize, Constants.tileSize));
                        rockTile.isRock = true;
                        wallTiles.Add(rockTile);
                        Level.rockTiles.Add(rockTile);

                    }

                }
            }

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
