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

        /// <summary>
        /// Loads the HUD.
        /// </summary>
        /// <param name="content"></param>
        public static void Load(ContentManager content)
        {
            digitTex = content.Load<Texture2D>("digits");
        }
       
        public static void Update(Vector2 playerPos)
        {
            position = new Vector2(playerPos.X - Constants.windowWidth / 2, playerPos.Y - Constants.windowHeight / 2);
        }

        /// <summary>
        /// Updates the currency on the HUD.
        /// </summary>
        /// <param name="currency">New currency.</param>
        public static void UpdateCurrencyHUD(int currency) //kallas när valutan ändras
        {
            digitArray[0] = currency / 1000;
            digitArray[1] = currency / 100 - (currency / 1000) * 10;
            digitArray[2] = currency / 10 - (currency / 100) * 10;
            digitArray[3] = currency - (currency / 10) * 10;
        }

        /// <summary>
        /// Updates the health on the HUD.
        /// </summary>
        /// <param name="health">New health.</param>
        public static void UpdateCurrentHealthHUD(int health) //kallas när spelaren tar skada eller nuvarande hälsa ändras generellt
        {
            currentHealthArray[0] = health / 100;
            currentHealthArray[1] = health / 10 - (health / 100) * 10;
            currentHealthArray[2] = health - (health / 10) * 10;
        }

        /// <summary>
        /// Updates the maxHealth on the HUD.
        /// </summary>
        /// <param name="maxHealth">New maxHealth.</param>
        public static void UpdateMaxHealthHUD(int maxHealth)
        {
            maxHealthArray[0] = maxHealth / 100;
            maxHealthArray[1] = maxHealth / 10 - (maxHealth / 100) * 10;
            maxHealthArray[2] = maxHealth - (maxHealth / 10) * 10;
        }

        public static void Draw(SpriteBatch sb) 
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