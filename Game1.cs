#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace PongClone
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        protected GameObjects gameObjects;

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
            gameObjects = new GameObjects{
                WindowSize = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height)
            };
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

            var paddleTexture = Content.Load<Texture2D>("Paddle");
            var paddle1StarLoc = new Vector2(0f, gameObjects.WindowSize.Height/2 - paddleTexture.Height/2);
            var paddle2StartLoc = new Vector2(gameObjects.WindowSize.Width - paddleTexture.Width,
                gameObjects.WindowSize.Height / 2 - paddleTexture.Height / 2);

            var paddle1 = new Paddle(paddleTexture, paddle1StarLoc, gameObjects.WindowSize, Paddle.PlayerTypes.Human);
            var paddle2 = new Paddle(paddleTexture, paddle2StartLoc, gameObjects.WindowSize, Paddle.PlayerTypes.Computer);
            var ball = new Ball(Content.Load<Texture2D>("Ball"), new Vector2(paddleTexture.Width, 0), gameObjects.WindowSize);
            ball.AttachTo(paddle1);

            var score = new Score(Content.Load<SpriteFont>("PongFont"), gameObjects.WindowSize);

            gameObjects.Ball = ball;
            gameObjects.Player1Paddle = paddle1;
            gameObjects.Player2Paddle = paddle2;
            gameObjects.Score = score;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameObjects.Player1Paddle.Update(gameTime, gameObjects);
            gameObjects.Player2Paddle.Update(gameTime, gameObjects);
            gameObjects.Ball.Update(gameTime, gameObjects);

            if (gameObjects.Ball.Location.X >= gameObjects.WindowSize.Width)
            {
                gameObjects.Score.Player1++;
                gameObjects.Ball.AttachTo(gameObjects.Player2Paddle);
            }
            else if (gameObjects.Ball.Location.X <= 0 - gameObjects.Player1Paddle.Width)
            {
                gameObjects.Score.Player2++;
                gameObjects.Ball.AttachTo(gameObjects.Player1Paddle);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            
            gameObjects.Player1Paddle.Draw(spriteBatch);
            gameObjects.Player2Paddle.Draw(spriteBatch);
            gameObjects.Ball.Draw(spriteBatch);
            gameObjects.Score.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

    public class GameObjects
    {
        public Paddle Player1Paddle { get; set; }
        public Paddle Player2Paddle { get; set; }
        public Ball Ball { get; set; }

        public Rectangle WindowSize { get; set; }
        public Score Score { get; set; }
    }
}
