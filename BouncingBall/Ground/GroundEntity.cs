using BouncingBall.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingBall.Ground
{
    class GroundEntity : IComponentEntity
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; }

        public int DrawOrder => 1;

        public GroundEntity(Texture2D spriteSheet, Vector2 position)
        {
            Position = position;
            Texture = spriteSheet;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
