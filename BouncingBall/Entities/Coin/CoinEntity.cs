using BouncingBall.Ball;
using BouncingBall.Entities.ScoreBoard;
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
        private readonly ScoreBoardEntity _scoreBoard;
        private readonly EntityManager _entityManager;
        private readonly SoundEffect _coinPickupSoundEffect;

        public Vector2 Position { get; private set; }

        public Texture2D Texture { get; }

        public int DrawOrder => 3;

        public Rectangle CollisionBox => new Rectangle((int)Position.X, (int)Position.Y, SPRITE_WIDTH, SPRITE_HEIGHT);

        public CoinEntity(Texture2D texture, Vector2 position, BallEntity ball, ScoreBoardEntity scoreBoard, 
            EntityManager entityManager, SoundEffect coinPickupSoundEffect)
        {
            Texture = texture;
            Position = position;
            _ball = ball;
            _scoreBoard = scoreBoard;
            _entityManager = entityManager;
            _coinPickupSoundEffect = coinPickupSoundEffect;
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
                _scoreBoard.IncreaseCoinsCollected();
                _coinPickupSoundEffect.Play();
                _entityManager.RemoveEntity(this);
            }

        }
    }
}
