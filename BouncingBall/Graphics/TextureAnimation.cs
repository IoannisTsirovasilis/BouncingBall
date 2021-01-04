using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BouncingBall.Graphics
{
    class TextureAnimation
    {
        private List<TextureAnimationFrame> _frames = new List<TextureAnimationFrame>();

        public TextureAnimationFrame this[int index]
        {
            get
            {
                return GetFrame(index);
            }
        }

        public int FrameCount => _frames.Count;

        public TextureAnimationFrame CurrentFrame
        {
            get
            {
                return _frames
                    .Where(f => f.TimeStamp <= PlaybackProgress)
                    .OrderBy(f => f.TimeStamp)
                    .LastOrDefault();
            }
        }

        public float Duration
        {
            get
            {
                return _frames.Any() ?_frames.Max(f => f.TimeStamp) : 0;
            }

        }

        public bool IsPlaying { get; private set; }

        public float PlaybackProgress { get; private set; }

        public bool ShouldLoop { get; set; } = true;

        public void AddFrame(Texture2D texture, float timeStamp)
        {
            TextureAnimationFrame frame = new TextureAnimationFrame(texture, timeStamp);

            _frames.Add(frame);
        }

        public void Update(GameTime gameTime)
        {
            if (IsPlaying)
            {
                PlaybackProgress += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (PlaybackProgress > Duration)
                {
                    if (ShouldLoop)
                        PlaybackProgress -= Duration;
                    else
                        Stop();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            TextureAnimationFrame frame = CurrentFrame;

            if (frame != null)
            {
                spriteBatch.Draw(frame.Texture, position, Color.White);
            }
        }

        public void Play()
        {
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
            PlaybackProgress = 0;
        }

        public TextureAnimationFrame GetFrame(int index)
        {
            if (index < 0 || index >= _frames.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "A frame with index " + index + " does not exist in this animation.");
            }

            return _frames[index];
        }

        public void Clear()
        {
            Stop();
            _frames.Clear();
        }

        public static TextureAnimation CreateSimpleAnimation(Texture2D texture, Point startPos, 
            int width, int height, Point offset, int frameCount, float frameLength)
        {
            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            TextureAnimation anim = new TextureAnimation();

            for (int i = 0; i < frameCount; i++)
            {
                anim.AddFrame(texture, frameLength * i);

                if (i == frameCount - 1)
                    anim.AddFrame(texture, frameLength * (i + 1));
            }

            return anim;

        }

    }
}
