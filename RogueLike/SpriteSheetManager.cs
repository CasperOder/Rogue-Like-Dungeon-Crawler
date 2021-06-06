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
        public static SpriteSheet playerPlaceHolder { get; private set; }
        public static SpriteSheet tempTile { get; private set; }

        public static SpriteSheet fire { get; private set; }
        public static SpriteSheet book;
        public static SpriteSheet corpse;

        public static SpriteSheet enemyHit;

        public static SpriteSheet player;

        public static SpriteSheet dummy;
        public static SpriteSheet floorTile { get; private set; }

        public static SpriteSheet bossMinos { get; private set; }
        public static SpriteSheet minosArm { get; private set; }
        public static SpriteSheet minosGround { get; private set; }

        public static SpriteSheet arrow;
        public static SpriteSheet arrowItem;

        public static SpriteSheet devil;

        public static SpriteSheet coin;
        public static SpriteSheet sweep;
        public static SpriteSheet sweepItem;
        public static SpriteSheet stairTile;
        public static SpriteSheet rock;
        public static SpriteSheet knife;
        public static SpriteSheet knifeItem;
        public static SpriteSheet wallTiles, invisibleTile;
        public static SpriteSheet damageBoost, healthBoost, attackSpeedBoost, speedBoost;
        public static SpriteSheet spear, spearItem;
        public static SpriteSheet upLadder, downLadder;
        public static SpriteSheet punch, punchItem, smash, smashItem;
        public static SpriteSheet shopKeeper, shopKeeperTextbox;
        public static SpriteSheet bow, bowItem, arrowBow;
        public static SpriteSheet windBreeze, raindrops, fireDrops, strongWind, bubbles;
        public static SpriteSheet throwing, throwItem, shuriken;
        public static SpriteSheet swordSwing;
        public static SpriteSheet fireRod, fireRodItem, fireBall;
        public static SpriteSheet iceRod, iceRodItem, iceBall;
        public static SpriteSheet healPotion;

        public static SpriteSheet gameOver;
        public static SpriteSheet newGame, exitGame;

        public static SpriteSheet backGroundTex, door;
        public static SpriteSheet start, resume, quit, options, back, continue_, pause;
        public static SpriteSheet muteMusicOff, muteMusicOn;
        public static SpriteSheet fullScreenOn, fullScreenOff;
        public static SpriteSheet titleScreenSheet;
        public static SpriteSheet doorSheet;
        

        public static SpriteSheet vase, shatteredVase;

        private static List<Point[]> playerPlaceHolderAnimations = new List<Point[]>();
        private static List<Point[]> minosAnimations = new List<Point[]>();
        private static List<Point[]> minosGroundAnimation = new List<Point[]>();

        private static List<Point[]> devilAnimations = new List<Point[]>();

        private static List<Point[]> fireAnimations = new List<Point[]>();
        private static List<Point[]> bookAnimations = new List<Point[]>();
        private static List<Point[]> corpseAnimations = new List<Point[]>();

        private static List<Point[]> enemyHitAnimation = new List<Point[]>();

        private static List<Point[]> windBreezeAnimation = new List<Point[]>();
        private static List<Point[]> rainAnimation = new List<Point[]>();
        private static List<Point[]> fireDropsAnimation = new List<Point[]>();
        private static List<Point[]> shopKeeperAnimation = new List<Point[]>();
        private static List<Point[]> rodBallAnimation = new List<Point[]>();
        private static List<Point[]> strongWindAnimation = new List<Point[]>();
        private static List<Point[]> bubblesAnimation = new List<Point[]>();
        private static List<Point[]> shatterAnimation = new List<Point[]>();


        private static List<Point[]> swordSwingAnimation = new List<Point[]>();

        private static List<Point[]> playerAnimations = new List<Point[]>();

        

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
            downLadder = new SpriteSheet(c.Load<Texture2D>("ladderdown"), sheetSize);
            upLadder = new SpriteSheet(c.Load<Texture2D>("ladderup"), sheetSize);
            rock = new SpriteSheet(c.Load<Texture2D>("rock"), sheetSize);
            knife = new SpriteSheet(c.Load<Texture2D>("knife"), sheetSize);
            knifeItem= new SpriteSheet(c.Load<Texture2D>("knifeitem"), sheetSize);
            spear = new SpriteSheet(c.Load<Texture2D>("spear"), sheetSize);
            spearItem = new SpriteSheet(c.Load<Texture2D>("spearitem"), sheetSize);
            punch = new SpriteSheet(c.Load<Texture2D>("punch"), sheetSize);
            punchItem = new SpriteSheet(c.Load<Texture2D>("punchitem"), sheetSize);
            smash = new SpriteSheet(c.Load<Texture2D>("smash"), sheetSize);
            smashItem = new SpriteSheet(c.Load<Texture2D>("smashitem"), sheetSize);
            throwing = new SpriteSheet(c.Load<Texture2D>("throw"), sheetSize);
            throwItem = new SpriteSheet(c.Load<Texture2D>("throwitem"), sheetSize);
            shuriken = new SpriteSheet(c.Load<Texture2D>("shuriken"), sheetSize);
            fireRod = new SpriteSheet(c.Load<Texture2D>("firerod"), sheetSize);
            fireRodItem = new SpriteSheet(c.Load<Texture2D>("fireroditem"), sheetSize);
            iceRod = new SpriteSheet(c.Load<Texture2D>("icerod"), sheetSize);
            iceRodItem = new SpriteSheet(c.Load<Texture2D>("iceroditem"), sheetSize);
            invisibleTile = new SpriteSheet(c.Load<Texture2D>("invisibletile"), sheetSize);
            healPotion = new SpriteSheet(c.Load<Texture2D>("healpotion"), sheetSize);

            damageBoost = new SpriteSheet(c.Load<Texture2D>("damageboost"), sheetSize);
            healthBoost = new SpriteSheet(c.Load<Texture2D>("healthboost"), sheetSize);
            attackSpeedBoost = new SpriteSheet(c.Load<Texture2D>("attackspeedboost"), sheetSize);
            speedBoost = new SpriteSheet(c.Load<Texture2D>("speedboost"), sheetSize);

            shopKeeperTextbox = new SpriteSheet(c.Load<Texture2D>("shopkeepertext"), sheetSize);

            bow = new SpriteSheet(c.Load<Texture2D>("bow"), sheetSize);
            bowItem = new SpriteSheet(c.Load<Texture2D>("bowitem"), sheetSize);
            arrowBow = new SpriteSheet(c.Load<Texture2D>("arrowbow"), sheetSize);

            gameOver = new SpriteSheet(c.Load<Texture2D>("gameover"), sheetSize);

            newGame = new SpriteSheet(c.Load<Texture2D>("NewGame"), sheetSize);
            exitGame = new SpriteSheet(c.Load<Texture2D>("exit"), sheetSize);

            backGroundTex = new SpriteSheet(c.Load<Texture2D>("background"), sheetSize);
            start = new SpriteSheet(c.Load<Texture2D>("start"), sheetSize);
            door = new SpriteSheet(c.Load<Texture2D>("Door"), sheetSize);
            resume = new SpriteSheet(c.Load<Texture2D>("ResumeButton"), sheetSize);
            quit = new SpriteSheet(c.Load<Texture2D>("QuitButton"), sheetSize);
            options = new SpriteSheet(c.Load<Texture2D>("OptionsButton"), sheetSize);
            back = new SpriteSheet(c.Load<Texture2D>("BackButton"), sheetSize);
            muteMusicOff = new SpriteSheet(c.Load<Texture2D>("MuteMusicOff"), sheetSize);
            muteMusicOn = new SpriteSheet(c.Load<Texture2D>("MuteMusicOn"), sheetSize);
            fullScreenOff = new SpriteSheet(c.Load<Texture2D>("FullScreenOff"), sheetSize);
            fullScreenOn = new SpriteSheet(c.Load<Texture2D>("FullScreenOn"), sheetSize);
            continue_ = new SpriteSheet(c.Load<Texture2D>("Continue"), sheetSize);
            pause = new SpriteSheet(c.Load<Texture2D>("PauseButton"), sheetSize);
            titleScreenSheet = new SpriteSheet(c.Load<Texture2D>("TitleScreenSheet"), sheetSize);
            doorSheet = new SpriteSheet(c.Load<Texture2D>("door-Sheet"), sheetSize);



            //Minos arm
            texture = c.Load<Texture2D>("Minos_arm");

            minosArm = new SpriteSheet(texture, sheetSize);


            //Floor tile
            sheetSize = new Point(5, 8);
            texture = c.Load<Texture2D>("Floor_Tile");

            floorTile = new SpriteSheet(texture, sheetSize);


            //Player
            sheetSize = new Point(3, 0);
            texture = c.Load<Texture2D>("IdleSpriteSheet");

            

            playerPlaceHolderAnimations.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
            });

            playerPlaceHolder = new SpriteSheet(texture, sheetSize, playerPlaceHolderAnimations);

            sheetSize = new Point(3, 8); 
            wallTiles = new SpriteSheet(c.Load<Texture2D>("walltiles"), sheetSize);


            //Shop Keeper
            sheetSize = new Point(1, 0);
            shopKeeperAnimation.Add(new Point[]
            {
                new Point(0,0),
                new Point(1,0)
            });
            shopKeeper = new SpriteSheet(c.Load<Texture2D>("shopkeeper"), sheetSize, shopKeeperAnimation);

            rodBallAnimation.Add(new Point[]
            {
                new Point(0,0),
                new Point(1,0),
            });
            fireBall = new SpriteSheet(c.Load<Texture2D>("fireball"), sheetSize, rodBallAnimation);
            iceBall = new SpriteSheet(c.Load<Texture2D>("iceball"), sheetSize, rodBallAnimation);

            

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

            fire = new SpriteSheet(texture, sheetSize, fireAnimations);

            //Corpse
            sheetSize = new Point(3, 0);
            texture = c.Load<Texture2D>("Corpse");

            corpseAnimations.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
            });

            corpse = new SpriteSheet(texture, sheetSize, corpseAnimations);

            //Book
            sheetSize = new Point(3, 0);
            texture = c.Load<Texture2D>("booksheet");

            bookAnimations.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
            });

            book = new SpriteSheet(texture, sheetSize, bookAnimations);

            //Enemy hit
            sheetSize = new Point(3, 0);
            texture = c.Load<Texture2D>("enemy-hit");

            enemyHitAnimation.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
            });

            enemyHit = new SpriteSheet(texture, sheetSize, enemyHitAnimation);

            shatterAnimation.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
                new Point(4, 0),
            });
            shatteredVase = new SpriteSheet(c.Load<Texture2D>("shattervase"), sheetSize, shatterAnimation);

            //Minos
            sheetSize = new Point(5, 0);
            texture = c.Load<Texture2D>("Minos-Sheet");

            minosAnimations.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
                new Point(4, 0),
            });

            bossMinos = new SpriteSheet(texture, sheetSize, minosAnimations);

            //Minos ground
            sheetSize = new Point(2, 0);
            texture = c.Load<Texture2D>("Minos-ground");


            minosGroundAnimation.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
            });

            minosGround = new SpriteSheet(texture, sheetSize, minosGroundAnimation);

            vase = new SpriteSheet(c.Load<Texture2D>("vase"), sheetSize);


            sheetSize = new Point(6, 0);
            windBreezeAnimation.Add(new Point[]
            {
                new Point(0,0),
                new Point(1,0),
                new Point(2,0),
                new Point(3,0),
                new Point(4,0),
                new Point(5,0),
                new Point(6,0),
            });

            windBreeze = new SpriteSheet(c.Load<Texture2D>("windbreeze"), sheetSize, windBreezeAnimation);

            sheetSize = new Point(8, 0);
            strongWindAnimation.Add(new Point[]
            {
                new Point(0,0),
                new Point(1,0),
                new Point(2,0),
                new Point(3,0),
                new Point(4,0),
                new Point(5,0),
                new Point(6,0),
                new Point(7,0),
                new Point(8,0),

            });
            strongWind = new SpriteSheet(c.Load<Texture2D>("strongwind"), sheetSize, strongWindAnimation);

            sheetSize = new Point(11, 0);
            fireDropsAnimation.Add(new Point[]
            {
                new Point(0,0),
                new Point(1,0),
                new Point(2,0),
                new Point(3,0),
                new Point(4,0),
                new Point(5,0),
                new Point(7,0),
                new Point(8,0),
                new Point(9,0),
                new Point(10,0),
                new Point(11,0),
            });

            fireDrops = new SpriteSheet(c.Load<Texture2D>("firedrops"), sheetSize, fireDropsAnimation);

            sheetSize = new Point(6, 0);
            
            rainAnimation.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(3, 0),
                new Point(4, 0),
                new Point(5, 0),
                new Point(6, 0),
            });
            raindrops = new SpriteSheet(c.Load<Texture2D>("rainsheet"), sheetSize, rainAnimation);

            //Sword sweep

            sheetSize = new Point(2, 0);

            swordSwingAnimation.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
            });

            swordSwing = new SpriteSheet(c.Load<Texture2D>("sword_swing"), sheetSize, swordSwingAnimation);

            //Player
            sheetSize = new Point(5, 3);

            playerAnimations.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
                new Point(4, 0),
                new Point(5, 0),

            });
            playerAnimations.Add(new Point[]
{
                new Point(0, 1),
                new Point(1, 1),
                new Point(2, 1),
                new Point(3, 1),
                new Point(4, 1),
                new Point(5, 1),

            });
            playerAnimations.Add(new Point[]
{
                new Point(0, 2),
                new Point(1, 2),
                new Point(2, 2),
                new Point(3, 2),
                new Point(4, 2),
                new Point(5, 2),

            });
            playerAnimations.Add(new Point[]
{
                new Point(0, 3),
                new Point(1, 3),
                new Point(2, 3),
                new Point(3, 3),
                new Point(4, 3),
                new Point(5, 3),

            });

            player = new SpriteSheet(c.Load<Texture2D>("Run-Sheet"), sheetSize, playerAnimations);

          sheetSize = new Point(5, 0);
            bubblesAnimation.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(3, 0),
                new Point(4, 0),
                new Point(5, 0),
            });
            bubbles = new SpriteSheet(c.Load<Texture2D>("bubbles"), sheetSize, bubblesAnimation);

            //Devil

            sheetSize = new Point(3, 0);
            devilAnimations.Add(new Point[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(2, 0),
                new Point(3, 0),
            });
            devil = new SpriteSheet(c.Load<Texture2D>("Devil"), sheetSize, devilAnimations);

        }

    }
}
