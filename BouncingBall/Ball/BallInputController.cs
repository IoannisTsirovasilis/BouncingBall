using BouncingBall.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BouncingBall.Ball
{
    class BallInputController : IGameEntity
    {
        private readonly BallEntity _ball;

        public int DrawOrder => 0;

        public BallInputController(BallEntity ball)
        {
            _ball = ball;
        }

        public void ProccessControls(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (IsAccelerating(keyboardState))
            {
                _ball.Accelerate();
            }
            else if (IsDecelerating(keyboardState))
            {
                _ball.Decelerate();
            }
            else
            {
                _ball.Run();
            }
        }
        
        private bool IsAccelerating(KeyboardState keyboardState)
        {
            return keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right);
        }

        private bool IsDecelerating(KeyboardState keyboardState)
        {
            return keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
