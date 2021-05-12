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
            MainMenuScreen,
            PlayScreen
        }

        public static MenuState menuState = MenuState.MainMenuScreen;

        public static List<Button> buttons = new List<Button>();

        //tillfälliga
        static Texture2D backGroundTex;
        static Texture2D start;
        static Texture2D door;


        static Texture2D swap;
        static Color currentFade;
        public static bool fadeIn;
        public static bool fadeOut;
        public static int f = 250;
        static MouseState mouseState, oldMouseState;

        public static void Load(ContentManager content) //tillfälligt tills jag orkar fixa med animationer
        {
            backGroundTex = content.Load<Texture2D>("background");
            start = content.Load<Texture2D>("start");
            door = content.Load<Texture2D>("Door");
            swap = backGroundTex;
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
                        case MenuState.MainMenuScreen:
                            buttons.Add(new Button(start, new Rectangle(100, 100, 100, 100), "startButton"));
                            break;
                        case MenuState.PlayScreen:
                            buttons.Add(new Button(start, new Rectangle(300, 300, 100, 100), "playButton"));
                            break;
                    }

                    break;

                case Game1.GameState.Play:
                    break;

                case Game1.GameState.GameOver:
                    break;
            }
        }

        public static void Update()
        {
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].ButtonClicked(mouseState, oldMouseState);
            }

            if (fadeOut && f > 0)
            {
                f--;
            }
            else if (fadeIn && f < 250)
            {
                f++;
            }
            else if (fadeOut && f == 0)
            {
                swap = door;
                fadeIn = true;
                fadeOut = false;
                menuState = MenuState.PlayScreen;
            }

            currentFade = new Color(f, f, f);
            
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(swap, new Rectangle(0, 0, 1850, 1000), currentFade);

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(spriteBatch);
            }
        }
    }
}
