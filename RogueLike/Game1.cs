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

        public static GameState gameState;
        public static Camera camera;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D kub;
        private double timeSinceLastFire;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
                    Menu.LoadButtons();
                    Menu.Update();
                    break;
                case GameState.Play:
                    Level.Update(gameTime, graphics);
                    break;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            timeSinceLastFire += gameTime.ElapsedGameTime.TotalSeconds;

            

            

            Window.Title = Level.currency.ToString();   
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            

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
