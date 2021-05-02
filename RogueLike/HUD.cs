using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RogueLike
{
    static class HUD 
    {
        static int[] digitArray = new int[4];
        static int[] currentHealthArray = new int[3];
        static int[] maxHealthArray = new int[3];

        static Rectangle numberBox = new Rectangle(0, 0, 20, 28);

        static Rectangle healthBox = new Rectangle(0, 0, 40, 56);
        static Rectangle healthSrc;

        static Vector2 position;

        static Texture2D digitTex;

        public static void Load(ContentManager c)
        {
            digitTex = c.Load<Texture2D>("digits");
        }
       
        public static void Update(Vector2 playerPos)
        {
            position = new Vector2(playerPos.X - Constants.windowWidth / 2, playerPos.Y - Constants.windowHeight / 2);


        }

        public static void UpdateCurrencyHUD(int c) //kallas när valutan ändras
        {
            digitArray[0] = c / 1000;
            digitArray[1] = c / 100 - (c / 1000) * 10;
            digitArray[2] = c / 10 - (c / 100) * 10;
            digitArray[3] = c - (c / 10) * 10;
        }

        public static void UpdateCurrentHealthHUD(int h) //kallas när spelaren tar skada eller nuvarande hälsa ändras generellt
        {
            currentHealthArray[0] = h / 100;
            currentHealthArray[1] = h / 10 - (h / 100) * 10;
            currentHealthArray[2] = h - (h / 10) * 10;
        }

        public static void UpdateMaxHealthHUD(int h) //kallas när spelarens maximala hälsa ändras
        {
            maxHealthArray[0] = h / 100;
            maxHealthArray[1] = h / 10 - (h / 100) * 10;
            maxHealthArray[2] = h - (h / 10) * 10;
        }

        public static void Draw(SpriteBatch sb) //rendertarget blir nog optimalt här. Får jobba på det en annan dag. /D
        {
            for (int i = 0; i < digitArray.Length; i++)
            {
                Rectangle srcRec = new Rectangle(digitArray[i] * digitTex.Width / 10, 0, digitTex.Width / 10, digitTex.Height / 2);
                numberBox = new Rectangle((int)position.X + i * numberBox.Width, (int)position.Y, numberBox.Width, numberBox.Height);

                sb.Draw(digitTex, numberBox, srcRec, Color.White);
            }

            for (int i = 0; i < currentHealthArray.Length;i++)
            {
                healthSrc = new Rectangle(currentHealthArray[i] * digitTex.Width / 10, 0, digitTex.Width / 10, digitTex.Height / 2);
                healthBox = new Rectangle((int)position.X + i * healthBox.Width, (int)position.Y + Constants.windowHeight - healthBox.Height, healthBox.Width, healthBox.Height);

                sb.Draw(digitTex, healthBox, healthSrc, Color.White);
            }

            for (int i = 0; i < maxHealthArray.Length; i++)
            {
                healthSrc = new Rectangle(maxHealthArray[i] * digitTex.Width / 10, 0, digitTex.Width / 10, digitTex.Height / 2);
                healthBox = new Rectangle((int)position.X + (i + 4) * healthBox.Width, (int)position.Y + Constants.windowHeight - healthBox.Height, healthBox.Width, healthBox.Height);

                sb.Draw(digitTex, healthBox, healthSrc, Color.White);
            }

            healthSrc = new Rectangle(0, digitTex.Height / 2, digitTex.Width / 10, digitTex.Height / 2);
            healthBox = new Rectangle((int)position.X + 3 * healthBox.Width, (int)position.Y + Constants.windowHeight - healthBox.Height, healthBox.Width, healthBox.Height);

            sb.Draw(digitTex, healthBox, healthSrc, Color.White);
        }
            


    }
}
