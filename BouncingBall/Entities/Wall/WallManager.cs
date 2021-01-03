using BouncingBall.Ball;
using BouncingBall.Entities;
using BouncingBall.Entities.ScoreBoard;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BouncingBall.Wall
{
    class WallManager : IGameEntity
    {
        private readonly BallEntity _ball;
        private readonly EntityManager _entityManager;
        private readonly Texture2D _texture;
        private readonly Random _random;
        private readonly ScoreBoardEntity _scoreBoard;

        private int _lastSpawnScore = -1;
        private double _currentTargetDistance;

        private const int WALL_DESPAWN_POS_X = -64;
        private const int WALL_POS_X = BouncingBall.WINDOW_WIDTH;
        private const int WALL_POS_Y = 450 - 96;
        private const int WALL_HEIGHT = 64;
        private const int MIN_SPAWN_DISTANCE = 1500;
        private const int MIN_WALL_DISTANCE = 1200;
        private const int MAX_WALL_DISTANCE = 2000;
        private const int WALL_DISTANCE_SPEED_TOLERANCE = 40;

        public int DrawOrder => 0;
        private bool CanSpawnWalls => _ball.IsAlive && _scoreBoard.Score >= MIN_SPAWN_DISTANCE;

        public WallManager(Texture2D spriteSheet, BallEntity ball, ScoreBoardEntity scoreBoard, EntityManager entityManager)
        {
            _ball = ball;
            _entityManager = entityManager;
            _texture = spriteSheet;
            _scoreBoard = scoreBoard;
            _random = new Random();
        }

        public void Initialize()
        {
            foreach (WallEntity w in _entityManager.GetEntitiesOfType<WallEntity>())
            {
                _entityManager.RemoveEntity(w);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (CanSpawnWalls &&
                (_lastSpawnScore <= 0 || (_scoreBoard.Score - _lastSpawnScore >= _currentTargetDistance)))
            {
                _currentTargetDistance = _random.NextDouble()
                    * (MAX_WALL_DISTANCE - MIN_WALL_DISTANCE) + MIN_WALL_DISTANCE;

                _currentTargetDistance += (_ball.Speed - BallEntity.START_SPEED) / (BallEntity.MAX_SPEED - BallEntity.START_SPEED) 
                    * WALL_DISTANCE_SPEED_TOLERANCE;

                _lastSpawnScore = _scoreBoard.Score;

                SpawnWall(_random.Next(1, 4));
            }

            foreach (WallEntity wall in _entityManager.GetEntitiesOfType<WallEntity>())
            {
                if (wall.Position.X < WALL_DESPAWN_POS_X)
                    _entityManager.RemoveEntity(wall);
            }
        }

        private void SpawnWall(int height)
        {
            for (int i = 0; i < height; i++)
            {
                WallEntity wall = new WallEntity(_texture, new Vector2(WALL_POS_X, WALL_POS_Y - i * WALL_HEIGHT), _ball);
                _entityManager.AddEntity(wall);
            }
        }

        public void Reset()
        {
            foreach (WallEntity wall in _entityManager.GetEntitiesOfType<WallEntity>())
            {
                _entityManager.RemoveEntity(wall);
            }

            _currentTargetDistance = 0;
            _lastSpawnScore = -1;
        }
    }
}
