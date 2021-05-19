using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public Button(Texture2D tex, Rectangle pos, string buttonName)
        {
            this.tex = tex;
            this.pos = pos;
            this.buttonName = buttonName;
        }

        public void ButtonClicked(MouseState mouseState, MouseState oldMouseState)
        {
            if (mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                if (pos.Contains(new Point(mouseState.X, mouseState.Y)))
                {
                    if (buttonName == "startButton")
                    {
                        if (Menu.f == 100)
                        {
                            Menu.fadeOut = true;
                            Menu.fadeIn = false;
                        }
                        
                    }
                    else if (buttonName == "playButton")
                    {
                        Game1.gameState = Game1.GameState.Play;
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
