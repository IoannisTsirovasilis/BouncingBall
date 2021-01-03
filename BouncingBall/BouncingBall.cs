using BouncingBall.Ball;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BouncingBall
{
    public class BouncingBall : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const string ASSET_NAME_BALL = "ball";
        private BallEntity _ball;
        private BallManager _ballManager;
        private const int WINDOW_WIDTH = 900;
        private const int WINDOW_HEIGHT = 450;
        private const int BALL_POS_X = 10;
        private const int BALL_POS_Y = WINDOW_HEIGHT - 96;

        public BouncingBall()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            _ball.Update(gameTime);
            _ballManager.Update(gameTime);           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            _ballManager.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
