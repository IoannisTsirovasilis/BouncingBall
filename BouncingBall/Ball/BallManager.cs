using BouncingBall.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingBall.Ball
{
    class BallManager : IGameEntity
    {
        private readonly BallEntity _ball;

        public int DrawOrder => 0;

        public BallManager(BallEntity ball)
        {
            _ball = ball;
        }

        private bool ShouldJump()
        {
            return _ball.Position.Y >= _ball.StartPosY;
        }

        private bool ShouldFall()
        {
            return _ball.VerticalVelocity >= 0;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _ball.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            if (ShouldJump())
            {
                _ball.Jump();
            }
            else if (ShouldFall())
            {
                _ball.Fall();
            }
        }
    }
}
