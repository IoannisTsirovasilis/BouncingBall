using BouncingBall.Ball;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BouncingBall.Entities.ScoreBoard
{
    class ScoreBoardEntity : IGameEntity
    {
        public Vector2 Position { get; }

        public SpriteFont Font { get; }

        private const int SCORE_DISCOUNT_FACTOR = 100;
        private const int SCOREBOARD_MARGIN = 200;
        private readonly BallEntity _ball;

        public int DrawOrder => 10;

        public int Score { get; set; } = 0;
        public int CoinsCollected { get; private set; } = 0;

        public ScoreBoardEntity(SpriteFont font, Vector2 position, BallEntity ball)
        {
            Font = font;
            Position = position;
            _ball = ball;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(Font, "Coins: " + CoinsCollected.ToString().PadLeft(3, '0'), Position, Color.Black);

            int score = Score / SCORE_DISCOUNT_FACTOR;
            Vector2 scorePosition = new Vector2(Position.X + SCOREBOARD_MARGIN, Position.Y);
            spriteBatch.DrawString(Font, "Score: " + score.ToString().PadLeft(5, '0'), scorePosition, Color.Black);
        }

        public void Update(GameTime gameTime)
        {
            Score += (int) Math.Floor(_ball.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void IncreaseCoinsCollected()
        {
            CoinsCollected++;
        }

        public void Reset()
        {
            Score = 0;
            CoinsCollected = 0;
        }
    }
}
