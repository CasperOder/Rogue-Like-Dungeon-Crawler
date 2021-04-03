﻿using System;
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

        private static List<Point[]> playerAnimations = new List<Point[]>();
        private static List<Point[]> fireAnimations = new List<Point[]>();

        public static void LoadContent(ContentManager c)
        {
            Point sheetSize;
            Texture2D texture;
            //Ball
            sheetSize = new Point(0, 0);
            texture = c.Load<Texture2D>("ball");

            ball = new SpriteSheet(texture, sheetSize);

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
