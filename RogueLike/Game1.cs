using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RogueLike
{
    public class Game1 : Game
    {
        public static Camera camera;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D kub;
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Constants.LoadConstants();
            graphics.PreferredBackBufferWidth = Constants.windowWidth;
            graphics.PreferredBackBufferHeight = Constants.windowHeight;
            graphics.ApplyChanges();

            //kub = Content.Load<Texture2D>(@"kub");

            SpriteSheetManager.LoadContent(Content);
            LoadWeapons.LoadAllWeapons();
            HUD.Load(Content);
            
            camera = new Camera(GraphicsDevice.Viewport);
            Level.Load_Level(graphics);
            
            
            
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            Level.Update(gameTime, graphics);

            Window.Title = Level.currency.ToString();   
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            
            Level.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
