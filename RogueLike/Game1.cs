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
            GameOver,
            Pause
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

        /// <summary>
        /// Sets the gamestate to Game Over 
        /// </summary>
        public static void SetGameOverScreen()
        {
            GameOver.LoadScreen();
            gameState = GameState.GameOver;
            SavefileHandler.DeleteSavefile();
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Start:
                    Menu.Update(graphics, Content);

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
                    Menu.Update(graphics, Content);
                    break;
                case GameState.Pause:
                    Menu.Update(graphics, Content);

                    if (Button.isFullScreen == true)
                    {
                        graphics.ToggleFullScreen();
                        graphics.ApplyChanges();
                        Button.isFullScreen = false;
                    }
                    break;
                case GameState.GameOver:
                    GameOver.Update(gameTime, graphics, Content);
                    break;
            }
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            
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
                case GameState.Pause:
                    spriteBatch.Begin();
                    Level.Draw(spriteBatch);
                    Menu.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    spriteBatch.Begin();
                    GameOver.Draw(spriteBatch);
                    break;
            }

            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
