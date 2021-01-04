using BouncingBall.Ball;
using BouncingBall.Entities;
using BouncingBall.Entities.Coin;
using BouncingBall.Entities.ScoreBoard;
using BouncingBall.Graphics;
using BouncingBall.Ground;
using BouncingBall.Wall;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BouncingBall
{
    public class BouncingBall : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int WINDOW_WIDTH = 900;
        public const int WINDOW_HEIGHT = 450;

        private const string ASSET_NAME_BALL = "ball";
        private const string ASSET_NAME_GROUND = "ground";
        private const string ASSET_NAME_WALL = "wall";
        private const string ASSET_NAME_SCORE_BOARD_FONT = "score-board";
        private const string ASSET_NAME_COIN_0 = "coin-0";
        private const string ASSET_NAME_COIN_1 = "coin-1";
        private const string ASSET_NAME_COIN_2 = "coin-2";
        private const string ASSET_NAME_COIN_3 = "coin-3";
        private const string ASSET_NAME_COIN_4 = "coin-4";
        private const string ASSET_NAME_COIN_PICKUP = "coin-pickup";
        private const float ROTATING_COIN_ANIMATION_FRAME_DURATION = 0.15f;

        private BallEntity _ball;
        private BallManager _ballManager;
        private GroundManager _groundManager;
        private readonly EntityManager _entityManager;
        private BallInputController _ballInputController;
        private ScoreBoardEntity _scoreBoard;
        private SpriteFont _scoreBoardFont;
        private WallManager _wallManager;
        private CoinManager _coinManager;
        private SoundEffect _coinPickupSoundEffect;
        private TextureAnimation _rotatingCoinAnimation;
        private Texture2D _coin0;
        private Texture2D _coin1;
        private Texture2D _coin2;
        private Texture2D _coin3;
        private Texture2D _coin4;

        private const int BALL_POS_X = 10;
        private const int BALL_POS_Y = WINDOW_HEIGHT - 96;
        private const int SCORE_BOARD_POS_X = WINDOW_WIDTH - 400;
        private const int SCORE_BOARD_POS_Y = 10;

        public BouncingBall()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _entityManager = new EntityManager();            
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var ballTexture = Content.Load<Texture2D>(ASSET_NAME_BALL);
            _ball = new BallEntity(ballTexture, new Vector2(BALL_POS_X, BALL_POS_Y));            

            _ballManager = new BallManager(_ball);

            var groundTexture = Content.Load<Texture2D>(ASSET_NAME_GROUND);
            _groundManager = new GroundManager(groundTexture, _ball, _entityManager);

            _ballInputController = new BallInputController(_ball);

            _scoreBoardFont = Content.Load<SpriteFont>(ASSET_NAME_SCORE_BOARD_FONT);
            _scoreBoard = new ScoreBoardEntity(_scoreBoardFont, new Vector2(SCORE_BOARD_POS_X, SCORE_BOARD_POS_Y), _ball);

            var wallTexture = Content.Load<Texture2D>(ASSET_NAME_WALL);
            _wallManager = new WallManager(wallTexture, _ball, _scoreBoard, _entityManager);

            _coinPickupSoundEffect = Content.Load<SoundEffect>(ASSET_NAME_COIN_PICKUP);

            _rotatingCoinAnimation = new TextureAnimation();

            _coin0 = Content.Load<Texture2D>(ASSET_NAME_COIN_0);
            _coin1 = Content.Load<Texture2D>(ASSET_NAME_COIN_1);
            _coin2 = Content.Load<Texture2D>(ASSET_NAME_COIN_2);
            _coin3 = Content.Load<Texture2D>(ASSET_NAME_COIN_3);
            _coin4 = Content.Load<Texture2D>(ASSET_NAME_COIN_4);

            _rotatingCoinAnimation.AddFrame(_coin0, ROTATING_COIN_ANIMATION_FRAME_DURATION);
            _rotatingCoinAnimation.AddFrame(_coin1, ROTATING_COIN_ANIMATION_FRAME_DURATION * 2);
            _rotatingCoinAnimation.AddFrame(_coin2, ROTATING_COIN_ANIMATION_FRAME_DURATION * 3);
            _rotatingCoinAnimation.AddFrame(_coin3, ROTATING_COIN_ANIMATION_FRAME_DURATION * 4);
            _rotatingCoinAnimation.AddFrame(_coin4, ROTATING_COIN_ANIMATION_FRAME_DURATION * 5);

            _rotatingCoinAnimation.Play();

            _coinManager = new CoinManager(_coin0, _ball, _scoreBoard, _entityManager, _coinPickupSoundEffect,
                _rotatingCoinAnimation);

            

            _entityManager.AddEntity(_ball);
            _entityManager.AddEntity(_ballManager);
            _entityManager.AddEntity(_groundManager);
            _entityManager.AddEntity(_scoreBoard);
            _entityManager.AddEntity(_wallManager);
            _entityManager.AddEntity(_coinManager);

            _groundManager.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Replay();
            }

            _ballInputController.ProccessControls(gameTime);
            _entityManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            _entityManager.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool Replay()
        {
            _ball.Initialize();

            _wallManager.Reset();
            _coinManager.Reset();
            _scoreBoard.Reset();

            _groundManager.Initialize();

            return true;
        }
    }
}
