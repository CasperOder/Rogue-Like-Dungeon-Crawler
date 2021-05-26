using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace RogueLike
{
    static class GameOver
    {
        static SpriteSheet background;
        static float gameOverFade;
        static double timeTillfade;
        static Player player;
        static Button exitButton;
        static MouseState mouseState, oldMouseState;

        /// <summary>
        /// Loads the Game Over screen.
        /// </summary>
        public static void LoadScreen()
        {
            player = Level.player;
            player.SetPlayerPosition(new Vector2(Constants.windowWidth / 2, Constants.windowHeight / 2));
            background = SpriteSheetManager.gameOver;
        }

        /// <summary>
        /// Updates the Game Over screen.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="graphics"></param>
        /// <param name="content"></param>
        public static void Update(GameTime gameTime, GraphicsDeviceManager graphics, ContentManager content)
        {
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            timeTillfade += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeTillfade >= 1 && gameOverFade < 1)
            {
                gameOverFade += 0.01f;
            }
            else if (gameOverFade >= 1)
            {
                exitButton = new Button(SpriteSheetManager.exitGame.texture, new Rectangle(Constants.windowWidth / 2 - 300, Constants.windowHeight * 4 / 5, 600, 105), "exitGame");
                exitButton.ButtonClicked(mouseState, oldMouseState, graphics, content);
            }
        }

        public static void Draw(SpriteBatch sb)
        {
            player.Draw(sb);

            sb.Draw(background.texture, Vector2.Zero, Color.White * gameOverFade);

            if (exitButton != null)
            {
                exitButton.Draw(sb);
            }
        }
    }
}