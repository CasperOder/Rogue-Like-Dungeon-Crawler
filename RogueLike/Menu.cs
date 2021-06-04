using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public class Menu
    {
        public enum MenuState
        {
            MainMenuHome,
            MainMenuPlay,
            MainMenuOption,
            PauseMenuHome,
            PauseMenuOption,
        }

        public static MenuState menuState = MenuState.MainMenuHome;
        //Skippa fade vid debug
        static bool debug = false;

        //public static MenuState menuState = MenuState.MainMenuScreen;

        public static List<Button> buttons = new List<Button>();

        public static Texture2D swap;
        public static Color currentFade;
        public static bool fadeIn;
        public static bool fadeOut;
        public static int f = 255;
        static MouseState mouseState, oldMouseState;
        public static string stateName;

        private static double timeSinceLastFrame = 0;
        private static double timeBetweenFrames = 0.2;
        private static int currentFrame = 0;
        private static int totalFrames = 7;
        private static Rectangle rect;


        public static void Load(ContentManager content)
        {
            swap = SpriteSheetManager.backGroundTex.texture;

            if (debug)
            {
                menuState = MenuState.MainMenuPlay;
                Console.WriteLine("menu in debug mode");
            }
        }

        public static void LoadButtons()
        {
            for (int i = 0; i < buttons.Count;)
            {
                buttons.Remove(buttons[0]);
            }

            switch (Game1.gameState)
            {
                case Game1.GameState.Start:

                    switch (menuState)
                    {
                        case MenuState.MainMenuHome:
                            buttons.Add(new Button(SpriteSheetManager.start.texture, new Rectangle(625, 600, 600, 140), "start"));
                            buttons.Add(new Button(SpriteSheetManager.options.texture, new Rectangle(727, 760, 395, 70), "options"));
                            buttons.Add(new Button(SpriteSheetManager.quit.texture, new Rectangle(782, 850, 235, 85), "quit"));
                            break;
                        case MenuState.MainMenuPlay:
                            buttons.Add(new Button(SpriteSheetManager.back.texture, new Rectangle(795, 760, 245, 70), "back"));
                            buttons.Add(new Button(SpriteSheetManager.continue_.texture, new Rectangle(540, 300, 720, 105), "continue"));
                            buttons.Add(new Button(SpriteSheetManager.newGame.texture, new Rectangle(540, 500, 720, 105), "newGame"));
                            break;
                        case MenuState.MainMenuOption:
                            buttons.Add(new Button(SpriteSheetManager.back.texture, new Rectangle(795, 760, 245, 70), "back"));
                            buttons.Add(new Button(SpriteSheetManager.muteMusicOff.texture, new Rectangle(200, 300, 282, 28), "muteMusic"));
                            buttons.Add(new Button(SpriteSheetManager.fullScreenOff.texture, new Rectangle(200, 350, 282, 28), "fullScreen"));
                            break;
                    }
                    break;

                case Game1.GameState.Pause:
                    switch (menuState)
                    {
                        case MenuState.PauseMenuHome:
                            buttons.Add(new Button(SpriteSheetManager.resume.texture, new Rectangle(742, 300, 365, 70), "resume"));
                            buttons.Add(new Button(SpriteSheetManager.options.texture, new Rectangle(727, + 400, 395, 70), "pauseOptions"));
                            buttons.Add(new Button(SpriteSheetManager.quit.texture, new Rectangle(782, 500, 235, 85), "pauseQuit"));
                            break;
                        case MenuState.PauseMenuOption:
                            buttons.Add(new Button(SpriteSheetManager.back.texture, new Rectangle(795, 760, 245, 70), "pauseBack"));
                            buttons.Add(new Button(SpriteSheetManager.muteMusicOff.texture, new Rectangle(200, 300, 282, 28), "muteMusic"));
                            buttons.Add(new Button(SpriteSheetManager.fullScreenOff.texture, new Rectangle(200, 350, 282, 28), "fullScreen"));
                            break;
                    }
                    

                    break;

                case Game1.GameState.GameOver:
                    break;

                case Game1.GameState.Play:
                    break;
            }
        }

        public static void UpdateFrame(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

            if (timeSinceLastFrame >= timeBetweenFrames)
            {
                timeSinceLastFrame = 0;
                currentFrame++;

                if (currentFrame >= totalFrames)
                {
                    currentFrame = 0;
                }
            }

            rect = new Rectangle(currentFrame * SpriteSheetManager.titleScreenSheet.texture.Width / totalFrames, 0, SpriteSheetManager.titleScreenSheet.texture.Width / totalFrames, SpriteSheetManager.titleScreenSheet.texture.Height);
        }

        public static void Update(GraphicsDeviceManager graphics, ContentManager content, GameTime gameTime)
        {
            UpdateFrame(gameTime);

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].ButtonClicked(mouseState, oldMouseState, graphics, content);
            }

            if (fadeOut && f > 0)
            {
                f = f - 5;
            }
            else if (fadeIn && f < 255)
            {
                f = f + 5;
            }
            else if (fadeOut && f == 0)
            {
                fadeIn = true;
                fadeOut = false;
                if (stateName == "play")
                {
                    menuState = MenuState.MainMenuPlay;
                    LoadButtons();
                    swap = SpriteSheetManager.door.texture;
                }
                else if (stateName == "main")
                {
                    menuState = MenuState.MainMenuHome;
                    LoadButtons();
                    swap = SpriteSheetManager.backGroundTex.texture;
                }
                else if (stateName == "options")
                {
                    menuState = MenuState.MainMenuOption;
                    LoadButtons();
                    swap = SpriteSheetManager.door.texture;
                }
                else if (stateName == "pauseOptions")
                {
                    menuState = MenuState.PauseMenuOption;
                    LoadButtons();
                }
                else if (stateName == "pauseMain")
                {
                    menuState = MenuState.PauseMenuHome;
                    LoadButtons();
                }
            }

            if (Game1.gameState == Game1.GameState.Play && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Game1.gameState = Game1.GameState.Pause;
                menuState = MenuState.PauseMenuHome;
                LoadButtons();
            }
            currentFade = new Color(f, f, f);
            
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (Game1.gameState == Game1.GameState.Start && menuState == MenuState.MainMenuPlay || Game1.gameState == Game1.GameState.Pause)
            {
                spriteBatch.Draw(swap, new Rectangle(0, 0, 1850, 1000), currentFade);
            }

            if (Game1.gameState == Game1.GameState.Start && menuState == MenuState.MainMenuHome)
            {
                spriteBatch.Draw(SpriteSheetManager.titleScreenSheet.texture, new Vector2(0, 0), rect, currentFade);
            }
            

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(spriteBatch);
            }
        }
    }
}