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
    public static class SpriteSheetManager
    {
        public static SpriteSheet ball { get; private set; }
        public static SpriteSheet player { get; private set; }
        public static SpriteSheet fire { get; private set; }
        public static SpriteSheet tempTile { get; private set; }
        public static SpriteSheet dummy;
        private static List<Point[]> playerAnimations = new List<Point[]>();
        private static List<Point[]> fireAnimations = new List<Point[]>();

        //Alla sprites hanteras med SpriteSheet objekt.
        //Alla SpriteSheet objekt sparas och hämtas från SpriteSheetManager
        //För att hämta ett Texture2D används "spriteSheetName.texture.
        //Använd gärna "spriteSheetName.frameSize" för spritesheets med flera frames då det drar ner på uträkningar.
        //SpriteSheet måste innehålla ett Texture2D och en Point som representerar hur många rader och kolumner med frames texturen har.
        //SpriteSheet kan innehålla en lista av animationssekvenser.
        //Deklarera nya spriteSheets i LoadContent funktionen.
        //GameObject använder nu SpriteSheet objekt istället för Texture2D

        public static void LoadContent(ContentManager c)
        {
            Point sheetSize;
            Texture2D texture;

            //Ball
            sheetSize = new Point(0, 0);
            texture = c.Load<Texture2D>("ball");

            //tillfälllig bakgrunds Tile
            texture = c.Load<Texture2D>("tempTile");
            tempTile = new SpriteSheet(texture, sheetSize);

            ball = new SpriteSheet(c.Load<Texture2D>("ball"), sheetSize);

            dummy = new SpriteSheet(c.Load<Texture2D>("dummy"), sheetSize);

            //Player
            sheetSize = new Point(3, 0);
            texture = c.Load<Texture2D>("IdleSpriteSheet");

            playerAnimations.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
            });

            player = new SpriteSheet(texture, sheetSize, playerAnimations);
            
            //Fire
            sheetSize = new Point(4, 0);
            texture = c.Load<Texture2D>("Fire");

            fireAnimations.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
                new Point(4, 0),
            });

            fire = new SpriteSheet(texture, sheetSize, playerAnimations);
        }

    }
}
