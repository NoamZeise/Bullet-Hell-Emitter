using bulletHell.Enemy.Patterns;
using Camera2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection;

namespace bulletHell
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int SCREEN_WIDTH = 1600;
        public static int SCREEN_HEIGHT = 900;

        Camera camera;

        Pattern test;
        Texture2D bulletTex;
        Texture2D emitterTex;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            camera = new Camera(GraphicsDevice, graphics, new RenderTarget2D(GraphicsDevice, SCREEN_WIDTH, SCREEN_HEIGHT));

            test = new Pattern(new Vector2(800, 200), 3, 0.02, 500, 500, 45, 0);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bulletTex = Content.Load<Texture2D>("bullet");
            emitterTex = Content.Load<Texture2D>("emitter");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        KeyboardState previousState;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                test._currentRotation += 0.2f;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                test._currentRotation -= 0.2f;

            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus) && previousState.IsKeyUp(Keys.OemPlus))
                test._shooterNum++;
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus) && previousState.IsKeyUp(Keys.OemMinus))
                test._shooterNum--;

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && previousState.IsKeyUp(Keys.Up))
                test._delay += 0.005;
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && previousState.IsKeyUp(Keys.Down))
                test._delay -= 0.005;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                test._spin += 0.5f;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                test._spin -= 0.5f;

            if (Keyboard.GetState().IsKeyDown(Keys.PageUp))
                test._speed += 10f;
            if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
                test._speed -= 10f;
            if (Keyboard.GetState().IsKeyDown(Keys.A) && previousState.IsKeyUp(Keys.A))
                test._seperation -= 1f;
            if (Keyboard.GetState().IsKeyDown(Keys.D) && previousState.IsKeyUp(Keys.D))
                test._seperation += 1f;
            if (Keyboard.GetState().IsKeyDown(Keys.Delete))
                test.Bullets.Clear();

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad8))
                test.Position.Y -= 5f;
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
                test.Position.Y += 5f;
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad4))
                test.Position.X -= 5f;
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad6))
                test.Position.X += 5f;

            test.Update(gameTime);

            previousState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.SetRenderTarget(camera.RenderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.Translation);


            foreach (var bullet in test.Bullets)
                spriteBatch.Draw(bulletTex, new Vector2(bullet.Position.X - bulletTex.Width / 2, bullet.Position.Y - bulletTex.Height / 2), Color.White);
           // spriteBatch.Draw(emitterTex, new Vector2(test.Position.X - emitterTex.Width / 2, test.Position.Y - emitterTex.Height / 2), Color.White);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(camera.RenderTarget, camera.ScreenRectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
