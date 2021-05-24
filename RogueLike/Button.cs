using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public class Button
    {
        public string buttonName;
        public Rectangle pos;
        public Texture2D tex;
        public static bool isFullScreen = false;
        public static bool willQuit = false;


        public Button(Texture2D tex, Rectangle pos, string buttonName)
        {
            this.tex = tex;
            this.pos = pos;
            this.buttonName = buttonName;
        }

        public void ButtonClicked(MouseState mouseState, MouseState oldMouseState, GraphicsDeviceManager graphics, ContentManager content)
        {
            if (mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                if (pos.Contains(new Point(mouseState.X, mouseState.Y)))
                {
                    //Start
                    if (buttonName == "start" && Menu.fadeOut == false)
                    {
                        Menu.stateName = "play";
                        if (Menu.f == 255)
                        {
                            Menu.fadeOut = true;
                            Menu.fadeIn = false;
                        }
                        
                    }
                    else if (buttonName == "options" && Menu.fadeOut == false)
                    {
                        Menu.stateName = "options";
                        if (Menu.f == 255)
                        {
                            Menu.fadeOut = true;
                            Menu.fadeIn = false;
                        }
                    }
                    else if (buttonName == "quit")
                    {
                        willQuit = true;
                    }
                    //Options
                    else if (buttonName == "back")
                    {
                        Menu.stateName = "main";
                        if (Menu.f == 255)
                        {
                            Menu.fadeOut = true;
                            Menu.fadeIn = false;
                        }
                    }
                    else if (buttonName == "muteMusic")
                    {
                        if (MediaPlayer.IsMuted == true)
                        {
                            Menu.buttons[1].tex = Menu.muteMusicOff;
                            MediaPlayer.IsMuted = false;
                        }
                        else if (MediaPlayer.IsMuted == false)
                        {
                            Menu.buttons[1].tex = Menu.muteMusicOn;
                            MediaPlayer.IsMuted = true;
                        }
                    }
                    else if (buttonName == "fullScreen")
                    {
                        if (isFullScreen == true)
                        {
                            Menu.buttons[2].tex = Menu.fullScreenOff;
                            isFullScreen = false;
                        }
                        else if (isFullScreen == false)
                        {
                            Menu.buttons[2].tex = Menu.fullScreenOn;
                            isFullScreen = true;
                        }
                    }
                    //Play
                    else if (buttonName == "newGame")
                    {
                        SavefileHandler.DeleteSavefile();

                        Game1.gameState = Game1.GameState.Play;
                    }
                    //Continue on saved file
                    else if (buttonName=="continue")
                    {
                        if (Game1.saveFileExist)
                        {
                            Level.LoadFromSave();
                            Game1.gameState = Game1.GameState.Play;
                        }
                    }
                    else if (buttonName == "exitGame")
                    {
                        Level.Load_Level(graphics, content);
                        Game1.gameState = Game1.GameState.Start;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Menu.currentFade);
        }
    }
}
