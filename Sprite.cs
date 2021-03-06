﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    public abstract class Sprite
    {
        protected readonly Texture2D texture;
        public Vector2 Location;
        public int Height { get { return texture.Height; } }
        public int Width { get { return texture.Width; } }
        public Vector2 Velocity { get; protected set; }
        public Rectangle BoundingBox { get { return new Rectangle((int)Location.X, (int)Location.Y, Width, Height); } }

        public Sprite(Texture2D texture, Vector2 location)
        {
            this.texture = texture;
            
            Location = location;
            Velocity = Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Location, Color.White);
        }

        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            Location += Velocity;
        }

        protected abstract void CheckBounds();
    }
}
