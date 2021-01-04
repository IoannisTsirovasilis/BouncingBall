using BouncingBall.Ball;
using BouncingBall.Entities.ScoreBoard;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BouncingBall.Entities.Coin
{
    class CoinManager : IGameEntity
    {
        private readonly BallEntity _ball;
        private readonly EntityManager _entityManager;
        private readonly Texture2D _texture;
        private readonly ScoreBoardEntity _scoreBoard;
        private readonly Random _random;
        private readonly SoundEffect _coinPickupSoundEffect;

        private int _lastSpawnScore = -1;
        private double _currentTargetDistance;

        private const int COIN_DESPAWN_POS_X = -16;
        private const int COIN_POS_X = BouncingBall.WINDOW_WIDTH;
        private const int COIN_POS_Y = 128;
        private const int MIN_SPAWN_DISTANCE = 2000;
        private const int MIN_COIN_DISTANCE = 600;
        private const int MAX_COIN_DISTANCE = 900;
        private const int COIN_DISTANCE_SPEED_TOLERANCE = 40;
        private const int COINS_TO_SPAWN = 3;
        private const int COINS_DISTANCE = 20;

        public int DrawOrder => 0;
        private bool CanSpawnCoins => _ball.IsAlive && _scoreBoard.Score >= MIN_SPAWN_DISTANCE;

        public CoinManager(Texture2D spriteSheet, BallEntity ball, ScoreBoardEntity scoreBoard, 
            EntityManager entityManager, SoundEffect coinPickupSoundEffect)
        {
            _ball = ball;
            _entityManager = entityManager;
            _texture = spriteSheet;
            _scoreBoard = scoreBoard;
            _random = new Random();
            _coinPickupSoundEffect = coinPickupSoundEffect;
        }

        public void Initialize()
        {
            foreach (CoinEntity w in _entityManager.GetEntitiesOfType<CoinEntity>())
            {
                _entityManager.RemoveEntity(w);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (CanSpawnCoins && _scoreBoard.Score - _lastSpawnScore >= _currentTargetDistance)
            {
                _currentTargetDistance = _random.NextDouble()
                    * (MAX_COIN_DISTANCE - MIN_COIN_DISTANCE) + MIN_COIN_DISTANCE;

                _currentTargetDistance += (_ball.Speed - BallEntity.START_SPEED) / (BallEntity.MAX_SPEED - BallEntity.START_SPEED)
                    * COIN_DISTANCE_SPEED_TOLERANCE;

                _lastSpawnScore = _scoreBoard.Score;

                SpawnCoin();
            }

            foreach (CoinEntity wall in _entityManager.GetEntitiesOfType<CoinEntity>())
            {
                if (wall.Position.X < COIN_DESPAWN_POS_X)
                    _entityManager.RemoveEntity(wall);
            }
        }

        private void SpawnCoin()
        {
            for (int i = 0; i < COINS_TO_SPAWN; i++)
            {
                CoinEntity wall = new CoinEntity(_texture, new Vector2(COIN_POS_X + i * COINS_DISTANCE, COIN_POS_Y), 
                    _ball, _scoreBoard, _entityManager, _coinPickupSoundEffect);
                _entityManager.AddEntity(wall);
            }
        }

        public void Reset()
        {
            foreach (CoinEntity coin in _entityManager.GetEntitiesOfType<CoinEntity>())
            {
                _entityManager.RemoveEntity(coin);
            }

            _currentTargetDistance = 0;
            _lastSpawnScore = -1;
        }
    }
}
