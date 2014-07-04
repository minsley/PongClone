using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongClone
{
    public class Ball : Sprite
    {
        private readonly Rectangle _screenBounds;
        private Paddle attachedToPaddle;

        public Ball(Texture2D texture, Vector2 location, Rectangle screenBounds) : base(texture, location)
        {
            _screenBounds = screenBounds;
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (attachedToPaddle != null)
            {
                Location.Y = attachedToPaddle.Location.Y + attachedToPaddle.Height/2 - Height/2;
                Location.X = attachedToPaddle == gameObjects.Player1Paddle
                    ? gameObjects.Player1Paddle.Width
                    : gameObjects.WindowSize.Width - gameObjects.Player2Paddle.Width - Width;

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Velocity = new Vector2(10f, attachedToPaddle.Velocity.Y);
                    attachedToPaddle = null;
                }
            } else
            {
                if (BoundingBox.Intersects(gameObjects.Player1Paddle.BoundingBox))
                {
                    Velocity = new Vector2(-Velocity.X, gameObjects.Player1Paddle.Velocity.Y);
                }
                else if (BoundingBox.Intersects(gameObjects.Player2Paddle.BoundingBox))
                {
                    Velocity = new Vector2(-Velocity.X, gameObjects.Player2Paddle.Velocity.Y);
                }
            }

            base.Update(gameTime, gameObjects);
            CheckBounds();
        }

        protected override void CheckBounds()
        {
            if(Location.Y >= _screenBounds.Height - Height || Location.Y <= 0 + Height) 
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
        }

        public void AttachTo(Paddle paddle)
        {
            attachedToPaddle = paddle;
        }
    }
}
