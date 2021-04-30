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
        public static SpriteSheet arrow;
        public static SpriteSheet arrowItem;
        public static SpriteSheet coin;
        public static SpriteSheet sweep;
        public static SpriteSheet sweepItem;
        public static SpriteSheet stairTile;
        public static SpriteSheet rock;
        public static SpriteSheet knife;
        public static SpriteSheet knifeItem;

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

            //tillfälliga från Davids testning
            dummy = new SpriteSheet(c.Load<Texture2D>("dummy"), sheetSize);
            arrow = new SpriteSheet(c.Load<Texture2D>("Arrow"), sheetSize);
            arrowItem = new SpriteSheet(c.Load<Texture2D>("arrowitem"), sheetSize);
            coin = new SpriteSheet(c.Load<Texture2D>("coinitem"), sheetSize);
            sweep = new SpriteSheet(c.Load<Texture2D>("sweep"), sheetSize);
            sweepItem = new SpriteSheet(c.Load<Texture2D>("sweepitem"), sheetSize);
            stairTile = new SpriteSheet(c.Load<Texture2D>("stairtile"), sheetSize);
            rock = new SpriteSheet(c.Load<Texture2D>("rock"), sheetSize);
            knife = new SpriteSheet(c.Load<Texture2D>("knife"), sheetSize);
            knifeItem = new SpriteSheet(c.Load<Texture2D>("knifeitem"), sheetSize);

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
