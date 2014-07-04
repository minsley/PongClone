using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongClone
{
    public class Paddle : Sprite
    {
        public enum PlayerTypes
        {
            Human,
            Computer
        }

        private readonly Rectangle _screenBounds;
        private readonly PlayerTypes _playerType;
        private readonly float moveSpeed = 8f;
        private readonly float computerDelayDistance = 20f;

        public Paddle(Texture2D texture, Vector2 location, Rectangle screenBounds, PlayerTypes playerType) : base(texture, location)
        {
            _screenBounds = screenBounds;
            _playerType = playerType;
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (_playerType == PlayerTypes.Human)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    Velocity = new Vector2(0, moveSpeed);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    Velocity = new Vector2(0, -moveSpeed);
                }
                else
                {
                    Velocity = Vector2.Zero;
                }
            }
            else if (_playerType == PlayerTypes.Computer)
            {
                if (gameObjects.Ball.Location.Y > Location.Y + Height + computerDelayDistance)
                {
                    Velocity = new Vector2(0, moveSpeed);
                }
                else if (gameObjects.Ball.Location.Y < Location.Y - computerDelayDistance)
                {
                    Velocity = new Vector2(0, -moveSpeed);
                }
                else
                {
                    Velocity = Vector2.Zero;
                }
            }

            base.Update(gameTime, gameObjects);
            CheckBounds();
        }

        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, _screenBounds.Height - texture.Height);
        }
    }
}
