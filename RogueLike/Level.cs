﻿using System;
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
    static class Level
    {
        static SpriteBatch sb;
        static Player player;
        static Vector2 playerStartPos;
        public static Tile[,] backgroundTiles;
        private static RenderTarget2D backRenderTarget;
        private static RenderTarget2D frontRenderTarget;
        static int noOfRoomsX, noOfRoomsY;
        static List<Room> generatedRoomList = new List<Room>();
        static List<Room> backgroundRoomList = new List<Room>();
        static Room[,] roomArray;
        static Texture2D lineTex;
        static List<Enemy> enemyList= new List<Enemy>();
        public static List<Item> itemsList = new List<Item>();

        public static KeyboardState keyboardState, oldKeyboardState = Keyboard.GetState();

        public static int currency;

  
        public static void Load_Level(GraphicsDeviceManager g)
        {
            sb = new SpriteBatch(g.GraphicsDevice);
            noOfRoomsX = 8;
            noOfRoomsY = 8;
            roomArray = new Room[noOfRoomsX, noOfRoomsY];
            frontRenderTarget = new RenderTarget2D(g.GraphicsDevice, Constants.roomWidth*noOfRoomsX, Constants.roomHeight*noOfRoomsY);
            backRenderTarget = new RenderTarget2D(g.GraphicsDevice, Constants.roomWidth*noOfRoomsX, Constants.roomHeight*noOfRoomsY);

            //playerStartPos = new Vector2(Constants.roomWidth * Constants.startRoomCoords+Constants.roomWidth/2, Constants.roomHeight * Constants.startRoomCoords+Constants.roomHeight/2);

            //player = new Player(SpriteSheetManager.player, playerStartPos, 0.1d);

            roomArray[Constants.startRoomCoords, Constants.startRoomCoords] = new Room(new Vector2(Constants.roomWidth * Constants.startRoomCoords, Constants.roomHeight * Constants.startRoomCoords), "smallRoom.txt", SpriteSheetManager.ball);

            player = new Player(SpriteSheetManager.player, roomArray[Constants.startRoomCoords, Constants.startRoomCoords].middlepos, 0.1d);

            

            player.ChangeWeapon(LoadWeapons.testMelee);

            generatedRoomList.Add(roomArray[Constants.startRoomCoords, Constants.startRoomCoords]);

            Random rnd = new Random();
            LoadLayout(rnd);

            DrawOnFrontRenderTarget(g.GraphicsDevice);
            DrawOnBackRenderTarget(g.GraphicsDevice);

            
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
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * (x-1), Constants.roomHeight * y), "smallRoom.txt", SpriteSheetManager.ball);
                                        newRoom.rightConnection = true;
                                        roomArray[x, y].leftConnection = true;
                                        roomArray[x-1, y] = newRoom;
                                        generatedRoomList.Add(newRoom);
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
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * x, Constants.roomHeight * (y-1)), "smallRoom.txt", SpriteSheetManager.ball);
                                        newRoom.downConnection = true;
                                        roomArray[x, y].upConnection = true;
                                        roomArray[x, y-1] = newRoom;
                                        generatedRoomList.Add(newRoom);
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
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * (x+1), Constants.roomHeight * y), "smallRoom.txt", SpriteSheetManager.ball);
                                        newRoom.leftConnection = true;
                                        roomArray[x, y].rightConnection = true;
                                        roomArray[x+1, y] = newRoom;
                                        generatedRoomList.Add(newRoom);
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
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * x, Constants.roomHeight * (y+1)), "smallRoom.txt", SpriteSheetManager.ball);
                                        newRoom.upConnection = true;
                                        roomArray[x, y].downConnection = true;
                                        roomArray[x, y+1] = newRoom;
                                        generatedRoomList.Add(newRoom);
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
                        if (y == 0)
                        {
                            topRooms.Add(roomArray[x, y]);
                        }
                        else if (roomArray[x, y - 1] == null)
                        {
                            topRooms.Add(roomArray[x, y]);
                        }

                        if (x > 0)
                        {
                            if (roomArray[x - 1, y] != null)
                            {
                                chance = rnd.Next(1, 11);
                                if (chance == 1)
                                {
                                    roomArray[x, y].leftConnection = true;
                                    roomArray[x - 1, y].rightConnection = true;
                                }
                            }
                        }

                        if (y > 0)
                        {
                            if (roomArray[x, y - 1] != null)
                            {
                                chance = rnd.Next(1, 11);
                                if (chance == 1)
                                {
                                    roomArray[x, y].upConnection = true;
                                    roomArray[x, y-1].downConnection = true;
                                }
                            }
                        }

                        if (x + 1 < roomArray.GetLength(0))
                        {
                            if (roomArray[x + 1, y] != null)
                            {
                                chance = rnd.Next(1, 11);
                                if (chance == 1)
                                {
                                    roomArray[x, y].rightConnection = true;
                                    roomArray[x + 1, y].leftConnection = true;
                                }
                            }
                        }

                        if (y + 1 < roomArray.GetLength(1))
                        {
                            if (roomArray[x, y + 1] != null)
                            {
                                chance = rnd.Next(1, 11);
                                if (chance == 1)
                                {
                                    roomArray[x, y].downConnection = true;
                                    roomArray[x, y+1].upConnection = true;
                                }
                            }
                        }

                        chance = rnd.Next(1, 10);
                        
                        if(chance==1)
                        {
                            Enemy dummy = new DummyEnemy(SpriteSheetManager.dummy,1,roomArray[x,y].middlepos);
                            enemyList.Add(dummy);
                        }

                        Item coin = new Item(10, true, SpriteSheetManager.coin, roomArray[x, y].middlepos);
                        itemsList.Add(coin);

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

            WeaponItem sweepItem = new WeaponItem(LoadWeapons.sweepMelee, 0, false, LoadWeapons.sweepMelee.itemSpriteSheet, topRooms[chance].middlepos);
            itemsList.Add(sweepItem);



            foreach(Room r in generatedRoomList)
            {
                r.CreateLevel();
            }
            foreach(Room r in backgroundRoomList)
            {
                r.CreateLevel();
            }

        }
        
        public static void Update(GameTime gameTime)
        {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            player.Movement(gameTime);

            for(int i=0;i<itemsList.Count;i++)
            {
                if(player.hitbox.Intersects(itemsList[i].hitbox))
                {
                    if(itemsList[i].autoPickUp || (keyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter)))
                    {

                        if (itemsList[i] is WeaponItem)
                        {
                            if (!player.isAttacking)
                            {
                                player.ChangeWeapon(itemsList[i].weaponItem);
                                currency += itemsList[i].coinGain;
                                itemsList.RemoveAt(i);
                            }
                            break;
                        }
                        else
                        {
                            currency += itemsList[i].coinGain;
                            itemsList.RemoveAt(i);
                            break;
                        }



                    }
                }
            }

            foreach(Room r in generatedRoomList)
            {
                foreach(Tile t in r.tileArray)
                {
                    if(t!=null)
                    {
                        if (t.hitbox.Intersects(player.hitbox)) 
                        {
                            player.TileCollisionHandler(t);
                        }
                    }
                }
            }

            foreach(Enemy e in enemyList)
            {
                if(e.GetPlayerDistance(player)<e.enemySpottingRange)
                {
                    Vector2 direction = player.middlepos - e.middlepos; 
                    direction.Normalize();

                    foreach (Room r in generatedRoomList)
                    {
                        foreach(Tile t in r.tileArray)
                        {
                            if(t!=null)
                            {
                                //metod för att upptäcka om någon tile är ivägen                                 
                            }
                        }
                        
                    }                    
                }
            }

            Game1.camera.SetPosition(new Vector2(player.hitbox.X + player.hitbox.Width / 2, player.hitbox.Y + player.hitbox.Height / 2));
        }

        public static void DrawOnFrontRenderTarget(GraphicsDevice g)
        {

            g.SetRenderTarget(frontRenderTarget);
            g.Clear(Color.Transparent);
            sb.Begin();

            foreach(Room r in generatedRoomList)
            {
                r.Draw(sb);
            }

            sb.End();
            g.SetRenderTarget(null);
        }

        public static void DrawOnBackRenderTarget(GraphicsDevice g)
        {
            g.SetRenderTarget(backRenderTarget);
            g.Clear(Color.Transparent);
            sb.Begin();

            foreach (Room r in backgroundRoomList)
            {
                r.Draw(sb);
            }

            sb.End();
            g.SetRenderTarget(null);
        }

        public static void Draw(SpriteBatch sb)
        {
            player.Draw(sb);

            sb.Draw(frontRenderTarget, Vector2.Zero, Color.White);
            sb.Draw(backRenderTarget, Vector2.Zero, Color.White);

            foreach(Item i in itemsList)
            {
                i.Draw(sb);
            }

            foreach(Enemy e in enemyList)
            {
                e.Draw(sb);
            }
        }        
    }
}