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
            playerStartPos = new Vector2(100, 100);

            player = new Player(SpriteSheetManager.player, playerStartPos, 0.1d);
            
            testRoom = new Room(new Vector2(600, 400), "smallRoom.txt", SpriteSheetManager.ball);
            testRoom.leftConnection = true;
            testRoom.CreateLevel();

        }


        public static void Update(GameTime gameTime)
        {
            player.Movement(gameTime);
            Game1.camera.SetPosition(new Vector2(player.hitbox.X + player.hitbox.Width / 2, player.hitbox.Y + player.hitbox.Height / 2));

        }

        public static void Draw(SpriteBatch sb)
        {
            player.Draw(sb);

            testRoom.Draw(sb);
        }

    }
}
