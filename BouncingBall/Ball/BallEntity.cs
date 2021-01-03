using BouncingBall.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingBall.Ball
{
    class BallEntity : IComponentEntity
    {
        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; }
        public float Speed { get; private set; }
        private readonly float _accelerationPerSecond = 30f;
        private float _accelerateSpeed = 800;
        private float _decelerateSpeed = 200f;
        private float _runningSpeed = 400f;
        private const float MAX_SPEED = 1600f;
        public float StartPosY { get; }
        public BallState State { get; private set; }

        public float VerticalVelocity { get; private set; }

        public int DrawOrder => 2;

        private const float GRAVITY = 2500;
        private const float START_VERTICAL_VELOCITY = -1300;

        public BallEntity(Texture2D spriteSheet, Vector2 position)
        {
            Position = position;
            StartPosY = Position.Y;
            Texture = spriteSheet;
            VerticalVelocity = START_VERTICAL_VELOCITY;
            State = BallState.Idle;
            Speed = _runningSpeed;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (State != BallState.Idle)
            {
                Position = new Vector2(Position.X, Position.Y + VerticalVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                VerticalVelocity += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (Position.Y >= StartPosY)
            {
                VerticalVelocity = START_VERTICAL_VELOCITY;
            }

            if (State == BallState.Accelerating)
            {
                Speed = _accelerateSpeed;
            }
            else if (State == BallState.Decelerating)
            {
                Speed = _decelerateSpeed;
            }
            else
            {
                Speed = _runningSpeed;
            }

            if (_runningSpeed >= MAX_SPEED)
            {
                _runningSpeed = MAX_SPEED;
            }
            else
            {
                _runningSpeed += _accelerationPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }    
            
            if (_accelerateSpeed >= MAX_SPEED)
            {
                _accelerateSpeed = MAX_SPEED;
            }
            else
            {
                _accelerateSpeed += _accelerationPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (_decelerateSpeed >= MAX_SPEED)
            {
                _decelerateSpeed = MAX_SPEED;
            }
            else
            {
                _decelerateSpeed += _accelerationPerSecond * (float)gameTime.ElapsedGameTime.TotalSeconds;
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

        public void Accelerate()
        {
            State = BallState.Accelerating;
        }

        public void Decelerate()
        {
            State = BallState.Decelerating;
        }

        public void Run()
        {
            State = BallState.Running;
        }
    }
}
