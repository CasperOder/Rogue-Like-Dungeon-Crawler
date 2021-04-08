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
            graphics.PreferredBackBufferWidth = 1850;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();

            base.Initialize();
        }
        private void DrawOnFrontRenderTarget()
        {

            GraphicsDevice.SetRenderTarget(Level.frontRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();

            
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
        }

        private void DrawOnBackRenderTarget()
        {
            GraphicsDevice.SetRenderTarget(Level.backRenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();

            for (int a = 0; a < Level.backgroundTiles.GetLength(0); a++)
            {
                for (int b = 0; b < Level.backgroundTiles.GetLength(1); b++)
                {
                    Level.backgroundTiles[a, b].Draw(spriteBatch);

                }
            }

            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Constants.LoadConstants();
            kub = Content.Load<Texture2D>(@"kub");

            SpriteSheetManager.LoadContent(Content);

            Level.LoadBackgroundTiles(GraphicsDevice);

            camera = new Camera(GraphicsDevice.Viewport);
            Level.Load_Level();
            
            Level.backRenderTarget = new RenderTarget2D(GraphicsDevice, Window.ClientBounds.Width, Window.ClientBounds.Height);
            Level.frontRenderTarget = new RenderTarget2D(GraphicsDevice, Window.ClientBounds.Width, Window.ClientBounds.Height);

            DrawOnBackRenderTarget();

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Level.Update(gameTime);

            
            DrawOnFrontRenderTarget();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            spriteBatch.Draw(Level.backRenderTarget, Vector2.Zero, Color.White);
            spriteBatch.Draw(Level.frontRenderTarget, Vector2.Zero, Color.White);

            Level.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
