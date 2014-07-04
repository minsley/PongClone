using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    public class Score
    {
        private readonly SpriteFont font;
        private readonly Rectangle gameBoundaries;

        public int Player1 { get; set; }
        public int Player2 { get; set; }

        public Score(SpriteFont font, Rectangle gameBoundaries)
        {
            this.font = font;
            this.gameBoundaries = gameBoundaries;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var scoreText = String.Format("{0} | {1}", Player1, Player2);
            var scorePosition = new Vector2(
                gameBoundaries.Width/2 - font.MeasureString(scoreText).X/2,
                gameBoundaries.Height - font.MeasureString(scoreText).Y + 10
                );
            spriteBatch.DrawString(font, scoreText, scorePosition, Color.White);
        }
    }
}
