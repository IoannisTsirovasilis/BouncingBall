using BouncingBall.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingBall.Ball
{
    class BallEntity : IGameEntity
    {
        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; }
        public float Speed { get; private set; }
        public float StartPosY { get; }
        public BallState State { get; private set; }

        public float VerticalVelocity { get; private set; }

        private const float GRAVITY = 2500;
        private const float START_VERTICAL_VELOCITY = -1300;

        public BallEntity(Texture2D spriteSheet, Vector2 position)
        {
            Position = position;
            StartPosY = Position.Y;
            Texture = spriteSheet;
            VerticalVelocity = START_VERTICAL_VELOCITY;
            State = BallState.Idle;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (State == BallState.Jumping || State == BallState.Falling)
            {
                Position = new Vector2(Position.X, Position.Y + VerticalVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                VerticalVelocity += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Position.Y >= StartPosY)
            {
                VerticalVelocity = START_VERTICAL_VELOCITY;
            }
        }

        public void Jump()
        {
            State = BallState.Jumping;
        }

        public void Fall()
        {
            State = BallState.Falling;
        }
    }
}
