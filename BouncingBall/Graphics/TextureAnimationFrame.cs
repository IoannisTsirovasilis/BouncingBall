using Microsoft.Xna.Framework.Graphics;
using System;

namespace BouncingBall.Graphics
{
    class TextureAnimationFrame
    {
        private Texture2D _texture;

        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value ?? throw new ArgumentNullException("value", "The texture cannot be null.");
            }
        }

        public float TimeStamp { get; }

        public TextureAnimationFrame(Texture2D texture, float timeStamp)
        {
            Texture = texture;
            TimeStamp = timeStamp;
        }

    }
}
