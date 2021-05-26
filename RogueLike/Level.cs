using System;
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

        static GraphicsDeviceManager graphics;
        static SpriteBatch sb;
        public static Player player;
        private static RenderTarget2D backRenderTarget;
        private static RenderTarget2D frontRenderTarget;
        private static RenderTarget2D shopRenderTarget;
        public static List<Room> generatedRoomList = new List<Room>();
        static List<Room> backgroundRoomList = new List<Room>();
        static Room[,] roomArray;
        static Room[,] shopRoomArray;
        static Room shopRoom;
        public static List<Projectile> projectilesOnScreenList;
        public static NPC shopKeeper;
        public static List<Tile> enemySpawnTiles = new List<Tile>();
        public static Ladder[] shopLadders = new Ladder[2]; //0 är ladder:n i roomArray, 1 är i shopen
        public static SpriteFont itemFont; //används för att avgöra texten när items har ett pris. Får gärna flyttas -D
        public static List<Enemy> enemyList;
        public static Random rnd = new Random();
        public static List<Item> itemsList = new List<Item>();
        public static List<Tile> endTileList = new List<Tile>();
        public static List<Tile> rockTiles = new List<Tile>();
        public static int currentCircle, minimumNoOfRooms;

        static bool isBossRoom; //true om spelaren är i ett bossrum, false annars
        static bool loadBoss; //false om man startar från en savefile, annars true
        static List<Boss> bossList;
        static byte currentBoss = 1;

        public static KeyboardState keyboardState, oldKeyboardState = Keyboard.GetState();

        public static int currency;

        public static List<Moveable_Object> vaseList = new List<Moveable_Object>();

        /// <summary>
        /// Loads the levels.
        /// </summary>
        /// <param name="graphicDeviceManager"></param>
        /// <param name="content"></param>
        public static void Load_Level(GraphicsDeviceManager graphicDeviceManager, ContentManager content)
        {
            graphics = graphicDeviceManager;
            itemFont = content.Load<SpriteFont>("itemfont");
            sb = new SpriteBatch(graphics.GraphicsDevice);

            roomArray = new Room[Constants.noOfRoomsX, Constants.noOfRoomsY];

            bossList = new List<Boss>();
            enemyList = new List<Enemy>();
            projectilesOnScreenList = new List<Projectile>();

            shopRoomArray = new Room[3, 3];

            loadBoss = true;

            frontRenderTarget = new RenderTarget2D(graphics.GraphicsDevice, Constants.roomWidth * Constants.noOfRoomsX, Constants.roomHeight * Constants.noOfRoomsY);
            backRenderTarget = new RenderTarget2D(graphics.GraphicsDevice, Constants.roomWidth * Constants.noOfRoomsX, Constants.roomHeight * Constants.noOfRoomsY);
            shopRenderTarget = new RenderTarget2D(graphics.GraphicsDevice, Constants.roomWidth * 3, Constants.roomHeight * 3);
            player = new Player(SpriteSheetManager.player, 0.1d);

            HUD.UpdateMaxHealthHUD((int)player.maxHealth);
            HUD.UpdateCurrentHealthHUD((int)player.health);

            player.ChangeWeapon(LoadWeaponsAndItems.testMelee);

            currency = 0;
            currentCircle = 0;

            LoadNewCircle();
        }


        /// <summary>
        /// Loads information about the state of the level from a savefile.
        /// </summary>
        public static void LoadFromSave()
        {
            SavefileHandler.LoadFromFile();
            loadBoss = false;
            LoadBossRoom();
            loadBoss = true;
            HUD.UpdateCurrencyHUD(currency);
        }

        /// <summary>
        /// Loads a new circle of the level.
        /// </summary>
        public static void LoadNewCircle()
        {
            isBossRoom = false;
            currentCircle++;

            switch (currentCircle)
            {
                case 1:
                    minimumNoOfRooms = 10;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(SoundManager.mainMenuTheme);
                    break;
                case 2:
                    minimumNoOfRooms = 15;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(SoundManager.mainMenuTheme);
                    break;
                case 3:
                    minimumNoOfRooms = 20;
                    break;
                case 4:
                    minimumNoOfRooms = 25;

                    break;
                case 5:
                    minimumNoOfRooms = 30;
                    break;
                case 6:
                    minimumNoOfRooms = 35;

                    break;
                case 7:
                    minimumNoOfRooms = 40;

                    break;
                case 8:
                    minimumNoOfRooms = 45;

                    break;
                case 9:
                    minimumNoOfRooms = 50;

                    break;
            }

            LoadLayout(rnd);
        }

        /// <summary>
        /// Loads the layout of the circle.
        /// </summary>
        /// <param name="rnd">Random class.</param>
        public static void LoadLayout(Random rnd)
        {
            Room.wallTiles.Clear();
            generatedRoomList.Clear();
            backgroundRoomList.Clear();
            endTileList.Clear();
            enemyList.Clear();
            itemsList.Clear();

            AmbientEffectManager.NewCircle();



            roomArray[Constants.startRoomCoords, Constants.startRoomCoords] = new Room(new Vector2(Constants.roomWidth * Constants.startRoomCoords, Constants.roomHeight * Constants.startRoomCoords), "spawnRoom.txt")
            {
                isSpawn = true
            };
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
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * (x - 1), Constants.roomHeight * y), "smallRoom.txt")
                                        {
                                            rightConnection = true
                                        };

                                        //newRoom.rightConnection = true;
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
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * x, Constants.roomHeight * (y - 1)), "smallRoom.txt")
                                        {
                                            downConnection = true
                                        };
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
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * (x + 1), Constants.roomHeight * y), "smallRoom.txt")
                                        {
                                            leftConnection = true
                                        };
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
                                        Room newRoom = new Room(new Vector2(Constants.roomWidth * x, Constants.roomHeight * (y + 1)), "smallRoom.txt")
                                        {
                                            upConnection = true
                                        };
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

            for (int x = 0; x < shopRoomArray.GetLength(0); x++)
            {
                for (int y = 0; y < shopRoomArray.GetLength(1); y++)
                {
                    if (x == 1 && y == 1)
                    {
                        shopRoom = new Room(new Vector2(Constants.roomWidth * x - 2000, Constants.roomHeight * y - 2000), "shopRoom.txt");
                        shopRoomArray[x, y] = shopRoom;
                    }
                    else
                    {
                        shopRoomArray[x, y] = new Room(new Vector2(Constants.roomWidth * x - 2000, Constants.roomHeight * y - 2000), "backRoom.txt");
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



                    }
                    else
                    {
                        Room r = new Room(new Vector2(Constants.roomWidth * x, Constants.roomHeight * y), "backRoom.txt");
                        backgroundRoomList.Add(r);
                    }
                }
            }

            chance = rnd.Next(0, topRooms.Count);
            topRooms[chance].exitRoom = true; //Bestämmer vilket rum som ska leda till nästa krets            

            bool shopLess = true;

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

            graphics.GraphicsDevice.SetRenderTarget(shopRenderTarget);
            graphics.GraphicsDevice.Clear(Color.Transparent);
            sb.Begin();

            foreach (Room room in shopRoomArray)
            {
                room.CreateLevel(rnd, currentCircle);
            }

            sb.End();
            graphics.GraphicsDevice.SetRenderTarget(null);

            enemyList = EnemyManager.spawnEnemies(enemySpawnTiles, currentCircle, rnd);

            DrawOnFrontRenderTarget();
            DrawOnBackRenderTarget();
        }

        /// <summary>
        /// Loads the boss room of the level.
        /// </summary>
        public static void LoadBossRoom()
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
            vaseList.Clear();

            Room bossRoom = new Room(Vector2.Zero, "bossRoom.txt")
            {
                exitRoom = true
            };

            bossRoom.CreateLevel(rnd, currentCircle);
            generatedRoomList.Add(bossRoom);

            if (loadBoss)
            {
                switch (currentCircle)
                {
                    case 1:
                        //Minos
                        bossList.Add(new Minos(SpriteSheetManager.bossMinos, 0.1d, 400, 400));
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
            }


            DrawOnFrontRenderTarget();
            DrawOnBackRenderTarget();

            player.SetPlayerPosition(bossRoom.playerSpawnPoint);

            if (bossList.Count != 0)
            {
                bossList[currentBoss - 1].SetPosition(bossRoom.bossSpawnPoint);

                bossList[currentBoss - 1].alive = true;
            }
            else
            {
                RemoveRockTiles();
            }

        }

        /// <summary>
        /// Updates all of the activities in the Level class.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            player.Movement(gameTime);


            AmbientEffectManager.UpdateAmbientEffects(gameTime, currentCircle, player.middlepos);

            for (int p = 0; p < projectilesOnScreenList.Count; p++)
            {
                projectilesOnScreenList[p].Update(gameTime);


                for (int vase = 0; vase < vaseList.Count; vase++)
                {
                    projectilesOnScreenList[p].CheckTargetCollision(vaseList[vase]);

                    if (vaseList[vase].health <= 0)
                    {
                        if (rnd.Next(5) == 0)
                        {
                            Item coin = LoadWeaponsAndItems.Coin(vaseList[vase].middlepos);
                            itemsList.Add(coin);
                        }
                        AmbientEffectManager.AddShatteredVase(vaseList[vase].middlepos);
                        vaseList.RemoveAt(vase);
                        vase--;
                    }
                }

                for (int e = 0; e < enemyList.Count; e++)
                {
                    projectilesOnScreenList[p].CheckTargetCollision(enemyList[e]);

                    if (enemyList[e].health <= 0)
                    {
                        Item coin = LoadWeaponsAndItems.Coin(enemyList[e].middlepos);
                        itemsList.Add(coin);
                        enemyList.RemoveAt(e);
                        e--;
                    }
                }


                for (int b = 0; b < bossList.Count; b++)
                {
                    projectilesOnScreenList[p].CheckTargetCollision(bossList[b]);

                    if (bossList[b].health <= 0 && bossList[b].alive)
                    {
                        bossList[b].alive = false;
                        RemoveRockTiles();
                        itemsList.Add(LoadWeaponsAndItems.HealPotion(bossList[b].middlepos, 50));
                    }
                }

                if (projectilesOnScreenList[p].isColliding)
                {
                    projectilesOnScreenList.RemoveAt(p);
                    p--;
                }

            }

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

            for (int vase = 0; vase < vaseList.Count; vase++)
            {
                player.InflictMeleeDamage(vaseList[vase]);
                if (vaseList[vase].health <= 0)
                {
                    if (rnd.Next(5) == 0)
                    {
                        Item coin = LoadWeaponsAndItems.Coin(vaseList[vase].middlepos);
                        itemsList.Add(coin);
                    }
                    AmbientEffectManager.AddShatteredVase(vaseList[vase].middlepos);
                    vaseList.RemoveAt(vase);
                    vase--;
                }
            }

            for (int e = 0; e < enemyList.Count; e++)
            {
                enemyList[e].Update(gameTime);
                player.InflictMeleeDamage(enemyList[e]);
                if (enemyList[e].health <= 0)
                {
                    Item coin = LoadWeaponsAndItems.Coin(enemyList[e].middlepos);
                    itemsList.Add(coin);
                    enemyList.RemoveAt(e);
                    e--;
                }
            }

            for (int b = 0; b < bossList.Count; b++)
            {
                if (bossList[b].alive)
                {
                    bossList[b].Update(gameTime);
                }

                player.InflictMeleeDamage(bossList[b]);
                if (bossList[b].health <= 0 && bossList[b].alive)
                {
                    bossList[b].alive = false;
                    RemoveRockTiles();
                    itemsList.Add(LoadWeaponsAndItems.HealPotion(bossList[b].middlepos, 50));
                }
            }

            foreach (Tile t in endTileList)
            {
                if (t.hitbox.Intersects(player.hitbox))
                {
                    if (isBossRoom)
                    {
                        LoadNewCircle();
                    }
                    else
                    {
                        LoadBossRoom();
                    }
                    break;
                }
            }

            shopKeeper.Update(gameTime);

            Game1.camera.SetPosition(new Vector2(player.hitbox.X + player.hitbox.Width / 2, player.hitbox.Y + player.hitbox.Height / 2));
            HUD.Update(player.middlepos);
        }

        /// <summary>
        /// Called to remove all the Rock Tiles;
        /// </summary>
        public static void RemoveRockTiles()
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
            DrawOnFrontRenderTarget();
        }

        public static void DrawOnFrontRenderTarget()
        {
            graphics.GraphicsDevice.SetRenderTarget(frontRenderTarget);
            graphics.GraphicsDevice.Clear(Color.Transparent);
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
            graphics.GraphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// Sets all of the enemies 'beenHit' to false;
        /// </summary>
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

        /// <summary>
        /// Creates an instance of a new vase.
        /// </summary>
        /// <param name="position">Middle position of the new vase.</param>
        public static void NewVase(Vector2 position)
        {
            Moveable_Object vase = new Moveable_Object(SpriteSheetManager.vase, 1, 1, 1, position, new Point(32, 32));
            vase.currentFrame.X = rnd.Next(vase.spriteSheet.sheetSize.X + 1);
            vaseList.Add(vase);
        }

        public static void DrawOnBackRenderTarget()
        {
            graphics.GraphicsDevice.SetRenderTarget(backRenderTarget);
            graphics.GraphicsDevice.Clear(Color.Transparent);
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
            graphics.GraphicsDevice.SetRenderTarget(null);
        }

        public static void Draw(SpriteBatch sb)
        {
            sb.Draw(frontRenderTarget, Vector2.Zero, Color.White);
            sb.Draw(backRenderTarget, Vector2.Zero, Color.White);
            sb.Draw(shopRenderTarget, new Vector2(-2000, -2000), Color.White);

            foreach (Room room in shopRoomArray)
            {
                room.Draw(sb);
            }

            foreach (Boss boss in bossList)
            {
                if (boss.alive)
                    boss.Draw(sb);
            }

            foreach (Projectile p in projectilesOnScreenList)
            {
                p.Draw(sb);
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

            foreach (Moveable_Object vase in vaseList)
            {
                vase.Draw(sb);
            }

            foreach (Enemy e in enemyList)
            {
                e.Draw(sb);
            }

            AmbientEffectManager.Draw(sb);

            HUD.Draw(sb);
        }
    }
}