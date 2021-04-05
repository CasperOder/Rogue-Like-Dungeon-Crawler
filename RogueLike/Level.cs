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
        public static int tileSize = 50;
        public static Tile[,] foregroundTiles;
        public static Tile[,] backgroundTiles;
        public static RenderTarget2D backRenderTarget;
        public static RenderTarget2D frontRenderTarget;

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
                        backgroundTiles[b, a] = new Tile(SpriteSheetManager.ball, new Rectangle(tileSize * b, tileSize * a, tileSize, tileSize));
                        
                    }
                }
            }
        }

        public static void Load_Level()
        {
            playerStartPos = new Vector2(100, 100);

            player = new Player(SpriteSheetManager.player, playerStartPos, 0.1d);

            StreamReader sr = new StreamReader("Level1.txt");
            List<string> levelReader = new List<string>();
            while (!sr.EndOfStream)
            {
                levelReader.Add(sr.ReadLine());
            }
            sr.Close();

            foregroundTiles = new Tile[levelReader[0].Length, levelReader.Count];

            for (int a = 0; a < levelReader.Count; a++)
            {
                for (int b = 0; b < levelReader[a].Length; b++)
                {
                    if (levelReader[a][b] == '-')
                    {
                        foregroundTiles[b, a] = new Tile(SpriteSheetManager.ball, new Rectangle(tileSize * b, tileSize * a, tileSize, tileSize));

                    }
                    else if (levelReader[a][b] == 'w')
                    {
                        foregroundTiles[b, a] = new Tile(SpriteSheetManager.ball, new Rectangle(tileSize * b, tileSize * a, tileSize, tileSize));
                    }
                }
            }
        }


        public static void Update(GameTime gameTime)
        {
            player.Movement(gameTime);
        }

        public static void Draw(SpriteBatch sb)
        {
            player.Draw(sb);
        }

    }
}
