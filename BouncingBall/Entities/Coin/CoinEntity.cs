using BouncingBall.Ball;
using BouncingBall.Entities.ScoreBoard;
using BouncingBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingBall.Entities.Coin
{
    class CoinEntity : IComponentEntity
    {
        private const int SPRITE_WIDTH = 16;
        private const int SPRITE_HEIGHT = 16;
        private readonly BallEntity _ball;

        public Vector2 Position { get; private set; }

        public Texture2D Texture { get; }

        public int DrawOrder => 3;

        public Rectangle CollisionBox => new Rectangle((int)Position.X, (int)Position.Y, SPRITE_WIDTH, SPRITE_HEIGHT);

        public CoinEntity(Texture2D texture, Vector2 position, BallEntity ball)
        {
            Texture = texture;
            Position = position;
            _ball = ball;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public void Update(GameTime gameTime)
        {
            float posX = Position.X - _ball.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position = new Vector2(posX, Position.Y);
        }

        
    }
}
