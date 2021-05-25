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
        static bool debug = true;

        public static List<Button> buttons = new List<Button>();

        //tillfälliga
        public static Texture2D backGroundTex;
        static Texture2D start;
        public static Texture2D door;
        static Texture2D resume;
        static Texture2D quit;
        static Texture2D options;
        public static Texture2D back;
        public static Texture2D muteMusicOff;
        public static Texture2D muteMusicOn;
        public static Texture2D fullScreenOff;
        public static Texture2D fullScreenOn;
        public static Texture2D continue_;
        public static Texture2D newGame;
        public static Texture2D pause;


        public static Texture2D swap;
        public static Color currentFade;
        public static bool fadeIn;
        public static bool fadeOut;
        public static int f = 255;
        static MouseState mouseState, oldMouseState;
        static KeyboardState keyboardState, oldKeyboardState;
        public static string stateName;
        static Vector2 position;

        public static void Load(ContentManager content) //tillfälligt tills jag orkar fixa med animationer
        {
            backGroundTex = content.Load<Texture2D>("background");
            start = content.Load<Texture2D>("start");
            door = content.Load<Texture2D>("Door");
            resume = content.Load<Texture2D>("ResumeButton");
            quit = content.Load<Texture2D>("QuitButton");
            options = content.Load<Texture2D>("OptionsButton");
            back = content.Load<Texture2D>("BackButton");
            muteMusicOff = content.Load<Texture2D>("MuteMusicOff");
            muteMusicOn = content.Load<Texture2D>("MuteMusicOn");
            fullScreenOff = content.Load<Texture2D>("FullScreenOff");
            fullScreenOn = content.Load<Texture2D>("FullScreenOn");
            continue_ = content.Load<Texture2D>("Continue");
            newGame = content.Load<Texture2D>("NewGame");
            pause = content.Load<Texture2D>("PauseButton");
            swap = backGroundTex;

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
                            buttons.Add(new Button(start, new Rectangle(625, 600, 600, 140), "start"));
                            buttons.Add(new Button(options, new Rectangle(727, 760, 395, 70), "options"));
                            buttons.Add(new Button(quit, new Rectangle(782, 850, 235, 85), "quit"));
                            break;
                        case MenuState.MainMenuPlay:
                            buttons.Add(new Button(back, new Rectangle(795, 760, 245, 70), "back"));
                            buttons.Add(new Button(continue_, new Rectangle(540, 300, 720, 105), "continue"));
                            buttons.Add(new Button(newGame, new Rectangle(540, 500, 720, 105), "newGame"));
                            break;
                        case MenuState.MainMenuOption:
                            buttons.Add(new Button(back, new Rectangle(795, 760, 245, 70), "back"));
                            buttons.Add(new Button(muteMusicOff, new Rectangle(200, 300, 282, 28), "muteMusic"));
                            buttons.Add(new Button(fullScreenOff, new Rectangle(200, 350, 282, 28), "fullScreen"));
                            break;
                    }
                    break;

                case Game1.GameState.Pause:
                    switch (menuState)
                    {
                        case MenuState.PauseMenuHome:
                            buttons.Add(new Button(resume, new Rectangle(742, 300, 365, 70), "resume"));
                            buttons.Add(new Button(options, new Rectangle(727, + 400, 395, 70), "pauseOptions"));
                            buttons.Add(new Button(quit, new Rectangle(782, 500, 235, 85), "back"));
                            break;
                        case MenuState.PauseMenuOption:
                            buttons.Add(new Button(back, new Rectangle(795, 760, 245, 70), "back"));
                            buttons.Add(new Button(continue_, new Rectangle(540, 300, 720, 105), "continue"));
                            buttons.Add(new Button(newGame, new Rectangle(540, 500, 720, 105), "newGame"));
                            break;
                    }
                    

                    break;

                case Game1.GameState.GameOver:
                    break;
            }
        }

        public static void Update(GraphicsDeviceManager graphics, ContentManager content, Vector2 playerPos)
        {
            position = new Vector2(playerPos.X - Constants.windowWidth / 2, playerPos.Y - Constants.windowHeight / 2);
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
                    swap = door;
                }
                else if (stateName == "main")
                {
                    menuState = MenuState.MainMenuHome;
                    LoadButtons();
                    swap = backGroundTex;
                }
                else if (stateName == "options")
                {
                    menuState = MenuState.MainMenuOption;
                    LoadButtons();
                    swap = door;
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
            if (Game1.gameState == Game1.GameState.Start || Game1.gameState == Game1.GameState.Pause)
            {
                spriteBatch.Draw(swap, new Rectangle(0, 0, 1850, 1000), currentFade);
            }
            
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(spriteBatch);
            }
        }
    }
}