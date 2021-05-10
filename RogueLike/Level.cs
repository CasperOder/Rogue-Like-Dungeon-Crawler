﻿using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RogueLike
{
    static class Level
    {
        static SpriteBatch sb;
        public static Player player;
        static Vector2 playerStartPos;
        public static Tile[,] backgroundTiles;
        private static RenderTarget2D backRenderTarget;
        private static RenderTarget2D frontRenderTarget;
        static int noOfRoomsX, noOfRoomsY;
        public static List<Room> generatedRoomList = new List<Room>();
        static List<Room> backgroundRoomList = new List<Room>();
        static Room[,] roomArray;
        static Room shopRoom;

        public static NPC shopKeeper;

        static Texture2D lineTex;
        public static List<Tile> enemySpawnTiles= new List<Tile>();
        public static Ladder[] shopLadders= new Ladder[2]; //0 är ladder:n i roomArray, 1 är i shopen
        public static SpriteFont itemFont; //används för att avgöra texten nät items kostar. Får gärna flyttas -D

        public static List<Enemy> enemyList = new List<Enemy>();

        public static Random rnd = new Random();

        //static List<Enemy> enemyList= new List<Enemy>();
        public static List<Item> itemsList = new List<Item>();
        public static List<Tile> endTileList = new List<Tile>();
        public static List<Tile> rockTiles = new List<Tile>();
        public static int currentCircle, minimumNoOfRooms;

        static bool isBossRoom; //true om spelaren är i ett bossrum, false annars
        static List<Boss> bossList = new List<Boss>();
        static byte currentBoss = 1;
        static Vector2 bossStartPos;

        public static KeyboardState keyboardState, oldKeyboardState = Keyboard.GetState();

        public static int currency;

        public static void Load_Level(GraphicsDeviceManager g, ContentManager c)
        {
            itemFont = c.Load <SpriteFont> ("itemfont");
            sb = new SpriteBatch(g.GraphicsDevice);
            noOfRoomsX = 8;
            noOfRoomsY = 8;
            roomArray = new Room[noOfRoomsX, noOfRoomsY];

            shopRoom = new Room(new Vector2(-1000, -1000), "shopRoom.txt", SpriteSheetManager.wallTiles);


            frontRenderTarget = new RenderTarget2D(g.GraphicsDevice, Constants.roomWidth * noOfRoomsX, Constants.roomHeight * noOfRoomsY);
            backRenderTarget = new RenderTarget2D(g.GraphicsDevice, Constants.roomWidth * noOfRoomsX, Constants.roomHeight * noOfRoomsY);

            //playerStartPos = new Vector2(Constants.roomWidth * Constants.startRoomCoords+Constants.roomWidth/2, Constants.roomHeight * Constants.startRoomCoords+Constants.roomHeight/2);

            //player = new Player(SpriteSheetManager.player, playerStartPos, 0.1d);

            //roomArray[Constants.startRoomCoords, Constants.startRoomCoords] = new Room(new Vector2(Constants.roomWidth * Constants.startRoomCoords, Constants.roomHeight * Constants.startRoomCoords), "smallRoom.txt", SpriteSheetManager.ball);

            player = new Player(SpriteSheetManager.player, 0.1d);

            HUD.UpdateMaxHealthHUD((int)player.maxHealth);
            HUD.UpdateCurrentHealthHUD((int)player.health);

            player.ChangeWeapon(LoadWeaponsAndItems.testMelee);

            

            //Minos
            bossList.Add(new Minos(SpriteSheetManager.bossMinos, 0.1d, 400, 400));

            LoadNewLevel(g);            

        }

        public static void LoadNewLevel(GraphicsDeviceManager g)
        {
            isBossRoom = false;
            currentCircle++;

            switch (currentCircle)
            {
                case 1:
                    minimumNoOfRooms = 5;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(SoundManager.mainMenuTheme);
                    break;
                case 2:
                    minimumNoOfRooms = 10;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(SoundManager.mainMenuTheme);
                    break;
                case 3:
                    minimumNoOfRooms = 15;
                    break;
                case 4:
                    minimumNoOfRooms = 20;

                    break;
                case 5:
                    minimumNoOfRooms = 25;
                    break;
                case 6:
                    minimumNoOfRooms = 30;

                    break;
                case 7:
                    minimumNoOfRooms = 35;

                    break;
                case 8:
                    minimumNoOfRooms = 40;

                    break;
                case 9:
                    minimumNoOfRooms = 45;

                    break;
            }

            LoadLayout(rnd, g);

        }

        //Ritar room Layouten
        public static void LoadLayout(Random rnd, GraphicsDeviceManager g)
        {
            Room.wallTiles.Clear();
            generatedRoomList.Clear();
            backgroundRoomList.Clear();
            endTileList.Clear();
            enemyList.Clear();
            itemsList.Clear();



            
            roomArray[Constants.startRoomCoords, Constants.startRoomCoords] = new Room(new Vector2(Constants.roomWidth * Constants.startRoomCoords, Constants.roomHeight * Constants.startRoomCoords), "spawnRoom.txt", SpriteSheetManager.wallTiles);
            roomArray[Constants.startRoomCoords, Constants.startRoomCoords].isSpawn = true;
            generatedRoomList.Add(roomArray[Constants.startRoomCoords, Constants.startRoomCoords]);
            player.SetPlayerPosition(roomArray[Constants.startRoomCoords, Constants.startRoomCoords].playerSpawnPoint);

            int chance;

            //Loopar tills vi har ett önskat antal rum.
            while (generatedRoomList.Count < minimumNoOfRooms)
            {
                for (int x = 0; x < roomArray.GetLength(0); x++)
                {
                    for (int y = 0; y < roomArray.GetLength(1); y++)
                    {
                        if (roomArray[x, y] != null)
                        {
                            if (x > 0)
                            {
                                if (roomArray[x - 1, y] == null)
                                {
                                    chance = rnd.Next(1, 101);
                                    if (chance == 1)
                                    {
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * (x-1), Constants.roomHeight * y), "smallRoom.txt", SpriteSheetManager.wallTiles);

                                        newRoom.rightConnection = true;
                                        roomArray[x, y].leftConnection = true;
                                        roomArray[x - 1, y] = newRoom;
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
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * x, Constants.roomHeight * (y-1)), "smallRoom.txt", SpriteSheetManager.wallTiles);
                                        newRoom.downConnection = true;
                                        roomArray[x, y].upConnection = true;
                                        roomArray[x, y - 1] = newRoom;
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
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * (x+1), Constants.roomHeight * y), "smallRoom.txt", SpriteSheetManager.wallTiles);
                                        newRoom.leftConnection = true;
                                        roomArray[x, y].rightConnection = true;
                                        roomArray[x + 1, y] = newRoom;
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
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * x, Constants.roomHeight * (y+1)), "smallRoom.txt", SpriteSheetManager.wallTiles);
                                        newRoom.upConnection = true;
                                        roomArray[x, y].downConnection = true;
                                        roomArray[x, y + 1] = newRoom;
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
            for (int x = 0; x < roomArray.GetLength(0); x++)
            {
                for (int y = 0; y < roomArray.GetLength(1); y++)
                {
                    if (roomArray[x, y] != null)
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
                                    roomArray[x, y - 1].downConnection = true;
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
                                    roomArray[x, y + 1].upConnection = true;
                                }
                            }
                        }

                        //chance = rnd.Next(1, 10);
                        
                        //if(chance==1)
                        //{
                        //    Enemy dummy = new Enemy(SpriteSheetManager.fire, 0.1, roomArray[x,y].middlepos, 300, 1000, 150, 60, 1d, 100, 100);
                        //    //Enemy dummy = new DummyEnemy(SpriteSheetManager.dummy,1,roomArray[x,y].middlepos);
                        //    //dummy.health = 50;
                        //    enemyList.Add(dummy);
                        //}

                        Item coin= LoadWeaponsAndItems.coin(roomArray[x,y].middlepos);
                        itemsList.Add(coin);

                    }
                    else
                    {
                        Room r = new Room(new Vector2(Constants.roomWidth * x, Constants.roomHeight * y), "backRoom.txt", SpriteSheetManager.tempTile);
                        backgroundRoomList.Add(r);
                    }
                }
            }


            
            chance = rnd.Next(0, topRooms.Count);
            topRooms[chance].exitRoom = true; //Bestämmer vilket rum som ska leda till nästa krets

            //WeaponItem sweepItem = new WeaponItem(LoadWeaponsAndItems.sweepMelee, -50, false, LoadWeaponsAndItems.sweepMelee.itemSpriteSheet, topRooms[chance].middlepos);
            //itemsList.Add(sweepItem);

            //WeaponItem knifeItem = new WeaponItem(LoadWeaponsAndItems.knifeMelee, 0, false, LoadWeaponsAndItems.knifeMelee.itemSpriteSheet, topRooms[chance].middlepos);
            //itemsList.Add(knifeItem);


            bool shopLess= true;
            
            do
            {
                chance = rnd.Next(0, generatedRoomList.Count);

                if (!generatedRoomList[chance].isSpawn && !generatedRoomList[chance].exitRoom)
                {
                    generatedRoomList[chance].fileName = "smallRoom.txt";
                    shopLess = false;
                    shopLadders[0] = new Ladder(SpriteSheetManager.upLadder, generatedRoomList[chance].middlepos, shopRoom.middlepos, "Press 'Space' to enter shop");
                    shopLadders[1] = new Ladder(SpriteSheetManager.downLadder, shopRoom.middlepos, generatedRoomList[chance].middlepos, "Press 'Space' to exit shop'");
                }
            }
            while (shopLess);

            foreach (Room r in generatedRoomList)
            {
                r.CreateLevel(rnd, currentCircle);
            }
            foreach (Room r in backgroundRoomList)
            {
                r.CreateLevel(rnd, currentCircle);
            }
            shopRoom.CreateLevel(rnd, currentCircle);

            enemyList = EnemyManager.spawnEnemies(enemySpawnTiles, currentCircle, rnd);

            
            DrawOnFrontRenderTarget(g.GraphicsDevice);
            DrawOnBackRenderTarget(g.GraphicsDevice);

        }

        public static void LoadBossRoom(GraphicsDeviceManager g)
        {
            isBossRoom = true;

            MediaPlayer.Stop();
            MediaPlayer.Play(SoundManager.bossTheme);

            for (int x = 0; x < roomArray.GetLength(0); x++)
            {
                for (int y = 0; y < roomArray.GetLength(1); y++)
                {
                    roomArray[x, y] = null;
                }
            }


            Room.wallTiles.Clear();
            generatedRoomList.Clear();
            backgroundRoomList.Clear();
            endTileList.Clear();
            enemyList.Clear();
            itemsList.Clear();

            Room bossRoom = new Room(Vector2.Zero, "bossRoom.txt", SpriteSheetManager.tempTile);
            bossRoom.exitRoom = true;

            bossRoom.CreateLevel(rnd, currentCircle);        
            generatedRoomList.Add(bossRoom);

            DrawOnFrontRenderTarget(g.GraphicsDevice);
            DrawOnBackRenderTarget(g.GraphicsDevice);

            player.SetPlayerPosition(bossRoom.playerSpawnPoint);

            bossList[currentBoss - 1].SetPosition(bossRoom.bossSpawnPoint);

            bossList[currentBoss - 1].alive = true;
        }

        public static void Update(GameTime gameTime, GraphicsDeviceManager g)
        {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            player.Movement(gameTime);


            for (int i = 0; i < itemsList.Count; i++)
            {
                if (player.hitbox.Intersects(itemsList[i].hitbox))
                {
                    if (itemsList[i].autoPickUp || (keyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter)))
                    {
                        if (itemsList[i].itemType == Item.ItemType.weaponType)
                        {
                            if (!player.isAttacking && (itemsList[i].coinGain + currency) >= 0)
                            {
                                player.ChangeWeapon(itemsList[i].weaponItem);
                                currency += itemsList[i].coinGain;
                                HUD.UpdateCurrencyHUD(currency);
                                itemsList.RemoveAt(i);
                            }
                            break;
                        }
                        else if (itemsList[i].itemType == Item.ItemType.coin)
                        {
                            currency += itemsList[i].coinGain;
                            itemsList.RemoveAt(i);
                            HUD.UpdateCurrencyHUD(currency);
                            break;

                        }
                        else if ((itemsList[i].coinGain + currency) >= 0)
                        {

                            
                                player.UpdatePlayerStats(itemsList[i].itemType, itemsList[i].multiplier);
                                currency += itemsList[i].coinGain;
                                HUD.UpdateCurrencyHUD(currency);
                                itemsList.RemoveAt(i);
                                break;
                            
                        }



                        //if (itemsList[i] is WeaponItem)
                        //{
                        //    if (!player.isAttacking && (itemsList[i].coinGain + currency) >= 0)
                        //    {
                        //        player.ChangeWeapon(itemsList[i].weaponItem);
                        //        currency += itemsList[i].coinGain;
                        //        HUD.UpdateCurrencyHUD(currency);
                        //        itemsList.RemoveAt(i);
                        //    }
                        //    break;
                        //}
                        //else
                        //{
                        //    currency += itemsList[i].coinGain;
                        //    itemsList.RemoveAt(i);
                        //    HUD.UpdateCurrencyHUD(currency);
                        //    break;
                        //}



                    }
                }
            }

            
            foreach (Ladder l in shopLadders)
            {
                if (l.hitbox.Intersects(player.hitbox))
                {
                    l.showText = true;
                    if (keyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space))
                    {

                        l.Moveplayer(player);
                        break;
                    }

                }
                else
                {
                    l.showText = false;
                }
            }
            


            for (int e = 0; e < enemyList.Count; e++)
            {
                player.InflictDamage(enemyList[e]);
                if (enemyList[e].health <= 0)
                {
                    Item coin = LoadWeaponsAndItems.coin(enemyList[e].middlepos);
                    itemsList.Add(coin);
                    enemyList.RemoveAt(e);
                    e--;
                }
            }

            for (int b = 0; b < bossList.Count; b++)
            {
                player.InflictDamage(bossList[b]);
                if (bossList[b].health <= 0)
                {
                    bossList[b].alive = false;
                    RemoveRockTiles(g.GraphicsDevice);
                }
            }

            //enemyList[0].Update(gameTime);

            foreach (Enemy e in enemyList)
            {
                e.Update(gameTime);
            }

            foreach (Boss boss in bossList)
            {
                if (boss.alive)
                    boss.Update(gameTime);
            }

            if (keyboardState.IsKeyDown(Keys.R))
            {
                RemoveRockTiles(g.GraphicsDevice);
            }


            foreach (Tile t in endTileList)
            {
                if (t.hitbox.Intersects(player.hitbox))
                {
                    if (isBossRoom)
                    {
                        LoadNewLevel(g);
                    }
                    else
                    {
                        LoadBossRoom(g);
                    }
                    break;


                }
            }

            shopKeeper.Update(gameTime);

            Game1.camera.SetPosition(new Vector2(player.hitbox.X + player.hitbox.Width / 2, player.hitbox.Y + player.hitbox.Height / 2));
            HUD.Update(player.middlepos); //positionerar HUD:en, måste ligga bland det sista i denna metoden.
        }

        public static void RemoveRockTiles(GraphicsDevice g)
        {
            for (int r = 0; r < Room.wallTiles.Count; r++)
            {
                if (Room.wallTiles[r].isRock)
                {
                    Room.wallTiles.RemoveAt(r);
                    r--;
                }
            }

            rockTiles.Clear();
            DrawOnFrontRenderTarget(g);
        }

        public static void DrawOnFrontRenderTarget(GraphicsDevice g)
        {

            g.SetRenderTarget(frontRenderTarget);
            g.Clear(Color.Transparent);
            sb.Begin();

            foreach (Tile r in rockTiles)
            {
                r.Draw(sb);
            }

            foreach (Room r in generatedRoomList)
            {
                r.Draw(sb);
            }

            sb.End();
            g.SetRenderTarget(null);
        }

        public static void UnhitAllEnemies()
        {
            foreach (Enemy e in enemyList)
            {
                e.beenHit = false;
            }
            foreach (Boss boss in bossList)
            {
                if (boss.alive)
                    boss.beenHit = false;
            }
        }




        public static void DrawOnBackRenderTarget(GraphicsDevice g)
        {
            g.SetRenderTarget(backRenderTarget);
            g.Clear(Color.Transparent);
            sb.Begin();


            foreach (Tile t in endTileList)
            {
                t.Draw(sb);
            }

            foreach (Room r in backgroundRoomList)
            {
                r.Draw(sb);
            }

            sb.End();
            g.SetRenderTarget(null);
        }

        public static void Draw(SpriteBatch sb)
        {
            sb.Draw(frontRenderTarget, Vector2.Zero, Color.White);
            sb.Draw(backRenderTarget, Vector2.Zero, Color.White);
            shopRoom.Draw(sb);

            foreach (Boss boss in bossList)
            {
                if (boss.alive)
                    boss.Draw(sb);
            }

            foreach (Tile tile in rockTiles)
            {
                tile.Draw(sb);
            }

            foreach (Ladder l in shopLadders)
            {
                l.Draw(sb);
            }

            shopKeeper.Draw(sb);

            player.Draw(sb);


            foreach (Item i in itemsList)
            {
                i.Draw(sb);
            }

            foreach (Enemy e in enemyList)
            {
                e.Draw(sb);
            }

            HUD.Draw(sb);
        }
    }
}