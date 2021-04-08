using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueLike
{
    class Level
    {
        static Player player;
        static Vector2 playerStartPos;
        public static Tile[,] backgroundTiles;
        public static RenderTarget2D backRenderTarget;
        public static RenderTarget2D frontRenderTarget;

        static List<Room> generatedRoomList = new List<Room>();
        static List<Room> backgroundRoomList = new List<Room>();
        static Room[,] roomArray = new Room[9, 9];
        
        public static Room testRoom; //tillfälligt room, för att testa och se så att allt fungerar


        public static void LoadBackgroundTiles(GraphicsDevice graphicsDevice)
        {
            List<string> levelReader = new List<string>();

            StreamReader sr = new StreamReader("Background.txt");
            while (!sr.EndOfStream)
            {
                levelReader.Add(sr.ReadLine());
            }
            sr.Close();

            backgroundTiles = new Tile[levelReader[0].Length, levelReader.Count];

            for (int a = 0; a < levelReader.Count; a++)
            {
                for (int b = 0; b < levelReader[a].Length; b++)
                {
                    if (levelReader[a][b] == 'w')
                    {

                        backgroundTiles[b, a] = new Tile(SpriteSheetManager.ball, new Rectangle(Constants.tileSize * b, Constants.tileSize * a, Constants.tileSize, Constants.tileSize));                        

                        

                    }
                }
            }
        }

        public static void Load_Level()
        {
            playerStartPos = new Vector2(Constants.roomWidth * Constants.startRoomCoords, Constants.roomHeight * Constants.startRoomCoords);

            player = new Player(SpriteSheetManager.player, playerStartPos, 0.1d);

            //    testRoom = new Room(new Vector2(600, 400), "smallRoom.txt", SpriteSheetManager.ball);
            //    testRoom.leftConnection = true;
            //    testRoom.CreateLevel();

            roomArray[Constants.startRoomCoords, Constants.startRoomCoords] = new Room(new Vector2(Constants.roomWidth * Constants.startRoomCoords, Constants.roomHeight * Constants.startRoomCoords), "smallRoom.txt", SpriteSheetManager.ball);

            generatedRoomList.Add(roomArray[Constants.startRoomCoords, Constants.startRoomCoords]);

            Random rnd = new Random();
            LoadLayout(rnd);

        }

        //Ritar room Layouten
        public static void LoadLayout(Random rnd)
        {
            int chance;

            //Loopar tills vi har ett önskat antal rum.
            while (generatedRoomList.Count < Constants.minimumNumberOfRooms)
            {
                for (int x = 0; x < roomArray.GetLength(0); x++)
                {
                    for (int y = 0; y < roomArray.GetLength(1); y++)
                    {
                        if (roomArray[x,y]!=null)
                        {
                            if (x > 0)
                            {
                                if (roomArray[x - 1, y] == null)
                                {
                                    chance = rnd.Next(1, 101);
                                    if (chance == 1)
                                    {
                                        NewRoom(x - 1, y);
                                    }
                                }
                            }
                            if (y > 0)
                            {
                                if (roomArray[x, y - 1] == null)
                                {
                                    chance = rnd.Next(1, 101);
                                    if (chance == 1)
                                    {
                                        NewRoom(x, y - 1);
                                    }
                                }

                            }

                            if (x + 1 < roomArray.GetLength(0))
                            {
                                if (roomArray[x + 1, y] == null)
                                {
                                    chance = rnd.Next(1, 101);
                                    if (chance == 1)
                                    {
                                        NewRoom(x + 1, y);
                                    }
                                }

                            }

                            if (y + 1 < roomArray.GetLength(1))
                            {
                                if (roomArray[x, y + 1] == null)
                                {
                                    chance = rnd.Next(1, 101);
                                    if (chance == 1)
                                    {
                                        NewRoom(x, y + 1);
                                    }
                                }

                            }
                        }
                    }
                }
            }

            List<Room> topRooms = new List<Room>();
           
            //Loopar för att avgöra på vilka sidor room har connections
            for(int x=0; x < roomArray.GetLength(0);x++)
            {
                for(int y=0;y<roomArray.GetLength(1);y++)
                {
                    if(roomArray[x,y]!=null)
                    {
                        if(y==0)
                        {
                            topRooms.Add(roomArray[x, y]);
                        }
                        else if(roomArray[x,y-1]==null)
                        {
                            topRooms.Add(roomArray[x, y]);
                        }

                        if(x>0)
                        {
                            if(roomArray[x-1,y] != null)
                            {
                                roomArray[x, y].leftConnection = true;
                            }
                        }

                        if(y>0)
                        {
                            if (roomArray[x, y-1] != null)
                            {
                                roomArray[x, y].upConnection = true;
                            }                            
                        }

                        if(x + 1 < roomArray.GetLength(0))
                        {
                            if (roomArray[x+1, y] != null)
                            {
                                roomArray[x, y].rightConnection = true;
                            }
                        }

                        if(y + 1 < roomArray.GetLength(1))
                        {
                            if (roomArray[x, y+1] != null)
                            {
                                roomArray[x, y].downConnection = true;
                            }
                        }
                    }
                    else
                    {
                        Room r = new Room(new Vector2(Constants.roomWidth * x, Constants.roomHeight * y), "backRoom.txt", SpriteSheetManager.tempTile);
                        backgroundRoomList.Add(r);
                    }
                }
            }

            chance = rnd.Next(1, topRooms.Count);
            topRooms[chance].exitRoom = true; //Bestämmer vilket rum som ska leda till nästa krets

            foreach(Room r in generatedRoomList)
            {
                r.CreateLevel();
            }
            foreach(Room r in backgroundRoomList)
            {
                r.CreateLevel();
            }
        }

        public static void NewRoom(int x, int y) //Metod för att skapa nytt room, så slipper jag skriva om samma sak 1000 ggr
        {
            Room newRoom = new Room(new Vector2(Constants.roomWidth * x, Constants.roomHeight * y), "smallRoom.txt", SpriteSheetManager.ball);
            roomArray[x, y] = newRoom;
            generatedRoomList.Add(newRoom);
        }

        public static void Update(GameTime gameTime)
        {
            player.Movement(gameTime);
            Game1.camera.SetPosition(new Vector2(player.hitbox.X + player.hitbox.Width / 2, player.hitbox.Y + player.hitbox.Height / 2));
        }

        public static void Draw(SpriteBatch sb)
        {
            player.Draw(sb);

            foreach(Room r in generatedRoomList)
            {
                r.Draw(sb);                
            }
            foreach(Room r in backgroundRoomList)
            {
                r.Draw(sb);
            }


            //testRoom.Draw(sb);
        }

    }
}
