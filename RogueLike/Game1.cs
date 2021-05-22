using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RogueLike
{
    public class Game1 : Game
    {
        public enum GameState
        {
            Start,
            Play,
            GameOver
        }

        public static bool saveFileExist;
        public static GameState gameState;
        public static Camera camera;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                GraphicsProfile = GraphicsProfile.HiDef
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            gameState = GameState.Start;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Constants.LoadConstants();
            graphics.PreferredBackBufferWidth = Constants.windowWidth;
            graphics.PreferredBackBufferHeight = Constants.windowHeight;
            graphics.ApplyChanges();

            //kub = Content.Load<Texture2D>(@"kub");

            SpriteSheetManager.LoadContent(Content);
            SoundManager.LoadAudio(Content);
            LoadWeaponsAndItems.LoadAllWeaponsAndItems();
            EnemyManager.LoadEnemies();
            HUD.Load(Content);
            Menu.Load(Content);
            try
            {
                SavefileHandler.ReadFile("savefile.txt");
                saveFileExist = true;
            }
            catch
            {
                saveFileExist = false;
            }
            Menu.LoadButtons();

            camera = new Camera(GraphicsDevice.Viewport);
            Level.Load_Level(graphics, Content);

            MediaPlayer.IsRepeating = true;
            

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Start:
                    Menu.Update();

                    if (Button.isFullScreen == true)
                    {
                        graphics.ToggleFullScreen();
                        graphics.ApplyChanges();
                        Button.isFullScreen = false;
                    }
                    if (Button.willQuit)
                    {
                        Exit();
                    }
                    break;
                case GameState.Play:
                    Level.Update(gameTime);
                    break;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            

            

            Window.Title = Level.currency.ToString();   
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            

            switch (gameState)
            {
                case GameState.Start:
                    spriteBatch.Begin();
                    Menu.Draw(spriteBatch);
                    break;
                case GameState.Play:
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);
                    Level.Draw(spriteBatch);
                    break;
            }

            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
