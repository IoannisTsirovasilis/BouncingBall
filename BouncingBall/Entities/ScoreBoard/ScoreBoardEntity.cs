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

        private const int _scoreDiscountFactor = 100;
        private readonly BallEntity _ball;

        public int DrawOrder => 10;

        public int Score { get; set; } = 0;

        public ScoreBoardEntity(SpriteFont font, Vector2 position, BallEntity ball)
        {
            Font = font;
            Position = position;
            _ball = ball;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int score = Score / _scoreDiscountFactor;
            spriteBatch.DrawString(Font, "Score: " + score.ToString().PadLeft(5, '0'), Position, Color.Black);
        }

        public void Update(GameTime gameTime)
        {
            Score += (int) Math.Floor(_ball.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
