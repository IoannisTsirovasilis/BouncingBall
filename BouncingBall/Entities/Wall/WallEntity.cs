using BouncingBall.Ball;
using BouncingBall.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace BouncingBall.Wall
{
    class WallEntity : IComponentEntity
    {
        private const int SPRITE_WIDTH = 23;
        private const int SPRITE_HEIGHT = 64;
        private readonly BallEntity _ball;

        public Vector2 Position { get; private set; }

        public Texture2D Texture { get; }

        public int DrawOrder => 3;

        public Rectangle CollisionBox => new Rectangle((int)Position.X, (int)Position.Y, SPRITE_WIDTH, SPRITE_HEIGHT);

        public WallEntity(Texture2D texture, Vector2 position, BallEntity ball)
        {
            Texture = texture;
            Position = position;
            _ball = ball;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            float posX = Position.X - _ball.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position = new Vector2(posX, Position.Y);

            CheckCollisions();
        }

        private void CheckCollisions()
        {
            if (CollisionBox.Intersects(_ball.CollisionBox))
            {
                _ball.Die();
            }

        }
    }
}
