using BouncingBall.Ball;
using BouncingBall.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace BouncingBall.Ground
{
    class GroundManager : IGameEntity
    {
        private readonly BallEntity _ball;
        private readonly EntityManager _entityManager;
        private readonly List<GroundEntity> _grounds;
        private readonly Texture2D _texture;

        private const int SPRITE_WIDTH = 900;

        private const int GROUND_POS_X = 0;
        private const int GROUND_POS_Y = 450 - 64;

        public int DrawOrder => 0;

        public GroundManager(Texture2D spriteSheet, BallEntity ball, EntityManager entityManager)
        {
            _ball = ball;
            _entityManager = entityManager;
            _texture = spriteSheet;
            _grounds = new List<GroundEntity>();
        }

        public void Initialize()
        {
            _grounds.Clear();

            foreach (GroundEntity gt in _entityManager.GetEntitiesOfType<GroundEntity>())
            {
                _entityManager.RemoveEntity(gt);
            }

            GroundEntity ground = new GroundEntity(_texture, new Vector2(GROUND_POS_X, GROUND_POS_Y));
            _grounds.Add(ground);

            _entityManager.AddEntity(ground);

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {          
        }

        public void Update(GameTime gameTime)
        {
            if (_grounds.Any())
            {
                float maxPosX = _grounds.Max(g => g.Position.X);

                if (maxPosX < 0)
                {
                    SpawnGround();
                }
            }

            List<GroundEntity> groundsToRemove = new List<GroundEntity>();

            foreach (GroundEntity gt in _grounds)
            {
                gt.Position = new Vector2(gt.Position.X - _ball.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, gt.Position.Y);

                if (gt.Position.X <= -SPRITE_WIDTH)
                {
                    _entityManager.RemoveEntity(gt);
                    groundsToRemove.Add(gt);
                }
            }

            foreach (GroundEntity gt in groundsToRemove)
            {
                _grounds.Remove(gt);
            }                
        }

        private void SpawnGround()
        {
            GroundEntity ground = new GroundEntity(_texture, new Vector2(GROUND_POS_X + SPRITE_WIDTH, GROUND_POS_Y));

            _entityManager.AddEntity(ground);
            _grounds.Add(ground);
        }
    }
}
