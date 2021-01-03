using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingBall.Entities
{
    interface IComponentEntity : IGameEntity
    {
        Vector2 Position { get; }
        Texture2D Texture { get; }
    }
}
