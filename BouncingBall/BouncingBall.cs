using BouncingBall.Ball;
using BouncingBall.Entities;
using BouncingBall.Ground;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BouncingBall
{
    public class BouncingBall : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private const int WINDOW_WIDTH = 900;
        public  const int WINDOW_HEIGHT = 450;

        private const string ASSET_NAME_BALL = "ball";
        private const string ASSET_NAME_GROUND = "ground";

        private BallEntity _ball;
        private BallManager _ballManager;
        private GroundManager _groundManager;
        private readonly EntityManager _entityManager;
        private BallInputController _ballInputController;

        private const int BALL_POS_X = 10;
        private const int BALL_POS_Y = WINDOW_HEIGHT - 96;        

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

            _entityManager.AddEntity(_ball);
            _entityManager.AddEntity(_ballManager);
            _entityManager.AddEntity(_groundManager);

            _groundManager.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

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
    }
}
